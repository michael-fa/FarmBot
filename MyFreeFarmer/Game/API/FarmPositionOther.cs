using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class FarmPositionOther
    {
        public static bool Retrieve()
        {
            return true;
        }

        /*
        public static bool StartProduction(Farmer game, int item_id, int slot)
        {
            if (!game.m_Info.m_LoggedIn) return false;
            
            game.m_JavaScript.ExecuteScript("selectRackItem(" + item_id + ");");
            return true;
        }*/
    }
}
