using OpenQA.Selenium.DevTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Int64 CurrentFarm()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return farm_number");
        }

        public Int64 FarmCount()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return farms_data[\"count\"]");
        }

        public int AvailablePositionCount(int farmid)
        {
            int ct = 0; 
            
            //We always have the first pos.
            for(int i=1 ; i<=6; i++)
            {
                if ((Int64)m_Game.m_JavaScript.ExecuteScript("return parseInt(farms_data.farms[" + farmid + "][" + i + "][\"buildingid\"]);") == 0) continue;
                ct++;
            }
            return ct;
        }

        //Dumb because we assume we controlled everything from the script, yet if we wanna take control for 2 secs or so the switching of positions won't be detected/this value is not correct anymore.
        public int m_currentLand = 0; //use buildinginfo and its available index? idk
        
        public Int64 GetCurrentRack()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return racksort");
        }

        public Int64 GetLevel()
        {
            return (Int64)m_Game.m_JavaScript.ExecuteScript("return parseInt(currentuserlevel);");
        }

        public int GetPoints()
        {
            return Convert.ToInt32(m_Game.m_JavaScript.ExecuteScript("return user_points"));
        }

        public string GetMoney()
        {
            //Note: This is not the actual money
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
