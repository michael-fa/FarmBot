using OpenQA.Selenium;

namespace MyFreeFarmer.Game.API
{
    public static class GlobalBox
    {
        public static bool IsShown(Farmer game)
        {
            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='globalbox']"));
            if (x != null && x.Displayed) return true;
            return false;
        }

        public static bool Close(Farmer game)
        {
            if (IsShown(game))
            {
                var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='globalbox_close']"));
                if (x != null && x.Displayed) x.Click();
                return false;
            }
            else return false;
        }
    }
}
