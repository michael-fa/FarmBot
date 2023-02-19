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

        public Farmer(int server, string user, string password)
        {
            //Init the core game structure
            m_Info = new Game.GameInfo(server, user, password);

            m_Driver = new FirefoxDriver();
            m_JavaScript = (IJavaScriptExecutor)m_Driver;
            m_Driver.Url = "https://myfreefarm.de";
            Console.WriteLine("\n----------------------------------");
            Thread.Sleep(3000);

            if(!Actions.Login(this))
            {
                Environment.Exit(0);
                return;
            }

            Console.WriteLine("INFO: User: " + m_Info.m_loginUser + "\n     Level: " + m_Info.m_Level + "\n     Points:" + m_Info.m_Points + "\n     Cash: " + m_Info.m_Money + "\n     Coins: " + m_Info.m_Coins);

        }

    }
}