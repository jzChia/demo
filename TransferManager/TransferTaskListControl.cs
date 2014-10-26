using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors.Repository;
using System.IO;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace TransferManager
{
    public partial class TransferTaskListControl : DevExpress.XtraEditors.XtraUserControl
    {
        private List<TransferTask> Tasks = new List<TransferTask>();
        private Timer refreshDSTM = new Timer();
        public Guid CurrentTaskID = Guid.Empty;
        private ProcessStartInfo info;
        private Process process;
        private CaptureHelper capHelper;
        private System.Threading.Thread td;
        private TaskViewType viewType = TaskViewType.PendingOrLoadingTasksOfDownload;
        private string server = @"\\Server-PC";
        private string user = "Lrx";
        private string password = "123";

        #region 消息
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
        #endregion

        public TransferTaskListControl()
        {
            InitializeComponent();          
        }

        public void Initialize() 
        {
            ConnectLan(server, user, password);
            info = new ProcessStartInfo();
            info.FileName = Application.StartupPath + "\\Transfer.exe";
            process = new Process();
            process.StartInfo = info;
            if (process.Start())
            {
                capHelper = new CaptureHelper(process.Id);
                capHelper.Initialize();
            }

            RefreshDataSource();
            this.refreshDSTM.Interval = 100;
            this.refreshDSTM.Tick += new EventHandler(refreshDSTM_Tick);
            this.refreshDSTM.Start();
        }

        #region 视图
        public void ChangeTaskView(TaskViewType viewType) 
        {
            this.viewType = viewType;
            RefreshDataSource();
        }

        private void RefreshDataSource() 
        {
            switch (viewType)
            {
                case TaskViewType.AllTasks:
                    {
                        this.gridControl1.DataSource = Tasks;
                        this.gridView1.ViewCaption = "所有任务";
                        break;
                    }
                case TaskViewType.PendingOrLoadingTasksOfDownload:
                    {
                        this.gridControl1.DataSource = PendingTasksOfDownload;
                        this.gridView1.ViewCaption = "待下载及正在下载的任务";
                        break;
                    }
                case TaskViewType.PendingOrLoadingTasksOfUpload:
                    {
                        this.gridControl1.DataSource = PendingTasksOfUpload;
                        this.gridView1.ViewCaption = "待下载及正在上传的任务";
                        break;
                    }
                case TaskViewType.CompletedTasksOfDownload:
                    {
                        this.gridControl1.DataSource = CompletedTasksOfDownload;
                        this.gridView1.ViewCaption = "已完成的下载任务";
                        break;
                    }
                case TaskViewType.CompletedTasksOfUpload:
                    {
                        this.gridControl1.DataSource = CompletedTasksOfUpload;
                        this.gridView1.ViewCaption = "已完成的上传任务";
                        break;
                    }
            }
            this.gridControl1.RefreshDataSource();
        }

        public List<TransferTask> PendingTasksOfDownload
        {
            get 
            {
                return Tasks.FindAll(IsPendingOrLoadingOfDownload);
            }
        }

        private bool IsPendingOrLoadingOfDownload(TransferTask task)
        {
            if(task.Status == TaskStatus.Loading || task.Status == TaskStatus.Pending)
            {
                if(task.Type==TaskType.Download) return true;
            }
            return false;
        }

        public List<TransferTask> PendingTasksOfUpload 
        {
            get 
            {
                return Tasks.FindAll(IsPendingOrLoadingOfUpload);
            }
        }

        private bool IsPendingOrLoadingOfUpload(TransferTask task)
        {
            if (task.Status == TaskStatus.Loading || task.Status == TaskStatus.Pending)
            {
                if (task.Type == TaskType.Upload) return true;
            }
            return false;
        }

        public List<TransferTask> CompletedTasksOfDownload 
        {
            get 
            {
                return Tasks.FindAll(IsCompletedOfDownload);
            }
        }

        private bool IsCompletedOfDownload(TransferTask task)
        {
            if (task.Status == TaskStatus.Completed && task.Type == TaskType.Download)
            {
                return true;
            }
            return false;
        }

        public List<TransferTask> CompletedTasksOfUpload
        {
            get
            {
                return Tasks.FindAll(IsCompletedOfUpload);
            }
        }

        private bool IsCompletedOfUpload(TransferTask task)
        {
            if (task.Status == TaskStatus.Completed && task.Type == TaskType.Upload)
            {
                return true;
            }
            return false;
        }
        #endregion

        void refreshDSTM_Tick(object sender, EventArgs e)
        {
            RefreshDataSource();

            TransferTask currentTask = GetTaskByID(CurrentTaskID);
            if (currentTask != null) 
            {
                if (currentTask.Status == TaskStatus.Pending) 
                {
                    Transfer(currentTask);
                }
                else if (currentTask.Status == TaskStatus.Completed || currentTask.Status == TaskStatus.Canceled)
                {
                    TransferNext();
                }
            }
        }

        public void AddTask(TransferTask task) 
        {
            this.Tasks.Add(task);
            if (CurrentTaskID.Equals(Guid.Empty)) CurrentTaskID = task.ID;
        }

        private void TransferNext() 
        {
            foreach (TransferTask task in Tasks) 
            {
                if (task.Status == TaskStatus.Pending) 
                {
                    Transfer(task);
                    break;
                }
            }
        }

        private void Transfer(TransferTask task)
        {
            task.Status = TaskStatus.Loading;
            this.CurrentTaskID = task.ID;
            task.Size = CalculateSize(task);
            td = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Excute));
            td.Start(task);

            string taskInfo = string.Format("{0}#{1}#{2}#{3}#{4}#{5}", new string[] { task.ID.ToString(), task.SourceFileName, task.DestFileName, task.Type.ToString(), task.RenameMode.ToString(), task.Category.ToString() }).Replace(' ', '$');
            Int32 id = 1;
            Int32 WM_COPYDATA = 0x004A;
            COPYDATASTRUCT cd = new COPYDATASTRUCT();
            cd.dwData = (IntPtr)id;
            cd.lpData = taskInfo;
            cd.cbData = taskInfo.Length;
            SendMessage((int)FindWindow(null, "TransferForm"), WM_COPYDATA, 0, ref cd);
        }

        private int ccc = 0;

        public void Excute(object obj)
        {
            TransferTask task = (TransferTask)obj;
            task.NetSize = 0;
            capHelper.Reset();
            while (task.Status == TaskStatus.Loading)
            {
                ccc++;
                if (ccc == 10)
                {
                    if (task.Type == TaskType.Upload)
                    {
                        task.Speed = capHelper.ProcInfo.NetSendBytes;
                    }
                    else
                    {
                        task.Speed = capHelper.ProcInfo.NetRecvBytes;
                    }
                    
                    task.ElapsedTime += 1;
                    if (task.Speed != 0)
                        task.LeftTime = (task.Size - task.Progress) / (task.Speed / 1024.0);
                    else task.LeftTime = -1;

                    ccc = 0;
                }

                double tt = 0;
                if (task.Type == TaskType.Upload)
                {
                    tt = capHelper.ProcInfo.AccNetSendBytes;
                }
                else
                {
                    tt = capHelper.ProcInfo.AccNetRecvBytes;
                }

                task.NetSize = tt / 1024.0;

                if (task.NetSize >= task.Size * 0.99) task.Progress = (int)(task.Size * 0.99);
                else task.Progress = (int)(task.NetSize);
                task.Name = System.IO.Path.GetFileNameWithoutExtension(task.SourceFileName);

                task.Name = task.NetSize.ToString() + " / " + task.Size.ToString();

                capHelper.RefershInfo();
            }        
        }

        public void CompleteTask(TransferTask currentTask)
        {
            currentTask.Progress = currentTask.Size;
            currentTask.Speed = 0;
            currentTask.LeftTime = 0;
            currentTask.Status = TaskStatus.Completed;
            RefreshDataSource();
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private int CalculateSize(TransferTask task) 
        {
            if (task.Category == TaskCategory.Files)  
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(task.SourceFileName);
                return (int)(fileInfo.Length / 1024);
            }
            else if (task.Category == TaskCategory.Features)
            {
                DirectoryInfo dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(task.SourceFileName));
                string nickName = System.IO.Path.GetFileNameWithoutExtension(task.SourceFileName);
                FileInfo[] fs = dir.GetFiles(string.Format("*{0}*", nickName));
                int length = 0;
                foreach (FileInfo f in fs)
                {
                    length += (int)f.Length;
                }

                if (task.DestFileName.ToLower().Contains(".gdb"))
                {
                    if (task.SourceFileName.ToLower().Contains(".gdb")) 
                    {

                    }
                    else
                    {
                        return length / 1024 + 1150;
                    }
                }
                else
                {
                    if (task.SourceFileName.ToLower().Contains(".gdb"))
                    {

                    }
                    else
                    {
                        return length / 1024;
                    }

                }
            }
            else if (task.Category == TaskCategory.Raster) 
            {
                if (task.DestFileName.ToLower().Contains(".gdb"))
                {
                    if (task.SourceFileName.ToLower().Contains(".gdb"))
                    {
                        IWorkspaceFactory inputWsF = null;
                        IWorkspace inputWs = null;
                        IRasterWorkspace inputRstWs = null;
                        IRasterDataset2 inputRstDs = null;
                        IRaster inputRaster = null;
                        string inputDirectory = string.Empty;
                        string inputRstName = string.Empty;

                        string input = task.SourceFileName.Trim();
                        if (input.ToLower().Contains(".gdb"))
                        {
                            inputWsF = new FileGDBWorkspaceFactoryClass();
                            inputDirectory = input.Substring(0, input.IndexOf(".gdb") + 4);
                        }
                        else
                        {
                            inputWsF = new RasterWorkspaceFactoryClass();
                            inputDirectory = System.IO.Path.GetDirectoryName(input);
                        }
                        inputRstName = System.IO.Path.GetFileName(input);
                        inputWs = inputWsF.OpenFromFile(inputDirectory, 0);
                        inputRstWs = inputWs as IRasterWorkspace;
                        inputRstDs = inputRstWs.OpenRasterDataset(inputRstName) as IRasterDataset2;
                        inputRaster = inputRstDs.CreateDefaultRaster();

                        IRawBlocks inputRawBlocks = (IRawBlocks)inputRstDs;
                        IRasterInfo inputRstInfo = inputRawBlocks.RasterInfo;

                        IRasterProps in_rasterProps = (IRasterProps)inputRaster;
                        int Height = in_rasterProps.Height;
                        int Width = in_rasterProps.Width;
                        rstPixelType in_rstPT = in_rasterProps.PixelType;
                        int BandsCount = inputRstInfo.BandCount;
                        Dictionary<rstPixelType, int> DictPT = new Dictionary<rstPixelType, int>();
                        DictPT.Clear();
                        DictPT.Add(rstPixelType.PT_DOUBLE, 64);
                        DictPT.Add(rstPixelType.PT_FLOAT, 32);
                        DictPT.Add(rstPixelType.PT_LONG, 32);
                        DictPT.Add(rstPixelType.PT_SHORT, 32);
                        DictPT.Add(rstPixelType.PT_UCHAR, 8);
                        DictPT.Add(rstPixelType.PT_ULONG, 32);
                        DictPT.Add(rstPixelType.PT_USHORT, 32);
                        DictPT.Add(rstPixelType.PT_CHAR, 8);

                        int Depth = 32;
                        DictPT.TryGetValue(in_rasterProps.PixelType, out Depth);

                        return (int)(1.0 * Height * Width * BandsCount * Depth / 8.0 / 1024);
                    }
                    else
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(task.SourceFileName);
                        return (int)(fileInfo.Length / 1024 + 1150);
                    }
                }
                else
                {
                    if (task.SourceFileName.ToLower().Contains(".gdb"))
                    {

                    }
                    else
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(task.SourceFileName);
                        return (int)(fileInfo.Length / 1024);
                    }

                }

            }
            return task.Size;
        }

        public TransferTask GetTaskByID(Guid taskID) 
        {
            foreach (TransferTask task in Tasks) 
            {
                if (task.ID.Equals(taskID)) 
                {
                    return task;
                }
            }
            return null;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName.Equals("Speed"))
            {
                TransferTask task = GetTaskByID(new Guid(gridView1.GetRowCellValue(e.RowHandle, "ID").ToString()));
                switch(task.Status)
                {
                    case TaskStatus.Completed: 
                        {
                            e.DisplayText = "已完成";
                            break;
                        }
                    case TaskStatus.Canceled: 
                        {
                            e.DisplayText = "已删除";
                            break;
                        }
                    case TaskStatus.Loading: 
                        {
                            int value = Convert.ToInt32(e.CellValue);
                            if (value / 1024 == 0) e.DisplayText = string.Format("{0} B/s", value);
                            else if (value / (1024 * 1024) == 0) e.DisplayText = string.Format("{0:F2} K/s", value / 1024.0);
                            else e.DisplayText = string.Format("{0:F2} M/s", value / 1024.0 / 1024.0);
                            break;
                        }
                    case TaskStatus.Pending: 
                        {
                            e.DisplayText = "等待中";
                            break;
                        }
                }
            }
            else if (e.Column.FieldName.Equals("LeftTime"))
            {
                try
                {
                    if (e.CellValue.ToString().Equals("Infinity")) e.DisplayText = "Infinity";
                    else if (e.CellValue.ToString().Equals("-1")) 
                    {
                        e.DisplayText = "--:--:--";
                    }
                    else
                    {
                        int value = Convert.ToInt32(e.CellValue);
                        int seconds = value % 60;
                        int minutes = (value % 3600) / 60;
                        int hours = (value % 216000) / 3600;
                        if (hours >= 24) e.DisplayText = "多于1天";
                        else e.DisplayText = string.Format("{0:D2}:{1:D2}:{2:D2}", new object[] { hours, minutes, seconds });
                    }
                }
                catch { }
            }
            else if (e.Column.FieldName.Equals("ElapsedTime"))
            {
                try
                {
                    if (e.CellValue.ToString().Equals("Infinity")) e.DisplayText = "Infinity";
                    else
                    {
                        int value = Convert.ToInt32(e.CellValue);
                        int seconds = value % 60;
                        int minutes = (value % 3600) / 60;
                        int hours = (value % 216000) / 3600;
                        if (hours >= 24) e.DisplayText = "多于1天";
                        else e.DisplayText = string.Format("{0:D2}:{1:D2}:{2:D2}", new object[] { hours, minutes, seconds });
                    }
                }
                catch { }
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName.Equals("Progress"))
            {
                RepositoryItemProgressBar riPb = e.RepositoryItem as RepositoryItemProgressBar;
                riPb.Step = 1;
                riPb.Maximum = (int)GetTaskByID(new Guid(gridView1.GetRowCellValue(e.RowHandle, "ID").ToString())).Size;
            }
        }

        public static string ConnectLan(string p_Path, string p_UserName, string p_PassWord)
        {
            System.Diagnostics.Process _Process = new System.Diagnostics.Process();
            _Process.StartInfo.FileName = "cmd.exe";
            _Process.StartInfo.UseShellExecute = false;
            _Process.StartInfo.RedirectStandardInput = true;
            _Process.StartInfo.RedirectStandardOutput = true;
            _Process.StartInfo.CreateNoWindow = true;
            _Process.Start();
            //NET USE //192.168.0.1 PASSWORD /USER:UserName
            _Process.StandardInput.WriteLine("net use " + p_Path + " " + p_PassWord + " /user:" + p_UserName);
            _Process.StandardInput.WriteLine("exit");
            _Process.WaitForExit();
            string _ReturnText = _Process.StandardOutput.ReadToEnd();// 得到cmd.exe的输出 
            _Process.Close();
            return _ReturnText;
        }
    }

    public enum TaskViewType
    {
        AllTasks = 0,
        PendingOrLoadingTasksOfDownload,
        PendingOrLoadingTasksOfUpload,
        CompletedTasksOfDownload,
        CompletedTasksOfUpload
    }
}
