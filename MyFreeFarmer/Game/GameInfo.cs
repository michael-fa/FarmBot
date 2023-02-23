using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game
{


    public struct GameInfo
    {
        public int m_loginServer;
        public string m_loginUser;
        public string m_loginPassword;
        private Farmer m_Game;

        public GameInfo(Farmer game, int pServer, string pUser, string pPass)
        {
            m_Game    = game;
            m_loginServer = pServer;   
            m_loginUser = pUser;   
            m_loginPassword = pPass;   
        }

        //About it's state
        public bool m_LoggedIn = false;

        public int GetCurrentFarm()
        {
            return (int)m_Game.m_JavaScript.ExecuteScript("return farm");
        }
        public int m_currentLand = 0;
        public int GetCurrentRack()
        {
            return (int)m_Game.m_JavaScript.ExecuteScript("return racksort");
        }

        //On login retrieved account data
        //public int m_Level;
        public int GetPoints()
        {
            return Convert.ToInt32(m_Game.m_JavaScript.ExecuteScript("return user_points"));
        }
        public string GetMoney()
        {
            return Convert.ToString(m_Game.m_JavaScript.ExecuteScript("return user_bar"))!;
        }
        public string GetCoins()
        {
            return Convert.ToString(m_Game.m_JavaScript.ExecuteScript("return user_coins"))!;
        }

        public bool HasPremium()
        {
            //Kinda untested if this will always be accurate.
            string x = (string)m_Game.m_JavaScript.ExecuteScript("return user_premium_bis");
            if(x != null && x.Equals("01.01.70, 01:00 Uhr"))return false;
            return true;
        }
    }
}
