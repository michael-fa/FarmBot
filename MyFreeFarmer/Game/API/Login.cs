using AMXWrapperCore;
using MyFreeFarmer.Game.Scripting;
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

            game.m_Info.m_IsBusy = true;
            try
            {
                if (game.m_Info.m_loginServer == 1) Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys("01");
                else Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys(game.m_Info.m_loginServer.ToString());
                Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginusername']")).SendKeys(game.m_Info.m_loginUser);
                Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginpassword']")).SendKeys(game.m_Info.m_loginPassword);

                game.m_JavaScript.ExecuteScript("createToken();");

                //wait until we got the game loaded (logged in) and catch auth error
                var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
                IWebElement errorMsg = null!;
                Thread.Sleep(2000);
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
                    case NoSuchElementException:; break;
                    case ElementClickInterceptedException: break;
                }
            }
            finally { }
            game.m_Info.m_LoggedIn = true;

            AMXPublic p;
            foreach (Script x in Manager.m_Scripts)
            {
                p = x.m_Amx.FindPublic("OnGameAvailable");
                if (p != null) p.Execute();
            }

            game.m_Info.m_IsBusy = false;
        }
    }
}