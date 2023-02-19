using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game
{
    internal static partial class Actions
    {
        internal static void Login(Farmer game)
        {   
            if (game.m_Info.m_LoggedIn) return;

            ActionManager.isBusy = true;
            //Login phase
            if (game.m_Info.m_loginServer == 1) Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys("01");
            else Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys(game.m_Info.m_loginServer.ToString());
            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginusername']")).SendKeys(game.m_Info.m_loginUser);
            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginpassword']")).SendKeys(game.m_Info.m_loginPassword);

            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginbutton']")).Click();

            //wait until we got the game loaded (logged in) and catch auth error
            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
            var errorMsg = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='errormessage']"));
            while (x == null || !x.Displayed)
            {
                errorMsg = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='errormessage']"));
                if (errorMsg != null && errorMsg.Displayed) break;
                x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
            }

            if(errorMsg != null && errorMsg.Displayed && errorMsg.Text.Contains("Username oder Passwort sind fehlerhaft"))
            {
                Log.Error("Could not log in using the given account data. (Invalid username or password)");
                ActionManager.isBusy = false;
                Environment.Exit(0);
                return;
            }

            //retrieve all the user data along the way

            game.m_Info.m_Level = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='levelnum']")).Text);
            game.m_Info.m_Points = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='pkt']")).Text.Replace(".", ""));
            game.m_Info.m_Money = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='bar']")).Text.Replace(".", "").Replace(",", "").Replace(" kT", ""));
            game.m_Info.m_Coins = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='coins']")).Text.Replace(".", ""));

            ActionManager.isBusy = false;
            game.m_Info.m_LoggedIn = true;
        }
    }
}
