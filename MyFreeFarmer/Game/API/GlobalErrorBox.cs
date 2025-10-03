using OpenQA.Selenium;

namespace MyFreeFarmer.Game.API
{
    public static class GlobalErrorBox
    {
        public static bool m_customShown = false;
        public static void Show(Farmer game, string content, string func = null!)
        {
            game.m_JavaScript.ExecuteScript($"globalerrorbox({content}, {func});");
            m_customShown = true;
        }
        public static bool IsShown(Farmer game)
        {
            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='globalerrorbox']"));
            if (x != null && x.Displayed) return true;
            return false;
        }

        //Untested!
        public static bool Close(Farmer game)
        {
            if (IsShown(game))
            {
                game.m_JavaScript.ExecuteScript("hideDiv('globalerrorbox'); hideDiv('globaltransp'); $('globalerrorbox_content').innerHTML = '';");
                return true;
            }
            else return false;
        }
    }
}
