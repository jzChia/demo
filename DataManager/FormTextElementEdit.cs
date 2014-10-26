using stdole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

using DevExpress.XtraEditors;

namespace Resee.DataManager
{
    public partial class FormTextElementEdit : DevExpress.XtraEditors.XtraForm
    {
        private TextElementEditType textElementEditType;
        private IActiveView activeView = null;
        private IPoint textPoint = null;
        private ITextElement textElement = null;
        private ITextSymbol textSymbol = null;
        private System.Drawing.Font font = null;
        private Color fontColor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="textElementEditType"></param>
        /// <param name="textPoint"></param>
        public FormTextElementEdit(IActiveView activeView,IPoint textPoint)
        {
            InitializeComponent();

            this.textElementEditType = TextElementEditType.New;
            this.activeView = activeView;
            this.textPoint = textPoint;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="textElementEditType"></param>
        /// <param name="textElement"></param>
        public FormTextElementEdit(IActiveView activeView, ITextElement textElement)
        {
            InitializeComponent();

            this.textElementEditType = TextElementEditType.Modify;
            this.activeView = activeView;
            this.textElement = textElement;
        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTextElementEdit_Load(object sender, EventArgs e)
        {
            switch (textElementEditType)
            {
                case TextElementEditType.New:
                    textSymbol = new TextSymbolClass();
                    break;
                case TextElementEditType.Modify:
                    textSymbol = textElement.Symbol;
                    txtText.Text = textElement.Text;
                    font = GetFontFromIFontDisp(textSymbol.Font);
                    fontColor = ColorTranslator.FromOle(textSymbol.Color.RGB);
                    break;
            }
        }
        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            if (textElementEditType == TextElementEditType.Modify)
            {
                fontDialog.Font = font;
                fontDialog.Color = fontColor;
            }

            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;

            font = fontDialog.Font;
            fontColor = fontDialog.Color;
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtText.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("文本内容不能为空，请确认后再试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //设置字体
            if (font == null)
            {
                FontDialog fontDialog = new FontDialog();
                font = fontDialog.Font;
            }
            IFontDisp fontDisp = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(font) as IFontDisp;
            textSymbol.Font = fontDisp;
            //设置颜色
            IColor color = new RgbColorClass();
            color.RGB = (fontColor.B) * 65536 + (fontColor.G) * 256 + fontColor.R;
            textSymbol.Color = color;
            //添加或更新文本
            switch (textElementEditType)
            {
                case TextElementEditType.New:
                    textElement = new TextElementClass();
                    textElement.Text = txtText.Text;
                    textElement.Symbol = textSymbol;

                    IElement element = textElement as IElement;
                    element.Geometry = textPoint;

                    IGraphicsContainer graphicsContainer = activeView as IGraphicsContainer;
                    graphicsContainer.AddElement(element, 0);
                    break;
                case TextElementEditType.Modify:
                    textElement.Text = txtText.Text;
                    textElement.Symbol = textSymbol;
                    break;
            }
            //刷新
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            DialogResult = DialogResult.OK;
        }

        public System.Drawing.Font GetFontFromIFontDisp(IFontDisp fontDisp)
        {
            string name = fontDisp.Name;
            float size = Convert.ToSingle(fontDisp.Size);
            FontStyle fontStyle = FontStyle.Regular;

            if (fontDisp.Bold)
                fontStyle = FontStyle.Bold;

            if (fontDisp.Italic)
                fontStyle = fontStyle | FontStyle.Italic;

            if (fontDisp.Strikethrough)
                fontStyle = fontStyle | FontStyle.Strikeout;

            if (fontDisp.Underline)
                fontStyle = fontStyle | FontStyle.Underline;

            System.Drawing.Font font = new System.Drawing.Font(name, size, fontStyle);

            return font;
        }


    }
}