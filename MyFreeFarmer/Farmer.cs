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
            Thread.Sleep(3000);

            Actions.Login(this);

            Console.WriteLine("INFO: User: " + m_Info.m_loginUser + "\n     Level: " + m_Info.m_Level + "\n     Punkte:" + m_Info.m_Points + "\n     Geld: " + m_Info.m_Money + "\n     Coins: " + m_Info.m_Coins);

        }

    }
}