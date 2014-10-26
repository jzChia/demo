using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    public class LoadTarget
    {
        public double Progress { get; set; }
        public string In_Directory { get; set; }
        public string In_FileName { get; set; }
        public string Out_Directory { get; set; }
        public string Out_FileName { get; set; }
        public double Size { get; set; }
        public double Speed { get; set; }
        public string Type { get; set; }
        public bool IsFinished { get; set; }
        public bool IsBusy { get; set; }
        public bool HasLoaded { get; set; }
    }


}
