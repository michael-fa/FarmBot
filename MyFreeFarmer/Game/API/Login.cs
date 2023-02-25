using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class Auth
    {
        public static void Login(Farmer game)
        {
            if (game.m_Info.m_LoggedIn) return;

            try
            {
                if (game.m_Info.m_loginServer == 1) Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys("01");
                else Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys(game.m_Info.m_loginServer.ToString());
                Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginusername']")).SendKeys(game.m_Info.m_loginUser);
                Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginpassword']")).SendKeys(game.m_Info.m_loginPassword);

                Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginbutton']")).Click();

                //wait until we got the game loaded (logged in) and catch auth error
                var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
                IWebElement errorMsg = null!;
                while (x == null || !x.Displayed)
                {
                    errorMsg = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='errormessage']"));
                    if (errorMsg != null && errorMsg.Displayed)
                    {
                        Log.Error("Could not log in using the given account data. (Invalid username or password)");
                        return;
                    }
                    x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
                }
            }
            catch (Exception ex)
            {
                switch (ex.InnerException)
                {
                    case NoSuchElementException: ; break;
                    case ElementClickInterceptedException: break;
                }
            }
            finally { }
            game.m_Info.m_LoggedIn = true;
        }
    }
}
