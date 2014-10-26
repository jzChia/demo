using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;


namespace MainGUI
{
    
    public partial class Form_Moasic : Form
    {

        public Form_Moasic()
        {
            InitializeComponent();
           
        }
       

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void FormMoasic_Load(object sender, EventArgs e)
        {
          
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUp_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDel_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDown_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBoxInputRaster_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }
        #region 新增代码
        private void UpdateTipLocation()
        {
           
        }
        public void SetToolTips()
        {
           
           
            
        }

        private void Form_Moasic_FormClosing(object sender, FormClosingEventArgs e)
        {
            //frm_tips.Close();
        }

        private void Form_Moasic_Move(object sender, EventArgs e)
        {
           // UpdateTipLocation();
        }

        private void Form_Moasic_Resize(object sender, EventArgs e)
        {
            //UpdateTipLocation();
        }

        private void comboBoxInputRaster_Enter(object sender, EventArgs e)
        {
            //frm_tips.UpdateTips("SGXQ", "001");
        }

        private void listBox1_Enter(object sender, EventArgs e)
        {
            //frm_tips.UpdateTips("SGXQ", "002");
        }

        private void textBoxOutput_Enter(object sender, EventArgs e)
        {
            //frm_tips.UpdateTips("SGXQ", "003");
        }

        private void comboBoxPatton_Enter(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGXQ", "004");
        }

        private void textBoxNum_Enter(object sender, EventArgs e)
        {
           // frm_tips.UpdateTips("SGXQ", "005");
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            //frm_tips.UpdateTips("SGXQ", "006");
        }
        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
