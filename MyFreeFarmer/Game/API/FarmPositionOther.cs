using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class FarmPositionOther
    {
        //So this apparently opens the position container up. We should not assume we are still on farm overview here!
        public static bool Retrieve(Farmer game, int farmID, int posID, int slotID)
        {
            game.m_JavaScript.ExecuteScript("farmAction('harvestProduction', " + farmID + ", " + posID + ", " + slotID + ");");
            return true;
        }

        public static bool StartProduction(Farmer game, int posID, int productionSlotID, int possibleProductSlot)
        {
            game.m_JavaScript.ExecuteScript("factory.start('start', " + posID + ", " + productionSlotID + ", " + possibleProductSlot + ");");
            return true;
        }
    }
}
