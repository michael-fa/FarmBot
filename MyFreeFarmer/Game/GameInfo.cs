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

        public GameInfo(int pServer, string pUser, string pPass)
        {
            m_loginServer = pServer;   
            m_loginUser = pUser;   
            m_loginPassword = pPass;   
        }

        //About it's state
        public bool m_LoggedIn = false;
        public int m_currentFarm = 1;
        public int m_currentBuilding = 0;
        public int m_currentStorage = 1;

        //On login retrieved account data
        public int m_Level;
        public int m_Points;
        public int m_Money;
        public int m_Coins;
    }
}
