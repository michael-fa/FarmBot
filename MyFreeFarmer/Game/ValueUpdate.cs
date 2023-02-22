using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game
{
    public static class ValueUpdate
    {
        public static Thread m_Thread = new Thread(new ThreadStart(UpdateLoop));
        private static Farmer m_Game = null!;
        public static bool m_Active = false;

        public static void Start(Farmer game)
        {
            Log.Debug("ValueUpdater is now running.");
            m_Active = true;
            m_Game = game;
            m_Thread.Start();
        }

        public static void Stop()
        {
            m_Game = null!;
            m_Active = false;
        }

        /// <summary>
        /// This method runs in a 5 second sleeping loop and updates needed values, removing the need to find elements for some.
        /// WARNING! This should only be called as a chain reaction when Login action has been triggered!
        /// </summary>
        /// <param name="game"></param>
        public static void UpdateLoop()
        {
            //MAY BE dangerous tho, if something is returned as null or simply completely wrong (not even an old value) we should skip that
            while(m_Active)
            {
                //Retr the basic user stats
                m_Game.m_Info.m_Level = Convert.ToInt32(Utils.FindElementIfExists(m_Game.m_Driver, By.XPath(".//*[@id='levelnum']")).Text);
                m_Game.m_Info.m_Points = Convert.ToInt32(Utils.FindElementIfExists(m_Game.m_Driver, By.XPath(".//*[@id='pkt']")).Text.Replace(".", ""));
                m_Game.m_Info.m_Money = Convert.ToInt32(Utils.FindElementIfExists(m_Game.m_Driver, By.XPath(".//*[@id='bar']")).Text.Replace(".", "").Replace(",", "").Replace(" kT", ""));
                m_Game.m_Info.m_Coins = Convert.ToInt32(Utils.FindElementIfExists(m_Game.m_Driver, By.XPath(".//*[@id='coins']")).Text.Replace(".", ""));
                Log.Debug("ValueUpdate has been performed.");
                Thread.Sleep(5000); //Update every 5 seconds
            }

            Log.Debug("ValueUpdater stopped.");
        }
    }
}
