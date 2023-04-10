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
        public static int SelectRackItem(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            if (!int.IsEvenInteger(args1[0].AsInt32()))
                return 0;

            API.Rack.SelectItem(game, args1[0].AsInt32());
            return 1;
        }
    }
}
