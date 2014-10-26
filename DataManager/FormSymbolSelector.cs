using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using stdole;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

using DevExpress.XtraEditors;

namespace Resee.DataManager
{
    /// <summary>
    /// 符号选择窗体，用于选择所需的符号
    /// </summary>
    public partial class FormSymbolSelector : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 当前符号样式分类，如点、线、面等
        /// </summary>
        private esriSymbologyStyleClass CurrentSymbologyStyleClass;
        /// <summary>
        /// 作为入口参数的符号对象
        /// </summary>
        private ISymbol InSymbol;
        /// <summary>
        /// 原始样式类子项，即未经过修改的图层样式类子项
        /// </summary>
        private IStyleGalleryItem OriginStyleGalleryItem;
        /// <summary>
        /// 当前样式类子项，即经过修改的图层样式类子项
        /// </summary>
        private IStyleGalleryItem currentStyleGalleryItem;
        private ISymbol exportSymbol;
        /// <summary>
        /// 作为出口参数的符号对象
        /// </summary>
        public ISymbol ExportSymbol
        {
            get { return exportSymbol; }
        }
        /// <summary>
        /// 点符号对象
        /// </summary>
        private IMarkerSymbol pMarkerSymbol;
        /// <summary>
        /// 线符号对象
        /// </summary>
        private ILineSymbol pLineSymbol;
        /// <summary>
        /// 面符号对象
        /// </summary>
        private IFillSymbol pFillSymbol;
        /// <summary>
        /// 外边线符号对象
        /// </summary>
        private ILineSymbol pOutlineSymbol;
        /// <summary>
        /// 文本符号对象
        /// </summary>
		private ITextSymbol pTextSymbol;
        /// <summary>
        /// 外边线交换子项标志
        /// </summary>
        private bool SwitchItemForOutLine = false;
        /// <summary>
        /// 未经修改的外边线符号
        /// </summary>
        private ILineSymbol pOriginOutlineSymbol;
        /// <summary>
        /// 用于预览的图像
        /// </summary>
        private Image PreViewImage;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="EntranceSymbol">符号对象</param>
        /// <param name="SymbologyStyleClass">符号样式类</param>
        public FormSymbolSelector(ISymbol EntranceSymbol, esriSymbologyStyleClass SymbologyStyleClass)
        {
            InitializeComponent();
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            InSymbol = pObjectCopy.Copy(EntranceSymbol) as ISymbol;
            CurrentSymbologyStyleClass = SymbologyStyleClass;
            currentStyleGalleryItem = new ServerStyleGalleryItemClass();
            pOutlineSymbol = new SimpleLineSymbolClass();
        }
        /// <summary>
        /// 窗体载入时触发事件
        /// </summary>
        /// <param name="sender">窗体控件</param>
        /// <param name="e">Form截入事件参数</param>
        private void SymbolSelectorForm_Load(object sender, EventArgs e)
        {
            comboBoxDisplayStyle.EditValue = "平铺";
            switch (CurrentSymbologyStyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols://点
					tabOptions.SelectedTabPage = tpeMarkerSymbols;
                    pMarkerSymbol = InSymbol as IMarkerSymbol;
                    byte[] myMarkerByte;// = new byte[3];
                    myMarkerByte = LongToRGB(pMarkerSymbol.Color.RGB);
                    colMarkerSymbolsColor.Color = System.Drawing.Color.FromArgb(myMarkerByte[0], myMarkerByte[1], myMarkerByte[2]);
                    spnMarkerSymbolsSize.EditValue = pMarkerSymbol.Size;
                    spnMarkerSymbolsAngle.EditValue = pMarkerSymbol.Angle;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols://线
					tabOptions.SelectedTabPage = tpeLineSymbols;
                    pLineSymbol = InSymbol as ILineSymbol;
                    byte[] myLineByte;// = new byte[3];
                    myLineByte = LongToRGB(pLineSymbol.Color.RGB);
					colLineSymbolsColor.Color = System.Drawing.Color.FromArgb(myLineByte[0], myLineByte[1], myLineByte[2]);
					spnLineSymbolsWidth.EditValue = pLineSymbol.Width;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols://面
					tabOptions.SelectedTabPage = tpeFillSymbols;
                    pFillSymbol = InSymbol as IFillSymbol;
					pOutlineSymbol.Color = pFillSymbol.Outline.Color;
					pOutlineSymbol.Width = pFillSymbol.Outline.Width;
                    try
                    {
                        byte[] myFillByte;// = new byte[3];
                        myFillByte = LongToRGB(pFillSymbol.Color.RGB);
						colFillSymbolsFillColor.Color = System.Drawing.Color.FromArgb(myFillByte[0], myFillByte[1], myFillByte[2]);
                    }
                    catch
                    {
						colFillSymbolsFillColor.Color = System.Drawing.Color.FromArgb(0);
						colFillSymbolsFillColor.Enabled = false;
                    }
					spnFillSymbolsLineWidth.EditValue = pFillSymbol.Outline.Width;
                    try
                    {
                        byte[] myOutLineByte;// = new byte[3];
                        myOutLineByte = LongToRGB(pFillSymbol.Outline.Color.RGB);
						colFillSymbolsLineColor.Color = System.Drawing.Color.FromArgb(myOutLineByte[0], myOutLineByte[1], myOutLineByte[2]);
                    }
                    catch
                    {
						colFillSymbolsLineColor.Color = System.Drawing.Color.FromArgb(0);
                        colFillSymbolsLineColor.Enabled = false;
                    }
                    break;
//				case esriSymbologyStyleClass.esriStyleClassLabels://文本
				case esriSymbologyStyleClass.esriStyleClassTextSymbols://文本
					tabOptions.SelectedTabPage = tpeTextSymbols;
					pTextSymbol = InSymbol as ITextSymbol;
					byte[] byteColor = LongToRGB(pTextSymbol.Color.RGB);
					colTextSymbolsColor.Color = System.Drawing.Color.FromArgb(byteColor[0], byteColor[1], byteColor[2]);
					foreach (FontFamily oneFontFamily in FontFamily.Families)
					{
						comTextSymbolsFont.Properties.Items.Add(oneFontFamily.Name);
					}
					comTextSymbolsFont.Text = pTextSymbol.Font.Name;//当前选择中的字体名
					comTextSymbolsFontSize.Text = pTextSymbol.Font.Size.ToString();//当前选择中字体大小
					btnTextSymbolsBoldFont.Checked = pTextSymbol.Font.Bold;
					btnTextSymbolsItalicFont.Checked = pTextSymbol.Font.Italic;
					btnTextSymbolsUnderlineFont.Checked = pTextSymbol.Font.Underline;
					btnTextSymbolsStrikethrough.Checked = pTextSymbol.Font.Strikethrough;
					ICharacterOrientation pCharacterOrientation = pTextSymbol as ICharacterOrientation;
					chkTextSymbolsCJK.Checked = pCharacterOrientation.CJKCharactersRotation;
					break;
            }
            axSymbologyControl.LoadStyleFile(Application.StartupPath + @"\Styles\ESRI.ServerStyle");
            axSymbologyControl.StyleClass = CurrentSymbologyStyleClass;
            currentStyleGalleryItem.Item = InSymbol;
            PreviewImage();
        }
        /// <summary>
        /// 确定选择符号操作
        /// </summary>
        /// <param name="sender">确定按钮</param>
        /// <param name="e">Button单击事件参数</param>
        private void SymbolSelectorOK_Click(object sender, EventArgs e)
        {
            switch (CurrentSymbologyStyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    exportSymbol = pMarkerSymbol as ISymbol;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    exportSymbol = pLineSymbol as ISymbol;
                    break;
				case esriSymbologyStyleClass.esriStyleClassFillSymbols:
					exportSymbol = pFillSymbol as ISymbol;
					break;
//				case esriSymbologyStyleClass.esriStyleClassLabels:
				case esriSymbologyStyleClass.esriStyleClassTextSymbols:
					exportSymbol = pTextSymbol as ISymbol;
					break;
			}
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 取消选择符号操作
        /// </summary>
        /// <param name="sender">取消按钮</param>
        /// <param name="e">Button单击事件参数</param>
        private void SymbolSelectorCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 选中已有项目时触发事件
        /// </summary>
        /// <param name="sender">符号选择控件</param>
        /// <param name="e">axSymbologyControl选择事件参数</param>
        private void axSymbologyControl_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            SwitchItemForOutLine = true;
            OriginStyleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            switch (CurrentSymbologyStyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
					currentStyleGalleryItem = pObjectCopy.Copy(OriginStyleGalleryItem) as IStyleGalleryItem;
                    pMarkerSymbol = currentStyleGalleryItem.Item as IMarkerSymbol;
                    byte[] myMarkerByte= LongToRGB(pMarkerSymbol.Color.RGB);
                    colMarkerSymbolsColor.Color = System.Drawing.Color.FromArgb(myMarkerByte[0], myMarkerByte[1], myMarkerByte[2]);
                    spnMarkerSymbolsSize.EditValue = pMarkerSymbol.Size;
                    spnMarkerSymbolsAngle.EditValue = pMarkerSymbol.Angle;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
					currentStyleGalleryItem = pObjectCopy.Copy(OriginStyleGalleryItem) as IStyleGalleryItem;
                    pLineSymbol = currentStyleGalleryItem.Item as ILineSymbol;
                    byte[] myLineByte = LongToRGB(pLineSymbol.Color.RGB);
					colLineSymbolsColor.Color = System.Drawing.Color.FromArgb(myLineByte[0], myLineByte[1], myLineByte[2]);
					spnLineSymbolsWidth.EditValue = pLineSymbol.Width;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
					currentStyleGalleryItem = pObjectCopy.Copy(OriginStyleGalleryItem) as IStyleGalleryItem;
                    IFillSymbol pTempFillSymbol = OriginStyleGalleryItem.Item as IFillSymbol;
                    pOriginOutlineSymbol = pTempFillSymbol.Outline;
                    pFillSymbol = currentStyleGalleryItem.Item as IFillSymbol;
                    try
                    {
                        byte[] myFillByte = LongToRGB(pFillSymbol.Color.RGB);
						colFillSymbolsFillColor.Color = System.Drawing.Color.FromArgb(myFillByte[0], myFillByte[1], myFillByte[2]);
                    }
                    catch
                    {
						colFillSymbolsFillColor.Color = System.Drawing.Color.FromArgb(0);
						colFillSymbolsFillColor.Enabled = false;
                    }
					spnFillSymbolsLineWidth.EditValue = pFillSymbol.Outline.Width;
                    try
                    {
                        byte[] myOutLineByte = LongToRGB(pFillSymbol.Outline.Color.RGB);
						colFillSymbolsLineColor.Color = System.Drawing.Color.FromArgb(myOutLineByte[0], myOutLineByte[1], myOutLineByte[2]);
                    }
                    catch
                    {
						colFillSymbolsLineColor.Color = System.Drawing.Color.FromArgb(0);
						colFillSymbolsLineColor.Enabled = false;
                    }
                    break;
//				case esriSymbologyStyleClass.esriStyleClassLabels:
				case esriSymbologyStyleClass.esriStyleClassTextSymbols:
					ITextSymbol pTempTextSymbol = currentStyleGalleryItem.Item as ITextSymbol;
					string strText = pTempTextSymbol.Text;
					currentStyleGalleryItem = pObjectCopy.Copy(OriginStyleGalleryItem) as IStyleGalleryItem;
					pTextSymbol = currentStyleGalleryItem.Item as ITextSymbol;
					pTextSymbol.Text = strText;
					currentStyleGalleryItem.Item = pTextSymbol;
 					pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
 					pTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
					byte[] byteColor = LongToRGB(pTextSymbol.Color.RGB);
					colTextSymbolsColor.Color = System.Drawing.Color.FromArgb(byteColor[0], byteColor[1], byteColor[2]);
					comTextSymbolsFont.Text = pTextSymbol.Font.Name;//当前选择中的字体名
					comTextSymbolsFontSize.Text = pTextSymbol.Font.Size.ToString();//当前选择中字体大小
					btnTextSymbolsBoldFont.Checked = pTextSymbol.Font.Bold;
					btnTextSymbolsItalicFont.Checked = pTextSymbol.Font.Italic;
					btnTextSymbolsUnderlineFont.Checked = pTextSymbol.Font.Underline;
					btnTextSymbolsStrikethrough.Checked = pTextSymbol.Font.Strikethrough;
					ICharacterOrientation pCharacterOrientation = pTextSymbol as ICharacterOrientation;
					chkTextSymbolsCJK.Checked = pCharacterOrientation.CJKCharactersRotation;
					break;
            }
            PreviewImage();
            SwitchItemForOutLine = false;
        }
        /// <summary>
        /// 预览选中符号，使其显示在图片控件中
        /// </summary>
        private void PreviewImage()
        {
            ISymbologyStyleClass pSymbologyStyleClass = axSymbologyControl.GetStyleClass(axSymbologyControl.StyleClass);
            IPictureDisp pPictureDisp = pSymbologyStyleClass.PreviewItem(currentStyleGalleryItem, PreviewSymbology.Width, PreviewSymbology.Height);
            PreViewImage = Image.FromHbitmap(new System.IntPtr(pPictureDisp.Handle));
            PreviewSymbology.Appearance.Image = PreViewImage;
        }
        /// <summary>
        /// RGB值转换R、G、B值
        /// </summary>
        /// <param name="LoneRGB">RGB值</param>
        /// <returns>RGB数组，取值范围0-255</returns>
        public static byte[] LongToRGB(long LongRGB)
        {
            byte[] pbyte=new byte[3];
            pbyte[0] = (byte)(LongRGB % 256);
            pbyte[1] = (byte)((LongRGB / 256) % 256);
            pbyte[2] = (byte)((LongRGB / 65536) % 256);
            return pbyte;
        }
        /// <summary>
        /// 显示样式改变时触发事件
        /// </summary>
        /// <param name="sender">显示样式组合框控件</param>
        /// <param name="e">ComboBoxEdit选择索引变化事件参数</param>
        private void comboBoxDisplayStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxDisplayStyle.EditValue.ToString())
            {
                case "平铺":
                    axSymbologyControl.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleIcon;
                    break;
                case"图标":
                    axSymbologyControl.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleSmallIcon;
                    break;
                case"报告":
                    axSymbologyControl.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleReport;
                    break;
                case"列表":
                    axSymbologyControl.DisplayStyle = esriSymbologyDisplayStyle.esriDisplayStyleList;
                    break;
            }
        }
        /// <summary>
        /// 选择更多符号按钮按下时触发事件
        /// </summary>
        /// <param name="sender">选择更多符号按钮</param>
        /// <param name="e">Button单击事件参数</param>
        private void SymbolSelectorMoreStyles_Click(object sender, EventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            myOpenFileDialog.Title = "请选择样式文件";
            myOpenFileDialog.InitialDirectory = Application.StartupPath+@"\StyleFiles\";
            myOpenFileDialog.Filter = "ESRI样式文件(*.Style,*.ServerStyle)|*.Style;*.ServerStyle";
            DialogResult myDialogResult = myOpenFileDialog.ShowDialog();
            if (myDialogResult == DialogResult.Cancel)
                return;
            axSymbologyControl.Clear();
            string FileName = myOpenFileDialog.FileName;
            int LastIndexOf = FileName.LastIndexOf('.');
            string FileExtend = FileName.Substring(LastIndexOf + 1).ToUpper();
            if (FileExtend == "SERVERSTYLE")
                axSymbologyControl.LoadStyleFile(FileName);
            else if (FileExtend == "STYLE")
                axSymbologyControl.LoadDesktopStyleFile(FileName);
		}
        /// <summary>
        /// 点符号颜色变化时触发事件
        /// </summary>
        /// <param name="sender">点符号颜色选择控件</param>
        /// <param name="e">ColorEdit内容变化事件参数</param>
		private void colMarkerSymbolsColor_EditValueChanged(object sender, EventArgs e)
		{
			System.Drawing.Color myMarkerColor = colMarkerSymbolsColor.Color;
			IRgbColor pRgbColor = new RgbColorClass();
			pRgbColor.Red = myMarkerColor.R;
			pRgbColor.Green = myMarkerColor.G;
			pRgbColor.Blue = myMarkerColor.B;
			pMarkerSymbol.Color = pRgbColor;
			currentStyleGalleryItem.Item = pMarkerSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 线符号颜色变化时触发事件
        /// </summary>
        /// <param name="sender">线符号颜色选择控件</param>
        /// <param name="e">ColorEdit内容变化事件参数</param>
		private void colLineSymbolsColor_EditValueChanged(object sender, EventArgs e)
		{
			System.Drawing.Color myLineColor = colLineSymbolsColor.Color;
			IRgbColor pRgbColor = new RgbColorClass();
			pRgbColor.Red = myLineColor.R;
			pRgbColor.Green = myLineColor.G;
			pRgbColor.Blue = myLineColor.B;
			pLineSymbol.Color = pRgbColor;
			currentStyleGalleryItem.Item = pLineSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 面符号颜色变化时触发事件
        /// </summary>
        /// <param name="sender">面符号颜色选择控件</param>
        /// <param name="e">ColorEdit内容变化事件参数</param>
		private void colFillSymbolsFillColor_EditValueChanged(object sender, EventArgs e)
		{
			System.Drawing.Color myFillColor = colFillSymbolsFillColor.Color;
			IRgbColor pRgbColor = new RgbColorClass();
			pRgbColor.Red = myFillColor.R;
			pRgbColor.Green = myFillColor.G;
			pRgbColor.Blue = myFillColor.B;
			pFillSymbol.Color = pRgbColor;
			currentStyleGalleryItem.Item = pFillSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 点符号大小变化时触发事件
        /// </summary>
        /// <param name="sender">点符号大小选择控件</param>
        /// <param name="e">SpinEdit内容变化事件参数</param>
		private void spnMarkerSymbolsSize_EditValueChanged(object sender, EventArgs e)
		{
			if (spnMarkerSymbolsSize.Value < 0)
			{
				spnMarkerSymbolsSize.Value = 0;
				return;
			}
			pMarkerSymbol.Size = (double)spnMarkerSymbolsSize.Value;
			currentStyleGalleryItem.Item = pMarkerSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 线符号大小变化时触发事件
        /// </summary>
        /// <param name="sender">线符号大小选择控件</param>
        /// <param name="e">SpinEdit内容变化事件参数</param>
		private void spnLineSymbolsWidth_EditValueChanged(object sender, EventArgs e)
		{
			if (spnLineSymbolsWidth.Value < 0)
			{
				spnLineSymbolsWidth.Value = 0;
				return;
			}
			pLineSymbol.Width = (double)spnLineSymbolsWidth.Value;
			currentStyleGalleryItem.Item = pLineSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 面符号大小变化时触发事件
        /// </summary>
        /// <param name="sender">面符号大小选择控件</param>
        /// <param name="e">SpinEdit内容变化事件参数</param>
		private void spnFillSymbolsLineWidth_EditValueChanged(object sender, EventArgs e)
		{
			if (spnFillSymbolsLineWidth.Value < 0)
			{
				spnFillSymbolsLineWidth.Value = 0;
				return;
			}
			pOutlineSymbol.Width = (double)spnFillSymbolsLineWidth.Value;
			if (SwitchItemForOutLine)
				pFillSymbol.Outline = pOriginOutlineSymbol;
			else
				pFillSymbol.Outline = pOutlineSymbol;
			currentStyleGalleryItem.Item = pFillSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 点符号角度变化时触发事件
        /// </summary>
        /// <param name="sender">点符号角度选择控件</param>
        /// <param name="e">SpinEdit内容变化事件参数</param>
		private void spnMarkerSymbolsAngle_EditValueChanged(object sender, EventArgs e)
		{
			pMarkerSymbol.Angle = (double)spnMarkerSymbolsAngle.Value;
			currentStyleGalleryItem.Item = pMarkerSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 面符号线颜色变化时触发事件
        /// </summary>
        /// <param name="sender">面符号线颜色选择控件</param>
        /// <param name="e">SpinEdit内容变化事件参数</param>
		private void colFillSymbolsLineColor_EditValueChanged(object sender, EventArgs e)
		{
			System.Drawing.Color myOutLineColor = colFillSymbolsLineColor.Color;
			IRgbColor pRgbColor = new RgbColorClass();
			pRgbColor.Red = myOutLineColor.R;
			pRgbColor.Green = myOutLineColor.G;
			pRgbColor.Blue = myOutLineColor.B;
			pOutlineSymbol.Color = pRgbColor;
			pFillSymbol.Outline = pOutlineSymbol;
			currentStyleGalleryItem.Item = pFillSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 文字颜色变化时触发事件
        /// </summary>
        /// <param name="sender">文字颜色选择控件</param>
        /// <param name="e">ColorEdit内容变化事件参数</param>
		private void colTextSymbolsColor_EditValueChanged(object sender, EventArgs e)
		{
			System.Drawing.Color myMarkerColor = colTextSymbolsColor.Color;
			IRgbColor pRgbColor = new RgbColorClass();
			pRgbColor.Red = myMarkerColor.R;
			pRgbColor.Green = myMarkerColor.G;
			pRgbColor.Blue = myMarkerColor.B;
			pTextSymbol.Color = pRgbColor;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 文字字体变化时触发事件
        /// </summary>
        /// <param name="sender">字体选择控件</param>
        /// <param name="e">FontEdit内容变化事件参数</param>
        private void comTextSymbolsFont_EditValueChanged(object sender, EventArgs e)
        {
            pTextSymbol.Font.Name = comTextSymbolsFont.Text;
            currentStyleGalleryItem.Item = pTextSymbol;
            PreviewImage();
        }
        /// <summary>
        /// 字体大小变化时触发事件
        /// </summary>
        /// <param name="sender">字体大小选择控件</param>
        /// <param name="e">FontEdit内容变化事件参数</param>
		private void comTextSymbolsFontSize_EditValueChanged(object sender, EventArgs e)
		{
			ComboBoxEdit pComboBoxEdit = sender as ComboBoxEdit;
			decimal dValue;
			try
			{
				dValue = Convert.ToDecimal(pComboBoxEdit.EditValue);
			}
			catch
			{
				pComboBoxEdit.EditValue = pComboBoxEdit.OldEditValue;
			}
		}
        /// <summary>
        /// 字体粗体状态变化时触发事件
        /// </summary>
        /// <param name="sender">字体按钮</param>
        /// <param name="e">Button单击事件参数</param>
		private void btnTextSymbolsBoldFont_CheckedChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Name = pTextSymbol.Font.Name;
			pFont.Size = pTextSymbol.Font.Size;
			pFont.Underline = pTextSymbol.Font.Underline;
			pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
			pFont.Italic = pTextSymbol.Font.Italic;
			pFont.Bold = btnTextSymbolsBoldFont.Checked;
			pTextSymbol.Font = pFont as IFontDisp;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 字体斜体状态变化时触发事件
        /// </summary>
        /// <param name="sender">字体按钮</param>
        /// <param name="e">Button单击事件参数</param>
		private void btnTextSymbolsItalicFont_CheckedChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Name = pTextSymbol.Font.Name;
			pFont.Size = pTextSymbol.Font.Size;
			pFont.Bold = pTextSymbol.Font.Bold;
			pFont.Underline = pTextSymbol.Font.Underline;
			pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
			pFont.Italic = btnTextSymbolsItalicFont.Checked;
			pTextSymbol.Font = pFont as IFontDisp;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 字体下划线状态变化时触发事件
        /// </summary>
        /// <param name="sender">字体按钮</param>
        /// <param name="e">Button单击事件参数</param>
		private void btnTextSymbolsUnderlineFont_CheckedChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Name = pTextSymbol.Font.Name;
			pFont.Size = pTextSymbol.Font.Size;
			pFont.Bold = pTextSymbol.Font.Bold;
			pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
			pFont.Italic = pTextSymbol.Font.Italic;
			pFont.Underline = btnTextSymbolsUnderlineFont.Checked;
			pTextSymbol.Font = pFont as IFontDisp;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 字体删除状态变化时触发事件
        /// </summary>
        /// <param name="sender">字体按钮</param>
        /// <param name="e">Button单击事件参数</param>
		private void btnTextSymbolsStrikethrough_CheckedChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Name = pTextSymbol.Font.Name;
			pFont.Size = pTextSymbol.Font.Size;
			pFont.Bold = pTextSymbol.Font.Bold;
			pFont.Italic = pTextSymbol.Font.Italic;
			pFont.Underline = pTextSymbol.Font.Underline;
			pFont.Strikethrough = btnTextSymbolsStrikethrough.Checked;
			pTextSymbol.Font = pFont as IFontDisp;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// CJK字符变化时触发事件
        /// </summary>
        /// <param name="sender">CJK字符状态按钮</param>
        /// <param name="e">TextEdit状态变化事件参数</param>
		private void chkTextSymbolsCJK_CheckedChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Name = pTextSymbol.Font.Name;
			pFont.Size = pTextSymbol.Font.Size;
			pFont.Bold = pTextSymbol.Font.Bold;
			pFont.Italic = pTextSymbol.Font.Italic;
			pFont.Underline = pTextSymbol.Font.Underline;
			pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
			pTextSymbol.Font = pFont as IFontDisp;
			ICharacterOrientation pCharacterOrientation = pTextSymbol as ICharacterOrientation;
			pCharacterOrientation.CJKCharactersRotation = chkTextSymbolsCJK.Checked;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 文字符号字体变化时触发事件
        /// </summary>
        /// <param name="sender">文字符号字体组合框控件</param>
        /// <param name="e">TextEdit选择索引变化事件参数</param>
		private void comTextSymbolsFont_SelectedValueChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Size = pTextSymbol.Font.Size;
			pFont.Underline = pTextSymbol.Font.Underline;
			pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
			pFont.Italic = pTextSymbol.Font.Italic;
			pFont.Bold = pTextSymbol.Font.Bold;
			pFont.Name = comTextSymbolsFont.Text;
			pTextSymbol.Font = pFont as IFontDisp;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
        /// <summary>
        /// 文字符号大小变化时触发事件
        /// </summary>
        /// <param name="sender">文字符号大小组合框控件</param>
        /// <param name="e">TextEdit选择值变化事件参数</param>
		private void comTextSymbolsFontSize_SelectedValueChanged(object sender, EventArgs e)
		{
			stdole.Font pFont = new stdole.StdFontClass();
			pFont.Name = pTextSymbol.Font.Name;
			pFont.Underline = pTextSymbol.Font.Underline;
			pFont.Strikethrough = pTextSymbol.Font.Strikethrough;
			pFont.Italic = pTextSymbol.Font.Italic;
			pFont.Bold = pTextSymbol.Font.Bold;
			pFont.Size = Convert.ToDecimal(comTextSymbolsFontSize.EditValue);
			pTextSymbol.Font = pFont as IFontDisp;
			currentStyleGalleryItem.Item = pTextSymbol;
			PreviewImage();
		}
    }
}
