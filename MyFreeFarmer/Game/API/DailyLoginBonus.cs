using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class DailyLoginBonus
    {
        public static bool IsShown(Farmer game)
        {
            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='loginbonus']"));
            if (x != null && x.Displayed) return true;
            return false;
        }

        public static bool Close(Farmer game)
        {
            if (!IsShown(game)) return false;
            game.m_JavaScript.ExecuteScript("loginbonus.close()");
            return false;
        }
    }
}