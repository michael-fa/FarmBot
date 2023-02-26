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

        public bool m_IsBusy = false;

        public GameInfo(Farmer game, int pServer, string pUser, string pPass)
        {
            m_Game    = game;
            m_loginServer = pServer;   
            m_loginUser = pUser;   
            m_loginPassword = pPass;   
        }

        public bool m_LoggedIn = false;

        public Int64 GetCurrentFarm()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return farm_number");
        }
        public Int64 GetFarmAmount()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return farms_data[\"count\"]");
        }

        public int m_currentLand = 0; //use buildinginfo and its available index? idk
        public Int64 GetCurrentRack()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return racksort");
        }

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
