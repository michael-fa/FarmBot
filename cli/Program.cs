using MyFreeFarmer;
using MyFreeFarmer.Game.API;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V108.Page;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace cli
{
    public class Program
    {
        static IniFile m_CfgUser = null!;
        static Thread m_CmdThread = null!;
        public static Farmer m_Farmer = null!;

        [DllImport("Kernel32")]
        static public extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        public delegate bool EventHandler(CtrlType sig);
        public static EventHandler? m_Handler;
        public static bool m_Running = false;










        public enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        public static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    CloseSafely();
                    return false;
                default:
                    return false;
            }
        }


        static void Main(string[] args)
        {
            m_Running = true;
            //CustomCode();
            Console.OutputEncoding = Encoding.Unicode;
            m_Handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(m_Handler, true);

            string[] m_LoginData = { "", "", "" };
            
            Console.WriteLine("Hello, World!");

            if (!Directory.Exists(Environment.CurrentDirectory + @"/Settings/")) Directory.CreateDirectory(Environment.CurrentDirectory + @"/Settings/");
            if (!Directory.Exists(Environment.CurrentDirectory + @"/Logs/")) Directory.CreateDirectory(Environment.CurrentDirectory + @"/Logs/");

            if (!File.Exists(Environment.CurrentDirectory + @"/Settings/account.ini"))
            {
                m_CfgUser = new IniFile(Environment.CurrentDirectory + @"/Settings/account.ini");
                m_CfgUser.Write("server", "1");
                m_CfgUser.Write("username", "changeme");
                m_CfgUser.Write("password", "changeme");
                Log.Error("No account config found in /Settings/ ! Please set your own server, name and password.");
                Environment.Exit(0);
                return;
            }
            else m_CfgUser = new IniFile(Environment.CurrentDirectory + @"/Settings/account.ini");

            if (!m_CfgUser.KeyExists("server") || !m_CfgUser.KeyExists("username") || !m_CfgUser.KeyExists("password"))
            {
                m_CfgUser.Write("server", "1");
                m_CfgUser.Write("username", "changeme");
                m_CfgUser.Write("password", "changeme");
                Log.Error("No account data set in /Settings/account.ini ! Please set your own server, name and password.");
                Environment.Exit(0);
                return;
            }

            m_LoginData[0] = m_CfgUser.Read("server");
            m_LoginData[1] = m_CfgUser.Read("username");
            m_LoginData[2] = m_CfgUser.Read("password");
            m_Farmer = new Farmer(Convert.ToInt32(m_LoginData[0]), m_LoginData[1], m_LoginData[2]);

            m_CmdThread = new Thread(() => CommandHandler());
            m_CmdThread.Start();
        }

        static public void CommandHandler()
        {
            string input = Console.ReadLine()!;
            string[] args = input.Split(' ');

            while (m_Running)
            {
                if (input.Length > 0)
                {
                    switch (input.Split(' ').ToArray()[0])
                    {
                        case "selectitem":
                            List<object> li = new List<object>();
                            li.Add(Int32.Parse(args[1]));
                            break;
                        case "printstats":
                            Console.WriteLine("INFO: User: " + m_Farmer.m_Info.m_loginUser + "\n     Level: " + m_Farmer.m_Info.GetLevel() + "\n     Points:" + m_Farmer.m_Info.GetPoints() + "\n     Cash: " + m_Farmer.m_Info.GetMoney() + "\n     Coins: " + m_Farmer.m_Info.GetCoins() + "\n     Premium: " + (m_Farmer.m_Info.HasPremium() ? ("Yes") : ("No")));
                            break;

                        case "test":
                            Console.WriteLine("TEST CMD CALLED");
                            FarmPositions.ClearField(m_Farmer, Int32.Parse(args[1]));
                            break;
                    }
                }
                Thread.Sleep(100);
                input = Console.ReadLine()!;
                args = input.Split(' ');
            }
        }

        static void CustomCode()
        {
 
            Environment.Exit(0);
        }

        static void CloseSafely()
        {
            m_Running = false;
            if(m_Farmer != null)m_Farmer.Stop();
            //copy current log txt to one with the date in name and delete the old one | we also replace : or / to - so that theres no language based error in folder/file names
            File.Copy(System.AppContext.BaseDirectory + "/Logs/current.txt", (System.AppContext.BaseDirectory + "Logs/" + DateTime.Now.ToString().Replace(':', '-').Replace('/', '-') + ".txt"));
            if (File.Exists(System.AppContext.BaseDirectory + "/Logs/current.txt")) File.Delete(System.AppContext.BaseDirectory + "/Logs/current.txt");
            Environment.Exit(0);
        }
    }
}