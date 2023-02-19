using MyFreeFarmer;
using OpenQA.Selenium.DevTools;

namespace cli
{
    internal class Program
    {
        static IniFile m_CfgUser = null!;
        static void Main(string[] args)
        {
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
            } else m_CfgUser = new IniFile(Environment.CurrentDirectory + @"/Settings/account.ini");

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
            Farmer farmer = new Farmer(Convert.ToInt32(m_LoginData[0]), m_LoginData[1], m_LoginData[2]);


        }
    }
}