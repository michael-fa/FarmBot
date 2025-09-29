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
        public static int OpenFarmPosition(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            //check if string is only 0-9
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;

            API.FarmPositions.Open(game, args1[0].AsInt32());
            Thread.Sleep(1500);
            return 1;
        }

        public static int CloseCurrentFarmPosition(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            API.FarmPositions.CloseCurrent(game);
            Thread.Sleep(1500);
            return 1;
        }

        public static int GetCurrentFarmPosition(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            return (Int32)game.m_Info.GetCurrentPosition();
        }

        public static int HarvestAllFields(AMX amx1, AMXArgumentList args1, Script caller_script, Farmer game)
        {
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;
            API.FarmPositions.HarvestGarden(game, args1[0].AsInt32());
            return 1;
        }

        public static int HarvestFieldInCurrentPos(AMX amx, AMXArgumentList args1, Script caller, Farmer game)
        {
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;

            API.FarmPositions.HarvestField(game, n);

            return 1;
        }

        public static int CultivateFieldInCurrentPos(AMX amx, AMXArgumentList args1, Script caller, Farmer game)
        {
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;

            API.FarmPositions.CultivateField(game, n);

            return 1;
        }

        public static int WaterFieldInCurrentPos(AMX amx, AMXArgumentList args1, Script caller, Farmer game)
        {
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;

            API.FarmPositions.WaterField(game, n);

            return 1;
        }

        public static int WaterGarden(AMX amx, AMXArgumentList args1, Script caller, Farmer game)
        {
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;

            API.FarmPositions.WaterGarden(game, n);

            return 1;
        }

        public static int HarvestGarden(AMX amx, AMXArgumentList args1, Script caller, Farmer game)
        {
            var isNumeric = int.TryParse(args1[0].AsInt32().ToString(), out int n);
            if (!isNumeric) return 0;

            API.FarmPositions.HarvestGarden(game, n);

            return 1;
        }

        public static int GetFarmPositionType(AMX amx, AMXArgumentList args1, Script caller, Farmer game)
        {
            try
            {
                if (args1.Length < 1) return -1;
                int pos = args1[0].AsInt32(); // << wichtig

                int type = API.FarmPositions.GetType(game, pos);
                // Debug:
                // Console.WriteLine($"GetFarmPositionType({pos}) = {type}");

                return type;
            }
            catch (Exception ex)
            {
                Log.Error($"GetFarmPositionType error: {ex}");
                return -1;
            }
        }


    }
}
