using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;



namespace MainGUI
{
    public partial class Form_Clip : Form  
    {
      

        public Form_Clip()
        {
            InitializeComponent();
          
        }

        public void InitInputCombox(IMapControl2 pMapControl)
        {
           
            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
         
        }

        private void Form_Clip_Load(object sender, EventArgs e)
        {
           
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
         
        }
        #region 新增代码
        private void UpdateTipLocation()
        {
           
        }
        public void SetToolTips()
        {
            ToolTip toolTips = new ToolTip();
            toolTips.SetToolTip(buttonOK, "点击实现裁剪操作");
            toolTips.SetToolTip(buttonCancel, "点击取消并关闭操作");
            toolTips.SetToolTip(button1, "点击执行裁剪函数");
           
        }

        private void Form_Clip_Move(object sender, EventArgs e)
        {
            UpdateTipLocation();
        }

        private void Form_Clip_Resize(object sender, EventArgs e)
        {
            UpdateTipLocation();
        }

        private void Form_Clip_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void comboBoxInput_Enter(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGCJ", "001");
        }

        private void comboBoxExtent_Enter(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGCJ", "002");
        }

        private void checkBoxGeo_Click(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGCJ", "003");
        }

        private void textBoxOutputRaster_Enter(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGCJ", "004");
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGCJ", "005");
        }

        private void checkBoxGeo_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
