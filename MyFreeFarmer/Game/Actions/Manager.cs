using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyFreeFarmer.Game
{
    public struct FarmAction
    {
        public Farmer farmer;
        public string Function;
        public List<object> args;
        public FarmAction(Farmer frm, string func, List<object>li)
        { 
            Function = func; args = li; farmer = frm; 
        }

    }

    public static partial class ActionManager
    {

        static public bool isBusy = false;
        static bool Active;
        static Thread thread = new Thread(new ThreadStart(Performer));
        static List<FarmAction> ActionList = new List<FarmAction>(); 

        public static void AddToPerform(FarmAction fa)
        {
            ActionList.Add(fa);
        }

        public static void Run()
        {
            Active = true;
            thread.Start();
            Log.Debug("Action Performer is now running!");
        }
        
        public static void Stop()
        {
            Active = false;
            ActionList.Clear();
            isBusy = false;
            //The actual stop happens when performer thread has ended. 
        }

        private static void Performer()
        {
            while (Active)
            {
                if(ActionList.Count> 0)
                {
                    for(int i=0; i<ActionList.Count; i++)
                    {
                        Log.Info("Now performing: " + ActionList[i].Function.ToString());
                        switch (ActionList[i].Function)
                        {
                            case "Login":
                                {
                                    Actions.Login(ActionList[i].farmer);
                                    break;
                                }
                            case "SelectRackItem":
                                {
                                    Actions.SelectRackItem(ActionList[i].farmer, (int)ActionList[i].args[0]);
                                    break;
                                }
                        }
                        while (isBusy){} //Wait until isBusy is false again
                        Log.Info("   - Done.");
                        Thread.Sleep(1000); //Wait two seconds and do the next
                    }
                }
                ActionList.Clear();
                Thread.Sleep(3000); //Wait a short time for the next action to be performed, if any
            }
            Log.Debug("Action Performer stopped.");
            return;
        }
    }
}
