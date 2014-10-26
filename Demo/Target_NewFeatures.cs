using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Demo
{
    public partial class Target_NewFeatures : DevExpress.XtraEditors.XtraForm
    {
        public LoadTarget Target { get; set; }

        public Target_NewFeatures()
        {
            InitializeComponent();
            Target = new LoadTarget();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Target.In_Directory = textEdit1.Text;
            Target.In_FileName = textEdit2.Text;
            Target.Out_Directory = textEdit3.Text;
            Target.Out_FileName = textEdit4.Text;
            Target.Progress = 0;
            Target.Size = int.MaxValue;
            Target.Type = "Vector";
            Target.IsFinished = false;
            Target.IsBusy = false;
            Target.HasLoaded = false;
        }


    }
}