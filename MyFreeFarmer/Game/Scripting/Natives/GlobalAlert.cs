using AMXWrapperCore;
using MyFreeFarmer.Game.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.Scripting
{
    public static partial class Natives
    {
        public static int PrintGlobalAlert(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            GlobalAlert.Show(game, args1[0].AsString());
            return 1;
        }
    }
}
