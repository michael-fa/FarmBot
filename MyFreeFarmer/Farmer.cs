using MyFreeFarmer.Game;
using MyFreeFarmer.Game.API;
using MyFreeFarmer.Game.Scripting;
using OpenQA.Selenium;
using AMXWrapperCore;
using OpenQA.Selenium.Firefox;
using System.Diagnostics;

namespace MyFreeFarmer
{



    public class Farmer
    {
        public Game.GameInfo m_Info;
        public IWebDriver m_Driver = null!;
        public FirefoxDriverService m_DriverService = FirefoxDriverService.CreateDefaultService();
        public IJavaScriptExecutor m_JavaScript = null!;

        /*Todo:
         * Startup: Wait for login/password/server div to appear, instead of sleeping 2s
         * routinen erstellen (Stapelverarbeitung)
         * Find a way to get a JS value about the current open position
        */

        public Farmer(int server, string user, string password)
        {
            try
            {

                using var handler = CommandHandler.Run(this, typeof(Farmer));

                Manager.m_Farmer = this;
                Manager.LoadFiles();
                Manager.RunInit(); //we check for the first script that had "OnInit" in it, <-- lol.

                //Init the "core game" structure
                m_Info = new Game.GameInfo(this, server, user, password);

                m_Driver = new FirefoxDriver(m_DriverService);
                Log.Debug("PID = " + m_DriverService.ProcessId);
                m_Driver.Manage().Window.Size = new System.Drawing.Size(1100, 950);
                m_Driver.Manage().Window.Position = new System.Drawing.Point(850, 1);
                m_JavaScript = (IJavaScriptExecutor)m_Driver;
                m_Driver.Url = "https://myfreefarm.de";

                //Fix
                m_JavaScript.ExecuteScript("currentposition = 0;");

                Console.WriteLine("\n----------------------------------");

                Thread.Sleep(2000);

                Auth.Login(this);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            
        }

        public void Stop()
        {
            foreach(Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle.Contains("My Free Farm"))
                    proc.CloseMainWindow();

                
            }

            foreach (Script x in Manager.m_Scripts)
            {
                Manager.UnloadScript(x);
            }

            if (m_Driver != null)m_Driver.Quit();
            Environment.Exit(0);
        }

        public int AmxScriptCommand(string cmdtext)
        {
            AMXPublic p;
            foreach (Script x in Manager.m_Scripts)
            {
                p = x.m_Amx.FindPublic("OnConsoleCommand");
                var cmd = p.AMX.Push(cmdtext);
                if (p != null)
                {
                    int success = p.Execute();
                    p.AMX.Release(cmd);
                    return success;
                }
            }
            return -1;
        }



        //Hardcoded commands..? 

        [ConsoleCommand(Description = "Display some game statistics")]
        public void stats()
        {
            Console.WriteLine("INFO: User: " + m_Info.m_loginUser + "\n     Level: " + m_Info.GetLevel() + "\n     Points:" + m_Info.GetPoints() + "\n     Cash: " + m_Info.GetMoney() + "\n     Coins: " + m_Info.GetCoins() + "\n     Premium: " + (m_Info.HasPremium() ? ("Yes") : ("No")));
        }

        [ConsoleCommand(Description = "Close the current farm field")]
        public void closecurrent()
        {
            Game.API.FarmPositions.CloseCurrent(this);
            Console.WriteLine("POS: " + this.m_Info.GetCurrentPosition());
        }


        [ConsoleCommand(Description = "Ope a farm field")]
        public void open(int id)
        {
            Game.API.FarmPositions.Open(this, id);
            Console.WriteLine("POS: " + this.m_Info.GetCurrentPosition());
        }
    }


    
}