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

            int _type = 0;
            try
            {
                _type = GetType(game, landid);
                if (_type == 0) return false;
                var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='farm" + game.m_Info.m_currentFarm.ToString() + "_pos" + landid.ToString() + "']"));
                if (x != null && x.Displayed) x.Click();
            }
            catch (Exception ex){
                Log.Exception(ex);
                game.m_Info.m_currentLand = 0; }
            finally {  }

            //Safety check: wait until the "container" of each position is shown.
            if (_type == 1)
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

        public static int GetType(Farmer game, int landid) 
        {
            //farm1_pos1 -> farm_pos_tt_name

            game.m_JavaScript.ExecuteScript("farmHoverPosition(1, " + game.m_Info.m_currentFarm.ToString() + ", " + landid.ToString() + ");");
            IDictionary <string, int> PosTypes = new Dictionary<string, int>();
            PosTypes.Add("Acker", 1);
            PosTypes.Add("Hühnerstall", 2);
            PosTypes.Add("Kuhstall", 3);
            PosTypes.Add("Schafskoppel", 4);
            PosTypes.Add("Imkerei", 5);
            PosTypes.Add("Bauernclub", 6);
            PosTypes.Add("Mayo-Küche", 7);
            PosTypes.Add("Käserei", 8);
            PosTypes.Add("Wollspinnerei", 9);
            PosTypes.Add("Bonbonüche", 10);
            PosTypes.Add("Fischzucht", 11);
            PosTypes.Add("Ziegenfarm", 12);
            PosTypes.Add("Ölpresse", 13);
            PosTypes.Add("Spezial manufaktur", 14); //this one may be wrong
            PosTypes.Add("Angorastall", 15);
            PosTypes.Add("Strickerei", 16);
            PosTypes.Add("Zimmerei", 17);
            PosTypes.Add("Ponyhof", 18);
            PosTypes.Add("Fahrzeughalle", 19);
            PosTypes.Add("Biosprit-Anlage", 20);
            PosTypes.Add("Teeverfeinerung", 21);
            PosTypes.Add("Bergstation", 22);
            PosTypes.Add("Sushibar", 23);

            var x = Utils.FindElementIfExists(game.m_Driver, By.XPath(".//*[@id='farm" + game.m_Info.m_currentFarm.ToString() + "_pos" + landid.ToString() + "_tt']"));
            if (x != null && x.Displayed)
            {
                x = x.FindElement(By.ClassName("farm_pos_tt_name"));
                Console.WriteLine(PosTypes[x.Text]);
                if (x != null) return PosTypes[x.Text];
            }
            return 0;
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
