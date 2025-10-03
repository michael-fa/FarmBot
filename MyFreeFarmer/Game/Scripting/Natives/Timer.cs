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
        public static int SetTimer(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1[2].AsInt32() > 1 || args1[2].AsInt32() < 0)
            {
                Log.Error("SetTimer: Argument 'repeating' is boolean. Please pass 0 or 1 only!");
                return 0;
            }

            try
            {
                ScriptTimer timer = new ScriptTimer(args1[1].AsInt32(), Convert.ToBoolean(args1[2].AsInt32()), args1[0].AsString(), caller_script);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return (caller_script.m_ScriptTimers.Count);
        }

        public static int SetTimerEx(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length < 4) return 1;
            try
            {
                int ln = args1[3].AsString().Length;
                object[] args = new object[ln];
                for (int i = 0; i < args1[3].AsString().Length; i++)
                {
                    switch (args1[3].AsString()[i])
                    {
                        case 'i':
                            args[i] = Cell.FromIntPtr(args1[i + 4].AsIntPtr()).AsInt32();
                            break;

                        case 'f':
                            args[i] = Cell.FromIntPtr(args1[i + 4].AsCellPtr().Value);
                            break;

                        case 's':
                            args[i] = args1[i + 4].AsString();
                            break;
                    }
                }
                ScriptTimer timer = new ScriptTimer(args1[1].AsInt32(), Convert.ToBoolean(args1[2].AsInt32()), args1[0].AsString(), caller_script, args1[3].AsString(), args);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return (caller_script.m_ScriptTimers.Count);
        }
        public static int KillTimer(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            foreach (ScriptTimer scrt in caller_script.m_ScriptTimers)
            {
                if (scrt != null)
                {
                    if (scrt.ID == args1[0].AsInt32())
                    {
                        scrt.KillTimer();
                        return 1;
                    }
                }
            }
            return 1;
        }
    }
}
