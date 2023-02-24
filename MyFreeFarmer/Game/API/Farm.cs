using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class Farm
    {
        public static bool Move(Farmer game, int farmid)
        {
            game.m_JavaScript.ExecuteScript("mapGo2Location(\"farm\", " + farmid + ");");
            return true;
        }
    }
}
