using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class NewsBox
    {
        public static bool IsShown(Farmer game)
        {
            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='newsbox']"));
            if (x != null && x.Displayed) return true;
            return false;
        }
    }
}
