using MyFreeFarmer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#nullable enable

// ===== Attribute + Metadata =====
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class ConsoleCommandAttribute : Attribute
{
    public string? Name { get; }
    public string? Description { get; init; }
    public string[] Aliases { get; init; } = Array.Empty<string>();
    public string? Example { get; init; }

    public ConsoleCommandAttribute() { }
    public ConsoleCommandAttribute(string name) => Name = name;
}

public sealed class CommandDescriptor
{
    public required string Name { get; init; }
    public string[] Aliases { get; init; } = Array.Empty<string>();
    public string? Description { get; init; }
    public string? Example { get; init; }
    public required MethodInfo Method { get; init; }
    public object? TargetInstance { get; init; }
}

// ===== Registry: scannt Assembly/Instanzen nach [ConsoleCommand] =====
public static class CommandRegistry
{



    private static readonly ConcurrentDictionary<string, CommandDescriptor> _byName =
        new(StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyCollection<CommandDescriptor> Descriptors => _byName.Values.Distinct().ToArray();

    /// <summary>Registriert alle mit [ConsoleCommand] markierten Methoden in den angegebenen Targets (Instanzen oder Typen).</summary>
    public static void Register(params object[] targetsOrTypes)
    {
        foreach (var obj in targetsOrTypes.Where(o => o is not null))
        {
            Type t;
            object? instance = null;

            if (obj is Type tt)
            {
                t = tt;
            }
            else
            {
                t = obj.GetType();
                instance = obj;
            }

            // Flags so setzen, dass bei Typen NUR statische, bei Instanzen statische+Instanz gescannt werden:
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            if (instance != null) flags |= BindingFlags.Instance;

            foreach (var mi in t.GetMethods(flags))
            {
                var attr = mi.GetCustomAttribute<ConsoleCommandAttribute>();
                if (attr is null) continue;

                var name = attr.Name ?? mi.Name;
                var desc = new CommandDescriptor
                {
                    Name = name,
                    Aliases = attr.Aliases,
                    Description = attr.Description,
                    Example = attr.Example,
                    Method = mi,
                    TargetInstance = mi.IsStatic ? null : instance
                };

                void add(string key)
                {
                    if (CommandRegistryTryGetExisting(key, out var existing))
                    {
                        // Gleiche Methode? Dann einfach ignorieren (kein Fehler).
                        if (existing.Method == mi) return;

                        // Andere Methode? Klarer Konflikt.
                        throw new InvalidOperationException(
                            $"Befehl/Alias '{key}' ist bereits registriert (Konflikt zwischen " +
                            $"{existing.Method.DeclaringType!.Name}.{existing.Method.Name} und {t.Name}.{mi.Name}).");
                    }
                    _byName.TryAdd(key, desc);
                }

                add(name);
                foreach (var alias in attr.Aliases ?? Array.Empty<string>())
                    add(alias);
            }

            // Hilfsfunktion (kannst du oben in die Klasse packen)
            static bool CommandRegistryTryGetExisting(string key, out CommandDescriptor existing)
                => _byName.TryGetValue(key, out existing!);

        }
    }

    public static bool TryGet(string name, out CommandDescriptor desc) => _byName.TryGetValue(name, out desc!);
}

// ===== CommandHandler: ReadLine-Loop + Parser + Binder =====
public sealed class CommandHandler : IDisposable
{

    static Farmer m_Farmer = null;
    private readonly CancellationTokenSource _cts = new();
    private Thread? _thread;

    public static CommandHandler Run(params object[] targetsOrTypes)
    {
        CommandRegistry.Register(targetsOrTypes);
        var handler = new CommandHandler();
        handler._thread = new Thread(() => handler.Loop(handler._cts.Token))
        {
            IsBackground = true,
            Name = "ConsoleCommandLoop"
        };
        
        m_Farmer = (Farmer)targetsOrTypes[0];
        handler._thread.Start();
        return handler;
    }

    public void Stop()
    {
        _cts.Cancel();
        _thread?.Join();
    }

    public void Dispose() => Stop();

    private void Loop(CancellationToken ct)
    {
        PrintWelcome();
        while (true) // !ct.IsCancellationRequested should be like this but idk why this quits after a invalid command maybe fixed 
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (line is null) break;
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            try
            {
                if (!ExecuteLine(line))

                {
                    int res = m_Farmer.AmxScriptCommand(line);
                    if (res == -1)
                    {
                        Console.WriteLine("Unknown command, use 'help' for a list of commands.");
                    }

                }
                    
                
            }
            catch (TargetInvocationException tie)
            {
                Console.WriteLine($"Error (inner): {tie.InnerException?.Message ?? tie.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void PrintWelcome()
    {
        Console.WriteLine("Console Command Handler ready. Type 'help'.");
    }

    public static bool ExecuteLine(string line)
    {
        var tokens = Tokenize(line);
        if (tokens.Count == 0) return true;

        var cmd = tokens[0];
        if (string.Equals(cmd, "help", StringComparison.OrdinalIgnoreCase))
        {
            if (tokens.Count == 1) PrintAllHelp();
            else PrintCommandHelp(tokens[1]);
            return true;
        }

        if (!CommandRegistry.TryGet(cmd, out var desc)) return false;

        var args = tokens.Skip(1).ToList();
        var parameters = desc.Method.GetParameters();
        var bound = BindArgs(parameters, args);
        desc.Method.Invoke(desc.TargetInstance, bound);
        return true;
    }

    // --- Tokenizer: unterstützt "quoted strings" & Escapes ---
    private static List<string> Tokenize(string input)
    {
        var list = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (c == '\\' && i + 1 < input.Length) { sb.Append(input[++i]); continue; }
            if (c == '"') { inQuotes = !inQuotes; continue; }
            if (char.IsWhiteSpace(c) && !inQuotes)
            {
                if (sb.Length > 0) { list.Add(sb.ToString()); sb.Clear(); }
                continue;
            }
            sb.Append(c);
        }
        if (sb.Length > 0) list.Add(sb.ToString());
        return list;
    }

    // --- Binder: unterstützt positional + named (--param=val / --param val) + bool-Switches ---
    private static object?[] BindArgs(ParameterInfo[] parameters, List<string> args)
    {
        var result = new object?[parameters.Length];
        var named = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Sammle named args:
        for (int i = 0; i < args.Count;)
        {
            var a = args[i];
            if (a.StartsWith("--"))
            {
                var keyVal = a.Substring(2);
                string key, val;

                var eqIdx = keyVal.IndexOf('=');
                if (eqIdx >= 0) { key = keyVal[..eqIdx]; val = keyVal[(eqIdx + 1)..]; i++; }
                else
                {
                    key = keyVal;
                    // --flag (bool switch) oder --key value
                    if (i + 1 < args.Count && !args[i + 1].StartsWith("--"))
                    {
                        val = args[i + 1];
                        i += 2;
                    }
                    else
                    {
                        val = "true";
                        i++;
                    }
                }
                named[key] = val;
            }
            else break; // ab hier nur noch positionale
        }

        // verbleibende sind positionale:
        var positionals = args.Where(a => !a.StartsWith("--")).ToList();
        int posIdx = 0;

        for (int p = 0; p < parameters.Length; p++)
        {
            var param = parameters[p];

            // params string[] sammelt rest
            bool isParams = param.GetCustomAttribute<ParamArrayAttribute>() != null;
            if (isParams)
            {
                var rest = positionals.Skip(posIdx).ToArray();
                result[p] = rest;
                posIdx = positionals.Count;
                continue;
            }

            // named?
            if (named.TryGetValue(param.Name!, out var namedVal))
            {
                result[p] = ConvertTo(namedVal, param.ParameterType);
                continue;
            }

            // positional?
            if (posIdx < positionals.Count)
            {
                result[p] = ConvertTo(positionals[posIdx++], param.ParameterType);
                continue;
            }

            // default?
            if (param.HasDefaultValue)
            {
                result[p] = param.DefaultValue;
                continue;
            }

            // bool-Switch ohne Wert? (implizit false -> default), sonst Fehler
            if (param.ParameterType == typeof(bool))
            {
                result[p] = false;
                continue;
            }

            throw new ArgumentException($"Parameter '{param.Name}' missing.");
        }

        return result;
    }

    // --- Type Conversion: string, numerics, bool, enum, Guid, DateTime, TimeSpan ---
    private static object? ConvertTo(string value, Type targetType)
    {
        var t = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (t == typeof(string)) return value;
        if (t == typeof(bool)) return ParseBool(value);
        if (t.IsEnum) return Enum.Parse(t, value, true);
        if (t == typeof(Guid)) return Guid.Parse(value);
        if (t == typeof(DateTime)) return DateTime.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(TimeSpan)) return TimeSpan.Parse(value, CultureInfo.InvariantCulture);

        if (t == typeof(int)) return int.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(long)) return long.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(short)) return short.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(byte)) return byte.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(double)) return double.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(float)) return float.Parse(value, CultureInfo.InvariantCulture);
        if (t == typeof(decimal)) return decimal.Parse(value, CultureInfo.InvariantCulture);

        // Fallback: Try ChangeType
        return Convert.ChangeType(value, t, CultureInfo.InvariantCulture);
    }

    private static bool ParseBool(string s)
    {
        if (string.Equals(s, "1", StringComparison.OrdinalIgnoreCase)) return true;
        if (string.Equals(s, "0", StringComparison.OrdinalIgnoreCase)) return false;
        if (string.Equals(s, "true", StringComparison.OrdinalIgnoreCase)) return true;
        if (string.Equals(s, "false", StringComparison.OrdinalIgnoreCase)) return false;
        if (string.Equals(s, "yes", StringComparison.OrdinalIgnoreCase)) return true;
        if (string.Equals(s, "no", StringComparison.OrdinalIgnoreCase)) return false;
        throw new FormatException($"Invalid Bool value: {s}");
    }

    private static void PrintAllHelp()
    {
        var rows = CommandRegistry.Descriptors
            .OrderBy(d => d.Name, StringComparer.OrdinalIgnoreCase)
            .Select(d =>
            {
                var alias = d.Aliases.Length > 0 ? $" (Aliases: {string.Join(", ", d.Aliases)})" : "";
                return $"{d.Name}{alias} – {d.Description}";
            });

        Console.WriteLine("Commands:");
        foreach (var r in rows) Console.WriteLine("  " + r);
        Console.WriteLine("Hint: help <command>");
    }

    private static void PrintCommandHelp(string name)
    {
        if (!CommandRegistry.TryGet(name, out var d))
        {
            Console.WriteLine($"Command '{name}' not found!");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"{d.Name}");
        if (d.Aliases.Length > 0) sb.AppendLine($"Alias: {string.Join(", ", d.Aliases)}");

        // Signatur
        var pi = d.Method.GetParameters();
        var signature = string.Join(" ", pi.Select(p =>
        {
            var isParams = p.GetCustomAttribute<ParamArrayAttribute>() != null;
            var core = isParams ? $"{p.Name}..." : p.Name;
            var opt = p.HasDefaultValue || isParams ? $"[{core}]" : $"<{core}>";
            return opt;
        }));
        sb.AppendLine($"Usage: {d.Name} {signature}");
        if (!string.IsNullOrWhiteSpace(d.Description)) sb.AppendLine(d.Description);
        if (!string.IsNullOrWhiteSpace(d.Example)) sb.AppendLine($"Beispiel: {d.Example}");
        Console.WriteLine(sb.ToString());
    }
}