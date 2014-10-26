using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TransferProcess
{
    public class MessageHelper
    {
        public static int WM_COPYDATA = 0x004A;

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        [DllImport("user32.dll")]
        private static extern long SendMessage(Int32 hwnd, Int32 msg, Int32 hwndFrom, ref COPYDATASTRUCT cd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public void SendMessage(string message, string lpClassName, string lpWindowName) 
        {
            Int32 id = 1;
            COPYDATASTRUCT cd = new COPYDATASTRUCT();
            cd.dwData = (IntPtr)id;
            cd.lpData = message;
            cd.cbData = message.Length;
            SendMessage((int)FindWindow(lpClassName, lpWindowName), WM_COPYDATA, 0, ref cd);
        }
    }
}
