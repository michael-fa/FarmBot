using MyFreeFarmer.Game.API;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game.API
{
    public static class FarmPositions
    {
        public static bool Open(Farmer game, int landid)
        {
            //Check for any obstructions we can assume may be there
            if (DailyLoginBonus.IsShown(game)) DailyLoginBonus.Close(game);
            if (NewsBox.IsShown(game)) NewsBox.Close(game);
            if (GlobalBox.IsShown(game)) GlobalBox.Close(game);

            game.m_Info.m_currentLand = landid;

            try
            {
                var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='farm" + game.m_Info.GetCurrentFarm().ToString() + "_pos" + landid.ToString() + "']"));
                if (GetType(game, landid) == 0) return false;
                if (x != null && x.Displayed) x.Click();
            }
            catch (Exception ex){
                Log.Exception(ex);
                game.m_Info.m_currentLand = 0; }
            finally {  }

            //Safety check: wait until the "container" of each position is shown.
            if (GetType(game, landid) == 1)
            {
                game.m_JavaScript.ExecuteScript("specialZoneFieldHandler(" + landid + ");");
            }
            else
            {
                game.m_JavaScript.ExecuteScript("initLocation(" + landid + ");");
            }


            Log.Debug(" - m.Info CurrentLand is = " + game.m_Info.m_currentLand);
            return true;
        }


        public static bool CloseCurrent(Farmer game)
        {
            if (DailyLoginBonus.IsShown(game)) DailyLoginBonus.Close(game);
            if (NewsBox.IsShown(game)) NewsBox.Close(game);
            if (GlobalBox.IsShown(game)) GlobalBox.Close(game);

            if (game.m_Info.m_currentLand == 0) return false;

            game.m_JavaScript.ExecuteScript("showMain();");

            game.m_Info.m_currentLand = 0;
            Log.Debug("CURLAND:" + game.m_Info.m_currentLand);
            return true;
        }

        public static Int64 GetType(Farmer game, int landid) 
        {
            return (Int64)game.m_JavaScript.ExecuteScript("return parseInt(farms_data['farms'][" +game.m_Info.GetCurrentFarm() + "][" + landid + "]['buildingid']);");
        }

        /*public static bool CultivateField(int fieldid, int item_id)
        {
            
        }

        public static bool HarvestField(int fieldid)
        {

        }

        public static bool WaterField(int fieldid)
        {

        }*/

            /* TO BE WORKED ON LATER.
            public static bool ClearField(int fieldid)
            {

            }*/
        }
    }
