using System;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

public static class Log
{
    public static void WriteLine(string _msg)
    {
        Console.WriteLine(_msg);
        if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
    }
    public static void Info(string _msg)
    {
        Console.WriteLine("[INFO] " + _msg);
        if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
    }

    public static void Error(string _msg)
    {
        Console.WriteLine("[ERROR] " + _msg);
        if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
    }

    public static void Warning(string _msg)
    {
        Console.WriteLine("[WARNING] " + _msg);
        if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
    }

    public static void Debug(string _msg)
    {
#if DEBUG
       
        Console.WriteLine("[DEBUG] " + _msg);
        System.Diagnostics.Debug.WriteLine("Utils.Log: " + _msg);
        if (_msg.Length > 0) File.AppendAllText("Logs/current.txt", _msg + "\n");    
#endif
    }

    public static void Exception(Exception e)
    {
        Console.WriteLine("---------------------------------------\n[EXCEPTION] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n---------------------------------------\n");
        File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", "---------------------------------------\n[EXCEPTION] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n-------------------------------------- -\n");
    }
}