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
        internal static bool Login(Farmer game)
        {
            if (game.m_Info.m_LoggedIn) return false;

            //Login phase
            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginserver']")).SendKeys(game.m_Info.m_loginServer.ToString());
            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginusername']")).SendKeys(game.m_Info.m_loginUser);
            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginpassword']")).SendKeys(game.m_Info.m_loginPassword);

            Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginbutton']")).Click();

            Console.WriteLine("Waiting");

            //wait until we got the game loaded
            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
            while (x == null || !x.Displayed)
            {
                x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='userinfoscontainer']"));
            }

            Console.WriteLine("Done");

            //retrieve all the user data along the way

            game.m_Info.m_Level = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='levelnum']")).Text);
            game.m_Info.m_Points = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='pkt']")).Text.Replace(".", ""));
            game.m_Info.m_Money = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='bar']")).Text.Replace(".", "").Replace(",", "").Replace(" kT", ""));
            game.m_Info.m_Coins = Convert.ToInt32(Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='coins']")).Text.Replace(".", ""));

            return (game.m_Info.m_LoggedIn = true);
        }
    }
}
