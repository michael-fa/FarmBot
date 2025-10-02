using MyFreeFarmer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using AMXWrapperCore;

namespace MyFreeFarmer.Game.Scripting
{
    public class Script
    {
        public string m_amxFile = null!;
        public AMX m_Amx;
        public Farmer m_Instance;
        public byte[] m_Hash = null!;
        //public List<ScriptTimer> m_ScriptTimers;


        public Script(Farmer inst, string _amxFile)
        {
            this.m_Instance = inst;
            this.m_amxFile = _amxFile;
            try
            {
                m_Amx = new AMX(_amxFile);

            }
            catch (Exception e)
            {
                Log.Exception(e);
                return;
            }

            //Find and only use the first script that has oninit to execute init stuff.
            AMXPublic p = m_Amx.FindPublic("OnInit");
            if (p != null && Manager.m_InitScript == null) Manager.m_InitScript = p;

            this.m_Amx.LoadLibrary(AMXDefaultLibrary.Core | AMXDefaultLibrary.Float | AMXDefaultLibrary.String | AMXDefaultLibrary.Console | AMXDefaultLibrary.DGram | AMXDefaultLibrary.Time);
            this.RegisterNatives();
            return;
        }

        public void StopAllTimers()
        {
            /*   foreach (ScriptTimer timer in DiscordAMX.m_ScriptTimers)
               {
                   timer.KillTimer();
               }
            */
        }


        /*public void RunTimerCallback(AMXArgumentList m_Args, string m_ArgFrmt, string m_Func)
        {
            AMXPublic m_AMXCallback = this.m_Amx.FindPublic(m_Func);
            if (m_AMXCallback == null) return;
            

            try
            {
                if (!m_ArgFrmt.Equals("Cx00A01"))
                {
                    int count = (m_Args.Length - 1);

                    List<CellPtr> Cells = new List<CellPtr>();

                    //Important so the format ( ex "iissii" ) is aligned with the arguments pushed to the callback, not being reversed
                    string reversed_format = Utils.Scripting.Reverse(m_ArgFrmt);

                    foreach (char x in reversed_format.ToCharArray())
                    {
                        if (count == 3) break; //stop at the format argument.
                        Console.WriteLine("Do again: " + count);
                        switch (x)
                        {
                            case 'i':
                                {
                                    m_AMXCallback.AMX.Push(4);
                                    count--;
                                    continue;
                                }
                            case 'f':
                                {
                                    m_AMXCallback.AMX.Push((float)m_Args[count].AsCellPtr().Get().AsFloat());
                                    count--;
                                    continue;
                                }

                            case 's':
                                {
                                    Cells.Add(m_AMXCallback.AMX.Push(m_Args[count].AsString()));
                                    count--;
                                    continue;
                                }
                        }
                    }

                    m_AMXCallback.Execute();

                    foreach (CellPtr cell in Cells)
                    {
                        m_AMXCallback.AMX.Release(cell);
                    }
                    GC.Collect();



                    Utils.Log.Debug("Script-Timer invoked \"" + m_Func + "\" | Format: " + m_ArgFrmt, this);
                }
                else
                {
                    //Call without ex arguments
                    m_AMXCallback.Execute();
                    Utils.Log.Debug("Script-Timer invoked  \"" + m_Func + "\"", this);
                }
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex, this);
            }
        }*/

        public bool RegisterNatives()
        {
            /*m_Amx.Register("Loadscript", (amx1, args1) => Natives.CoreNatives.Loadscript(amx1, args1, this));
            m_Amx.Register("INI_ReadFloat", (amx1, args1) => Cell.FromFloat(Natives.ININatives.INI_ReadFloat(amx1, args1, this)).AsCellPtr().Value.ToInt32());
            */

            m_Amx.Register("DisplayGlobalBox", (amx1, args1) => Scripting.Natives.DisplayGlobalBox(amx1, args1, this, m_Instance));
            m_Amx.Register("HideGlobalBox", (amx1, args1) => Scripting.Natives.HideGlobalBox(amx1, args1, this, m_Instance));
            m_Amx.Register("IsGlobalBoxShown", (amx1, args1) => Scripting.Natives.IsGlobalBoxShown(amx1, args1, this, m_Instance));

            m_Amx.Register("DisplayGlobalAlert", (amx1, args1) => Scripting.Natives.PrintGlobalAlert(amx1, args1, this, m_Instance));

            m_Amx.Register("LoginBonusIsOpen", (amx1, args1) => Scripting.Natives.LoginBonusIsOpen(amx1, args1, this, m_Instance));
            m_Amx.Register("CloseLoginBonus", (amx1, args1) => Scripting.Natives.CloseLoginBonus(amx1, args1, this, m_Instance));

            m_Amx.Register("SelectRackItem", (amx1, args1) => Scripting.Natives.SelectRackItem(amx1, args1, this, m_Instance));
            m_Amx.Register("GetSelectedRackItem", (amx1, args1) => Scripting.Natives.GetSelectedRackItem(amx1, args1, this, m_Instance));

            m_Amx.Register("GetCurrentFarmPosition", (amx1, args1) => Scripting.Natives.GetCurrentFarmPosition(amx1, args1, this, m_Instance));
            m_Amx.Register("OpenFarmPosition", (amx1, args1) => Scripting.Natives.OpenFarmPosition(amx1, args1, this, m_Instance));
            m_Amx.Register("CloseCurrentFarmPosition", (amx1, args1) => Scripting.Natives.CloseCurrentFarmPosition(amx1, args1, this, m_Instance));
            m_Amx.Register("GetFarmPositionType", (amx1, args1) => Scripting.Natives.GetFarmPositionType(amx1, args1, this, m_Instance));
            m_Amx.Register("HarvestAllFields", (amx1, args1) => Scripting.Natives.HarvestAllFields(amx1, args1, this, m_Instance));
            m_Amx.Register("HarvestFieldInCurrentPos", (amx1, args1) => Scripting.Natives.HarvestFieldInCurrentPos(amx1, args1, this, m_Instance));
            m_Amx.Register("CultivateFieldInCurrentPos", (amx1, args1) => Scripting.Natives.CultivateFieldInCurrentPos(amx1, args1, this, m_Instance));
            m_Amx.Register("WaterFieldInCurrentPos", (amx1, args1) => Scripting.Natives.WaterFieldInCurrentPos(amx1, args1, this, m_Instance));
            m_Amx.Register("HarvestGarden", (amx1, args1) => Scripting.Natives.HarvestGarden(amx1, args1, this, m_Instance));
            m_Amx.Register("WaterGarden", (amx1, args1) => Scripting.Natives.WaterGarden(amx1, args1, this, m_Instance));

            return true;
        }
    }
}
