
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
    }
}
