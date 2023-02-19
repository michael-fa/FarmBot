using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyFreeFarmer.Game
{
    public struct FarmAction
    {
        public Farmer farmer;
        public string Function;
        public object?[] Arguments;
        public FarmAction(Farmer frm, string func, object?[] arg)
        { Function = func; Arguments = arg; farmer = frm; }

    }

    internal static partial class ActionManager
    {

        static public bool isBusy = false;
        static bool Active;
        static Thread thread = thread = new Thread(new ThreadStart(Performer));
        static List<FarmAction> ActionList = new List<FarmAction>(); 

        public static void AddToPerform(FarmAction fa)
        {
            ActionList.Add(fa);
        }

        public static void Run()
        {
            Active = true;
            thread.Start();
            Log.Info("Action Performer is now running!");
        }

        public static void Stop()
        {
            Active = false;
            Log.Info("Action Performer has been stopped!");
        }

        private static void Performer()
        {
            while (Active)
            {
                if(ActionList.Count> 0)
                {
                    foreach(FarmAction fa in ActionList)
                    {
                        Log.Info("Action Performer is now performing: " + fa.Function.ToString());
                        switch (fa.Function)
                        {
                            case "Login":
                                {
                                    Actions.Login(fa.farmer);
                                    break;
                                }
                        }
                        while (isBusy){} //Wait until isBusy is false again
                        Log.Info("   - Done.");
                        Thread.Sleep(2000); //Wait two seconds and do the next
                    }
                    ActionList.Clear();
                }
                Thread.Sleep(5000); //Wait a short time for the next action to be performed, if any
            }
            return;
        }
    }
}
