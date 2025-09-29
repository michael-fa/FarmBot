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
        public int m_Pos = 0; //Field/Factory/Animals
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

        public int GetCurrentPosition()
        {
            return m_Pos;
        }

        public void SetCurrentPosition(int _i)
        {
            Console.WriteLine("&&&/$%§%§$%$debug called");
            m_Pos = _i;
        }

        public Int64 GetCurrentRack()
        {
            return (Int32)m_Game.m_JavaScript.ExecuteScript("return racksort");
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
