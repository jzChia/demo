using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Transfer
{
    public partial class TransferForm : Form
    {
        private string sourceFileName;
        private string destFileName;
        private TaskType type;
        private RenameMode renameMode;
        private TaskCategory category;
        private Guid TaskId;

        private const int WM_COPYDATA = 0x004A;

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

        public TransferForm()
        {
            InitializeComponent();
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    {
                        COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                        Type mytype = mystr.GetType();
                        mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                        string taskInfo = mystr.lpData.Substring(0, mystr.cbData);
                        taskInfo = taskInfo.Replace('$', ' ');
                        string[] pms = taskInfo.Split('#');
                        TaskId = Guid.Parse(pms[0]);
                        sourceFileName = pms[1];
                        destFileName = pms[2];
                        type = (TaskType)Enum.Parse(typeof(TaskType), pms[3]);
                        renameMode = (RenameMode)Enum.Parse(typeof(RenameMode), pms[4]);
                        category = (TaskCategory)Enum.Parse(typeof(TaskCategory), pms[5]);

                        System.Threading.Thread td = new System.Threading.Thread(Excute);
                        td.Start();
                        break;
                    }
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        public void Excute()
        {
            MessageBox.Show("OK");

            switch (category)
            {
                case TaskCategory.Files:
                    {
                        TransferHelper.CopyFiles(sourceFileName, destFileName, renameMode);
                        break;
                    }
                case TaskCategory.Features:
                    {
                        TransferHelper.CopyFeatures(sourceFileName, destFileName, renameMode);
                        break;
                    }
                case TaskCategory.Raster:
                    {
                        TransferHelper.CopyRaster(sourceFileName, destFileName, renameMode);
                        break;
                    }
            }


            string s = TaskId.ToString();
            Int32 id = 1;
            Int32 WM_COPYDATA = 0x004A;
            COPYDATASTRUCT cd = new COPYDATASTRUCT();
            cd.dwData = (IntPtr)id;
            cd.lpData = s;
            cd.cbData = s.Length;
            SendMessage((int)FindWindow(null, "MainForm"), WM_COPYDATA, 0, ref cd);
        }
    }

    public enum RenameMode
    {
        Overwrite,
        Accumulate
    }

    public enum TaskType
    {
        Upload,
        Download
    }

    public enum TaskCategory
    {
        Features,
        Raster,
        Files,
        Folder
    }
}
