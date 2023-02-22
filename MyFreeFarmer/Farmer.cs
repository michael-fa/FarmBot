using MyFreeFarmer.Game;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MyFreeFarmer
{
    public class Farmer
    {
        public Game.GameInfo m_Info;
        public IWebDriver m_Driver = null!;
        public IJavaScriptExecutor m_JavaScript = null!;

        /*Todo:
            Check for newsbox/error once in a while? 
                  or write an error handler when object is obscured we can get rid of whats in the way
            Startup: Wait for login/password/server div to appear, instead of sleeping 2s

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

            //Init the core game structure
            m_Info = new Game.GameInfo(server, user, password);

            m_Driver = new FirefoxDriver();
            m_JavaScript = (IJavaScriptExecutor)m_Driver;
            m_Driver.Url = "https://myfreefarm.de";
            Console.WriteLine("\n----------------------------------");

            Thread.Sleep(2000);

            ActionManager.Run();
        }

        ~Farmer()
        {
            if (ValueUpdate.m_Active)
                ValueUpdate.Stop();
            ActionManager.Stop();
            m_Driver.Quit();
            
        }
    }
}