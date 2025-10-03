using AMXWrapperCore;
using MyFreeFarmer;
using MyFreeFarmer.Game.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyFreeFarmer
{
    public class ScriptTimer
    {
        public int ID;
        public bool m_Active = false;
        public bool m_Repeat = true;
        int m_msWait;
        string m_Func;
        Script m_ParentScript;
        System.Timers.Timer m_Timer;
        AMXPublic m_AMXCallback;
        public string m_ArgFrmt;
        public object[] m_Args;
        private object lockObject;
        public ScriptTimer(int interval, bool rep, string funcCall, Script arg_parent_Script)
        {
            m_ParentScript = arg_parent_Script!;
            m_AMXCallback = m_ParentScript.m_Amx.FindPublic(funcCall);
            if (m_AMXCallback == null)
            {
                return;
            }
            m_msWait = interval;
            m_Args = null!;
            m_ArgFrmt = null!;
            m_Func = funcCall!;
            m_Active = true;
            m_Repeat = rep;
            lockObject = new object();


            arg_parent_Script.m_ScriptTimers.Add(this);
            
            this.ID = arg_parent_Script.m_ScriptTimers.Count;

            m_Timer = new System.Timers.Timer(m_msWait);
            m_Timer.Elapsed += OnTimedEvent!;
            lockObject = new object();
            m_Timer.AutoReset = m_Repeat;
            m_Timer.Start();

            Log.Debug("Initialised Script-Timer (\"" + m_Func + "\") !");
        }

        public ScriptTimer(int interval, bool rep, string funcCall, Script arg_parent_Script, string _ArgsFrm, params object[] _args)
        {
            m_ParentScript = arg_parent_Script;
            m_AMXCallback = m_ParentScript.m_Amx.FindPublic(funcCall);
            if (m_AMXCallback == null)
            {
                return;
            }
            m_msWait = interval;
            m_Func = funcCall;
            m_Active = true;
            m_Repeat = rep;
            m_ArgFrmt = _ArgsFrm!;
            m_Args = _args.ToArray();


            arg_parent_Script.m_ScriptTimers.Add(this);
            this.ID = arg_parent_Script.m_ScriptTimers.Count;

            m_Timer = new System.Timers.Timer(m_msWait);
            m_Timer.Elapsed += OnTimedEvent!;
            lockObject = new object();
            m_Timer.AutoReset = m_Repeat;
            m_Timer.Start();
            Log.Debug("Initialised Script-Timer (\"" + m_Func + "\") !");
            return;
        }



        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                m_AMXCallback = m_ParentScript.m_Amx.FindPublic(m_Func);
                if (m_AMXCallback == null) { return; }
                List<CellPtr> _list = new List<CellPtr>();

                if (m_Args == null || m_Args.Length == 3 || m_ArgFrmt.Length == 0) //the end of args passed
                {
                    m_AMXCallback.Execute();
                    Log.Debug("Script-Timer invoked \"" + m_Func + "\"");
                    return;
                }

                for (int i = m_Args.Length - 1; i >= 0; i--)
                {
                    switch (m_Args[i].GetType().ToString())
                    {
                        case "System.Int32":
                            int intValue = (int)m_Args[i];
                            m_AMXCallback.AMX.Push(intValue);
                            break;

                        case "System.String":
                            string stringValue = (string)m_Args[i];
                            _list.Add(m_AMXCallback.AMX.Push(stringValue));
                            break;

                        case "System.Float":
                            float floatValue = (float)m_Args[i];
                            m_AMXCallback.AMX.Push((float)m_Args[i]);
                            break;
                    }
                }
                m_AMXCallback.Execute();

                foreach (CellPtr cell in _list)
                {
                    m_AMXCallback.AMX.Release(cell);
                }
                GC.Collect();

                Log.Debug("Script-TimerEx invoked \"" + m_Func + "\"");
            }
        }

        public bool KillTimer()
        {
            if (!this.m_Active) return false;


            m_Timer.Stop();
            this.m_Active = false;
            this.m_ArgFrmt = "";

            return true;
        }
    }
}
