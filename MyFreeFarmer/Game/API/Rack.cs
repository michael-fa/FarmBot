
namespace MyFreeFarmer.Game.API
{
    public static partial class Rack
    {
        public static bool SelectItem(Farmer game, int item_id)
        {
            if (!game.m_Info.m_LoggedIn) return false;
            game.m_JavaScript.ExecuteScript("selectRackItem(" + item_id + ");");
            return true;
        }

        public static int GetSelectedItem(Farmer game, int item_id)
        {
            if (!game.m_Info.m_LoggedIn) return 1;
            return Convert.ToInt32(game.m_JavaScript.ExecuteScript("return selected"));
        }
    }
}
