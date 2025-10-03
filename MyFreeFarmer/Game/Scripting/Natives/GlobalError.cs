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
        public static int DisplayGlobalErrorBox(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            //GlobalBox.Close(game);
            GlobalErrorBox.Show(game, args1[0].AsString(), args1[1].AsString());
            return 1;
        }

        public static int HideGlobalErrorBox(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            //GlobalBox.Close(game);
            GlobalErrorBox.Close(game);
            return 1;
        }

        public static int IsGlobalErrorBoxShown(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            if (GlobalErrorBox.IsShown(game)) return 1;
            else return 0;
        }
    }
}
