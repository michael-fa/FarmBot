using MyFreeFarmer.Game;
using MyFreeFarmer.Game.API;
using MyFreeFarmer.Game.Scripting;
using OpenQA.Selenium;
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

            foreach(Script x in Manager.m_Scripts)
            {
                Manager.UnloadScript(x);
            }
            
            if(m_Driver != null)m_Driver.Quit();
            Environment.Exit(0);
        }
    }
}