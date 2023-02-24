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
                game.m_JavaScript.ExecuteScript("hideDiv('globalbox'); hideDiv('globaltransp'); $('globalbox_content').innerHTML = '';");
                return true;
            }
            else return false;
        }
    }
}
