using MyFreeFarmer.Game;
using MyFreeFarmer.Game.API;
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
         * Find a way to get a players level by his points
         * routinen erstellen (Stapelverarbeitung)
        */

        public Farmer(int server, string user, string password)
        {
            if(!Directory.Exists(Environment.CurrentDirectory + @"/Logs") || !Directory.Exists(Environment.CurrentDirectory + @"/Settings"))
            {
                Log.Error("Logs or Settings Directory missing!");
                return;
            }
            if (!File.Exists(Environment.CurrentDirectory + @"/Settings/account.ini"))
            {
                Log.Error("/Settings/account.ini missing!");
                return;
            }

            try
            {
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
            catch { }

            
        }
        public void Stop()
        {
            while (this.m_Info.m_IsBusy) { }
            foreach(Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle.Contains("My Free Farm"))
                    proc.CloseMainWindow();

                
            }
        }
    }
}