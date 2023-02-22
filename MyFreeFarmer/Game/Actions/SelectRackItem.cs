using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game
{
    public static partial class Actions
    {
        public static bool SelectRackItem(Farmer game, int item_id)
        {
            if (!game.m_Info.m_LoggedIn) return false;
            var x = game.m_Driver.FindElementIfExists(By.XPath(".//*[@id='rackitem" + item_id + "']"));
            if (x == null || !x.Displayed) return false;
            x.Click();
            return true;
        }
    }
}
