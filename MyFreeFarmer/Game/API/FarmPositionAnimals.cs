using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class FarmPositionAnimals
    {
        public static bool Retrieve()
        {
            
            return true;
        }

        public static bool FeedAnimalPosition(Farmer game, int farmid, int posid)
        {
            game.m_JavaScript.ExecuteScript("feedCacheFire(" + farmid + ", " + posid + ");");
            return true;
        }
    }
}
