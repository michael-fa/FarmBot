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
        public static int DisplayGlobalBox(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            //GlobalBox.Close(game);
            GlobalBox.Show(game, args1[0].AsString(), args1[1].AsString(), (GlobalBox.GB_DISPLAY_STYLE)args1[2].AsInt32(), null);
            return 1;
        }

        public static int HideGlobalBox(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            //GlobalBox.Close(game);
            GlobalBox.Close(game);
            return 1;
        }

        public static int IsGlobalBoxShown(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            if (GlobalBox.IsShown(game)) return 1;
            else return 0;
        }
    }
}
