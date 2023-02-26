using OpenQA.Selenium;

namespace MyFreeFarmer.Game.API
{
    public static class GlobalBox
    {
        public enum GB_DISPLAY_STYLE
        {
            ICONS_NONE = 0,
            ICONS_YES_ONLY = 1,
            ICONS_ALL = 2,
            ICONS_ALLYESFUNC = 3
        }
        public static bool m_customShown = false;
        public static void Show(Farmer game, string title, string content, GB_DISPLAY_STYLE style, string func = null!)
        {
            switch(style)
            {
                case GB_DISPLAY_STYLE.ICONS_NONE:
                    game.m_JavaScript.ExecuteScript("globalBox(\"" + title + "\", \"" + content + "\", 'showMain()', null, 0, 1);");
                    break;

                case GB_DISPLAY_STYLE.ICONS_YES_ONLY:
                    game.m_JavaScript.ExecuteScript("globalBox(\"" + title + "\", \"" + content + "\", 'showMain()', null, 1);");
                    break;

                case GB_DISPLAY_STYLE.ICONS_ALL:
                    game.m_JavaScript.ExecuteScript("globalBox(\"" + title + "\", \"" + content + "\", 'showMain()');");
                    break;

                case GB_DISPLAY_STYLE.ICONS_ALLYESFUNC:
                    game.m_JavaScript.ExecuteScript("globalBox(\"" + title + "\", \"" + content + "\", '" + func + "');");
                    break;
            }
            
            m_customShown = true;
        }
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
