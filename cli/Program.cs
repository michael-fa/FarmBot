using MyFreeFarmer;
using MyFreeFarmer.Game;
using MyFreeFarmer.Game.API;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V108.Page;
using System.Collections;

namespace cli
{
    internal class Program
    {
        static IniFile m_CfgUser = null!;
        static void Main(string[] args)
        {
            //CustomCode();


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
            Farmer farmer = new Farmer(Convert.ToInt32(m_LoginData[0]), m_LoginData[1], m_LoginData[2]);

            ActionManager.AddToPerform(new FarmAction(farmer, "Login", null!));


            string[] input = { Console.ReadLine()! };

            while (true)
            {
                if (input.Length>0)
                {
                    input = Console.ReadLine()!.Split(" ");
                    switch (input[0])
                    {
                        case "selectitem":
                            List<object> li = new List<object>();
                            li.Add(Int32.Parse(input[1]));
                            ActionManager.AddToPerform(new FarmAction(farmer, "SelectRackItem", li));
                            break;
                        case "printstats":Console.WriteLine("INFO: User: " + farmer.m_Info.m_loginUser + "\n     Points:" + farmer.m_Info.GetPoints() + "\n     Cash: " + farmer.m_Info.GetMoney() + "\n     Coins: " + farmer.m_Info.GetCoins() + "\n     Premium: " + (farmer.m_Info.HasPremium() ? ("Yes") : ("No") ));
                            break;

                        case "test":
                            FarmPositions.Open(farmer, Convert.ToInt32(input[1]));
                            break;
                        case "test2":
                            Farm.Move(farmer, 1);
                            break;
                    }
                }
                Thread.Sleep(100);
            }


        }

        static void CustomCode()
        {
 
            Environment.Exit(0);
        }
    }
}