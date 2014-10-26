using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransferProcess
{
    public class TransferTask
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string SourceFileName { get; set; }
        public string DestFileName { get; set; }
        public int Progress { get; set; }
        public int Size { get; set; }
        public double NetSize { get; set; }
        public double Speed { get; set; }
        public double ElapsedTime { get; set; }
        public double LeftTime { get; set; }
        public RenameMode RenameMode { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
        public TaskCategory Category { get; set; }

        public TransferTask(Guid id, string sourceFileName, string destFileName, TaskType type, RenameMode renameMode, TaskCategory category)
        {
            this.ID = id;
            this.Name = System.IO.Path.GetFileNameWithoutExtension(this.SourceFileName);
            this.SourceFileName = sourceFileName;
            this.DestFileName = destFileName;
            this.Progress = 0;
            this.Size = int.MaxValue;
            this.NetSize = 0;
            this.Speed = 0;
            this.ElapsedTime = 0;
            this.LeftTime = int.MaxValue;
            this.RenameMode = renameMode;
            this.Type = type;
            this.Status = TaskStatus.Pending;
            this.Category = category;
        }

        public TransferTask(Guid id, string sourceFileName, string destFileName, TaskType type, RenameMode renameMode, TaskCategory category, int taskSize)
        {
            this.ID = id;
            this.Name = System.IO.Path.GetFileNameWithoutExtension(this.SourceFileName);
            this.SourceFileName = sourceFileName;
            this.DestFileName = destFileName;
            this.Progress = 0;
            this.Size = taskSize;
            this.NetSize = 0;
            this.Speed = 0;
            this.ElapsedTime = 0;
            this.LeftTime = int.MaxValue;
            this.RenameMode = renameMode;
            this.Type = type;
            this.Status = TaskStatus.Pending;
            this.Category = category;
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

    public enum TaskStatus
    {
        Pending,
        Loading,
        Completed,
        Canceled
    }

    public enum TaskCategory
    {
        Features,
        Raster,
        Files,
        Folder
    }
}
