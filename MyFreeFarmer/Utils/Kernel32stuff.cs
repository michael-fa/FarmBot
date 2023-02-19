using System.Runtime.InteropServices;
using System.Text;

namespace telepawn_cli
{
    public static class Kernel32
    {
        [DllImport("Kernel32")]
        static public extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        public delegate bool EventHandler(CtrlType sig);
        public static EventHandler? m_Handler;










        public enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        public static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    //StopSafely();
                    return false;
                default:
                    return false;
            }
        }


        public static void Init()
        {
            Console.OutputEncoding = Encoding.Unicode;
            m_Handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(m_Handler, true);
            return;
        }
    }

}
