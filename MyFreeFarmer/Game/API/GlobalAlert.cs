using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class GlobalAlert
    {
        public static void Show(Farmer game, string message)
        {
            game.m_JavaScript.ExecuteScript("globalAlert('" + message + "');");
        }
    }
}
