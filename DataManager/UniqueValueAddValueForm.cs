using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Resee.DataManager
{
    /// <summary>
    /// 添加唯一值窗体，用于新增唯一值渲染中的唯一值
    /// </summary>
    public partial class UniqueValueAddValueForm : DevExpress.XtraEditors.XtraForm
    {
        private ValueType valueType;

        public string Value
        {
            get { return ValueTextEdit.Text; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UniqueValueAddValueForm(ValueType valueType)
        {
            InitializeComponent();

            this.valueType = valueType;
        }
        /// <summary>
        /// 添加值按钮按下时触发事件
        /// </summary>
        /// <param name="sender">添加值按钮</param>
        /// <param name="e">Button单击事件参数</param>
        private void AddValue_Click(object sender, EventArgs e)
        {
            if (valueType == ValueType.Int)
            {
                int intValue;
                bool isOK = Int32.TryParse(ValueTextEdit.Text, out intValue);
                if (!isOK)
                {
                    XtraMessageBox.Show("请输入整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            
        }
    }
}