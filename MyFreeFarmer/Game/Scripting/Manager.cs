using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


 //Note
//Unload/Reload should check for
//scriptfile hash Differences because of name problems
namespace MyFreeFarmer.Game.Scripting
{
    static class Manager
    {
        private static bool m_Inited;
        public static List<Script>m_Scripts= new List<Script>();
        public static Farmer m_Farmer = null!;
        public static AMXPublic m_InitScript = null!;
    
        public static void LoadFiles()
        {
            Script scr = null!;
            foreach (string x in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Scripts\"))
            {
                if (!x.Contains(".amx")) continue;
                Log.Debug("(Script) Trying to load " + x + "..");
                try
                {
                    scr = new Script(m_Farmer, x);

                }
                catch (Exception ex) { Log.Exception(ex);
                    Log.Debug(".. failed!");
                }

                m_Scripts.Add(scr!);

                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(x))
                    {
                        scr.m_Hash = md5.ComputeHash(stream);
                    }
                }

                Log.Debug(".. generated script file hash: " + BitConverter.ToString(scr.m_Hash).Replace("-", "").ToLowerInvariant() + "!");
            }

            if(m_InitScript == null)
            {
                Log.Error("No script with OnInit public found! Need at least one (default) script loaded.");
                m_Farmer.Stop();
                return;
            }
        }

        public static void UnloadScript(Script script)
        {
            if(script.m_Amx != null)
            {
                var p = script.m_Amx.FindPublic("OnUnload");
                if (p != null) p.Execute();

                script.m_Amx.Dispose();
                script.m_Amx = null!;
            }
            m_Scripts.Remove(script);
        }

        public static void RunInit()
        {
            if (m_Inited) return;
            m_InitScript.Execute();
            m_Inited = true;
        }
        
    }
}
