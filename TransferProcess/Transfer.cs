using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TransferProcess
{
    public partial class Transfer : Form
    {
        private List<TransferTask> taskList = new List<TransferTask>();
        private Timer taskTM = new Timer();
        private Guid currentTaskID = Guid.Empty;

        public Transfer()
        {
            InitializeComponent();

            MessageBox.Show("OK");

            taskTM.Interval = 100;
            taskTM.Tick += new EventHandler(TaskTM_Tick);
            taskTM.Start();
        }

        void TaskTM_Tick(object sender, EventArgs e)
        {
            if (currentTaskID == Guid.Empty) return;
            TransferTask currentTask = GetTaskByID(currentTaskID);
            if (currentTask.Status == TaskStatus.Completed || currentTask.Status == TaskStatus.Canceled || currentTask.Status == TaskStatus.Loading) return;
            TransferNext();
        }

        /// <summary>
        /// 根据GUID获取任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        private TransferTask GetTaskByID(Guid taskID)
        {
            foreach (TransferTask task in taskList)
            {
                if (task.ID.Equals(taskID))
                {
                    return task;
                }
            }
            return null;
        }

        /// <summary>
        /// 传输下一个任务
        /// </summary>
        private void TransferNext() 
        {
            foreach (TransferTask task in taskList) 
            {
                if (task.Status == TaskStatus.Pending) 
                {
                    currentTaskID = task.ID;
                    BeginTransfer(task);
                }
            }
        }

        private void AddTransferTask(TransferTask task)
        {
            if (taskList.Count == 0)
            {
                currentTaskID = task.ID;
            }
            taskList.Add(task);
        }

        /// <summary>
        /// 传输入口函数
        /// </summary>
        /// <param name="task"></param>
        private void BeginTransfer(TransferTask task) 
        {
            System.Threading.Thread td = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Excute));
            td.Start(task); 
        }

        private void Excute(object taskObj) 
        {
            TransferTask task = taskObj as TransferTask;
            switch (task.Category)
            {
                case TaskCategory.Files:
                    {
                        TransferHelper.CopyFiles(task);
                        break;
                    }
                case TaskCategory.Features:
                    {
                        TransferHelper.CopyFeatures(task);
                        break;
                    }
                case TaskCategory.Raster:
                    {
                        TransferHelper.CopyRaster(task);
                        break;
                    }
            }

            string s = task.ID.ToString();
            Int32 id = 1;
            Int32 WM_COPYDATA = 0x004A;
            COPYDATASTRUCT cd = new COPYDATASTRUCT();
            cd.dwData = (IntPtr)id;
            cd.lpData = s;
            cd.cbData = s.Length;
            SendMessage((int)FindWindow(null, "MainForm"), WM_COPYDATA, 0, ref cd);
        } 

        //------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------消息-------------------------------------------------------
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

        /// <summary>
        /// 任务监听
        /// </summary>
        /// <param name="m"></param>
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
                        Guid taskId = Guid.Parse(pms[0]);
                        string sourceFileName = pms[1];
                        string destFileName = pms[2];
                        TaskType taskType = (TaskType)Enum.Parse(typeof(TaskType), pms[3]);
                        RenameMode taskRenameMode = (RenameMode)Enum.Parse(typeof(RenameMode), pms[4]);
                        TaskCategory taskCategory = (TaskCategory)Enum.Parse(typeof(TaskCategory), pms[5]);
                        TransferTask task = new TransferTask(taskId, sourceFileName, destFileName, taskType, taskRenameMode, taskCategory);
                        AddTransferTask(task);
                        break;
                    }
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RasterReadHelper featReadHelper = new RasterReadHelper(@"E:\Desktop(2014.10.10)\Test\BMP.jpg");
            featReadHelper.Delete("BMP.bmp");
        }
        //------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------
    }
}
