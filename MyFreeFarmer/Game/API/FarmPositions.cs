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
        public static bool IsAvailable(Farmer game, int landid)
        {
            Int64 x = (Int64)game.m_JavaScript.ExecuteScript("return parseInt(farms_data['farms'][" + game.m_Info.CurrentFarm() + "][" + landid + "]['status']);");
            if (x == 1) return true;
            return false;
        }

        public static bool Open(Farmer game, int landid)
        {
            //Check for any obstructions we can assume may be there
            if (DailyLoginBonus.IsShown(game)) DailyLoginBonus.Close(game);
            if (NewsBox.IsShown(game)) NewsBox.Close(game);
            if (GlobalBox.IsShown(game)) GlobalBox.Close(game);

            game.m_Info.m_currentLand = landid;

            try
            {
                var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='farm" + game.m_Info.CurrentFarm().ToString() + "_pos" + landid.ToString() + "']"));
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
            return (Int64)game.m_JavaScript.ExecuteScript("return parseInt(farms_data['farms'][" + game.m_Info.CurrentFarm() + "][" + landid + "]['buildingid']);");
        }

        /// <summary>
        /// Make sure you always selectRackItem to choose a item before using this.
        /// </summary>
        /// <param name="game">The bot instance.</param>
        /// <param name="fieldid">The field on the farm pos to plant something.</param>
        public static void CultivateField(Farmer game, int fieldid)
        {
            game.m_JavaScript.ExecuteScript("selectMode(0, true, selected);");
            game.m_JavaScript.ExecuteScript("parent.cache_me(" + game.m_Info.m_currentLand + ", " + fieldid + ", garten_prod[" + fieldid + "], garten_kategorie[" + fieldid + "]);");
        }
        
        public static void HarvestField(Farmer game, int fieldid)
        {
            game.m_JavaScript.ExecuteScript("selectMode(1, true, selected);");
            game.m_JavaScript.ExecuteScript("parent.cache_me(" + game.m_Info.m_currentLand + ", " + fieldid + ", garten_prod[" + fieldid + "], garten_kategorie[" + fieldid + "]);");
        }

        public static void WaterField(Farmer game, int fieldid)
        {
            game.m_JavaScript.ExecuteScript("selectMode(2, true, selected);");
            game.m_JavaScript.ExecuteScript("parent.cache_me(" + game.m_Info.m_currentLand + ", " + fieldid + ", garten_prod[" + fieldid + "], garten_kategorie[" + fieldid + "]);");
        }

        public static void ClearField(Farmer game, int fieldid)
        {
            Console.WriteLine("raeumeFeld(" + game.m_Info.m_currentLand + ", " + fieldid + ");");
            game.m_JavaScript.ExecuteScript("raeumeFeld(\" + game.m_Info.m_currentLand + \", \" + fieldid + \");");
        }


        //Whole

        public static void HarvestGarden(Farmer game, int pos)
        {
            if (!game.m_Info.HasPremium()) return;
            game.m_JavaScript.ExecuteScript("cropGarden(" + pos + ");");
            if (GlobalBox.IsShown(game)) GlobalBox.Close(game);
        }

        public static void WaterGarden(Farmer game, int pos)
        {
            if (!game.m_Info.HasPremium()) return;
            game.m_JavaScript.ExecuteScript("waterGarden(" + pos + ");");
            if (GlobalBox.IsShown(game)) GlobalBox.Close(game);
        }
    }
}
