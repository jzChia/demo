using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TransferProcess;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TransferProcessManager
{
    public partial class TransferManagerControl : UserControl
    {
        private List<TransferTask> downloadTasks = new List<TransferTask>();
        private List<TransferTask> uploadTasks = new List<TransferTask>();

        private ProcessStartInfo downloadPSI;
        private ProcessStartInfo uploadPSI;
        private Process downloadProcess;
        private Process uploadProcess;
        private string server = @"\\192.168.1.116";
        private string user = "Lrx";
        private string password = "123";

        [DllImport("user32.dll")]
        private static extern long SendMessage(Int32 hwnd, Int32 msg, Int32 hwndFrom, ref COPYDATASTRUCT cd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const int WM_COPYDATA = 0x004A;

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        public TransferManagerControl()
        {
            InitializeComponent();
        }

        public void Intialize() 
        {
            ConnectLan(server, user, password);

            downloadPSI = new ProcessStartInfo();
            downloadPSI.FileName = Application.StartupPath + "\\TransferProcess.exe";
            downloadProcess = new Process();
            downloadProcess.StartInfo = downloadPSI;
            downloadProcess.Start();

            //uploadPSI = new ProcessStartInfo();
            //uploadPSI.FileName = Application.StartupPath + "\\Transfer.exe";
            //uploadProcess = new Process();
            //uploadProcess.StartInfo = uploadPSI;
            //uploadProcess.Start();
        }

        public void AddDownloadTask(TransferTask task) 
        {
            downloadTasks.Add(task);
            string taskInfo = string.Format("{0}#{1}#{2}#{3}#{4}#{5}", new string[] { task.ID.ToString(), task.SourceFileName, task.DestFileName, task.Type.ToString(), task.RenameMode.ToString(), task.Category.ToString() }).Replace(' ', '$');
            Int32 id = 1;
            Int32 WM_COPYDATA = 0x004A;
            COPYDATASTRUCT cd = new COPYDATASTRUCT();
            cd.dwData = (IntPtr)id;
            cd.lpData = taskInfo;
            cd.cbData = taskInfo.Length;
            SendMessage((int)FindWindow(null, "Transfer"), WM_COPYDATA, 0, ref cd);
        }



        /// <summary>
        /// 连接远程服务器
        /// </summary>
        /// <param name="p_Path"></param>
        /// <param name="p_UserName"></param>
        /// <param name="p_PassWord"></param>
        /// <returns></returns>
        public static string ConnectLan(string p_Path, string p_UserName, string p_PassWord)
        {
            System.Diagnostics.Process _Process = new System.Diagnostics.Process();
            _Process.StartInfo.FileName = "cmd.exe";
            _Process.StartInfo.UseShellExecute = false;
            _Process.StartInfo.RedirectStandardInput = true;
            _Process.StartInfo.RedirectStandardOutput = true;
            _Process.StartInfo.CreateNoWindow = true;
            _Process.Start();
            _Process.StandardInput.WriteLine("net use " + p_Path + " " + p_PassWord + " /user:" + p_UserName);
            _Process.StandardInput.WriteLine("exit");
            _Process.WaitForExit();
            string _ReturnText = _Process.StandardOutput.ReadToEnd();
            _Process.Close();
            return _ReturnText;
        }
    }
}
