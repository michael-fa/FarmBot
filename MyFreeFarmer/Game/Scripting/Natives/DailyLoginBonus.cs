using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.Scripting
{
    public static partial class Natives
    {
        public static int LoginBonusIsOpen(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            if (API.DailyLoginBonus.IsShown(game)) return 1;
            return 0;
        }

        public static int CloseLoginBonus(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            if (API.DailyLoginBonus.IsShown(game))
            {
                API.DailyLoginBonus.Close(game);
                return 1;
            }
            return 0;
        }

        //TODO
        //CollectAvailableBonus
        //   --> OnLoginBonusRetrieved
    }
}
