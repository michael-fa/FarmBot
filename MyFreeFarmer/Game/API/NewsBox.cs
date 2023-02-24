using OpenQA.Selenium;

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

        public static bool Close(Farmer game)
        {
            if (IsShown(game))
            {
                game.m_JavaScript.ExecuteScript("setNewsUnread(1);");
                return true;
            }
            else return false;
        }
    }
}
