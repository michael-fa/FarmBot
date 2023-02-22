using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game
{
    public static partial class Actions
    {
        public static void HandleObstructingElements(Farmer game, Exception ex)
        {
            Log.Warning("Action on hold because: " +  ex.Message + "\n at " + ex.Source);
        }
    }
}
