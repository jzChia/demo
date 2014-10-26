using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
    public partial class MetaDataQuery : Form
    {
        public MetaDataQuery(int index)
        {
            InitializeComponent();
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            xtraTabControl1.SelectedTabPageIndex = index;
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
