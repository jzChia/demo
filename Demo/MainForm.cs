using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using System.Data.OleDb;

using DevExpress.XtraBars;
using ESRI.ArcGIS.SystemUI;
using Microsoft.Win32;
using Resee.ImageProcessing;
using MainGUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesFile;
using System.Security.Principal;
using TransferManager;
using System.Runtime.InteropServices;
using Demo.Utility;
using Demo.Entities;
using System.Diagnostics;

namespace Demo
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm(int mode)
        {
            InitializeComponent();
            Initialize();
            ribbonPage3.Visible = (mode > 1);
            ribbonPage7.Visible = (mode > 2);
            ribbon.SelectedPage = ribbonPage1;
        }
        #region class private members
        private ITool m_pMapPanTool = null;
        private ITool m_pMapZoomInTool = null;
        private ITool m_pMapZoomOutTool = null;
        private ICommand m_pMapFixedZoomInCommand = null;
        private ICommand m_pMapFixedZoomOutCommand = null;
        private ICommand m_pMapFullExtentCommand = null;
        private ICommand m_pMapPreExtentCommand = null;
        private ICommand m_pMapNextExtentCommand = null;
        private ICommand m_pMapRefreshCommand = null;
        private ICommand m_pMapAddDataCommand = null;

        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;

        private IToolbarMenu m_pTOCLayerMenu = null;
        private IToolbarMenu m_pTOCMapMenu = null;
        #endregion

        private void Initialize() 
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            xtraTabControl2.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            m_mapControl = (IMapControl3)axMapControl1.Object;

            #region 命令按钮初始化
            m_pMapPanTool = new ControlsMapPanTool() as ITool;
            m_pMapZoomInTool = new ControlsMapZoomInTool() as ITool;
            m_pMapZoomOutTool = new ControlsMapZoomOutTool() as ITool;
            m_pMapFullExtentCommand = new ControlsMapFullExtentCommand() as ICommand;
            m_pMapFixedZoomInCommand = new ControlsMapZoomInFixedCommand() as ICommand;
            m_pMapFixedZoomOutCommand = new ControlsMapZoomOutFixedCommand() as ICommand;
            m_pMapPreExtentCommand = new ControlsMapZoomToLastExtentForwardCommand() as ICommand;
            m_pMapNextExtentCommand = new ControlsMapZoomToLastExtentBackCommand() as ICommand;
            m_pMapRefreshCommand = new ControlsMapRefreshViewCommand() as ICommand;
            m_pMapAddDataCommand = new ControlsAddDataCommand() as ICommand;
            #endregion

            #region TOC右键菜单
            m_pTOCLayerMenu = new ToolbarMenu() as IToolbarMenu;
            m_pTOCLayerMenu.AddItem(new TOCTools.RemoveLayerCommand(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pTOCLayerMenu.AddItem(new TOCTools.ZoomToLayerCommand(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pTOCLayerMenu.AddItem(new TOCTools.LayerPropertiesCommand(), -1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pTOCLayerMenu.SetHook(m_mapControl);

            m_pTOCMapMenu = new ToolbarMenu() as IToolbarMenu;
            m_pTOCMapMenu.AddItem(new TOCTools.TOCLayersVisibility(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pTOCMapMenu.AddItem(new TOCTools.TOCLayersVisibility(), 2, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pTOCMapMenu.SetHook(m_mapControl);
            #endregion

            axTOCControl1.SetBuddyControl(m_mapControl);
            m_mapControl.Map.Name = "图层";
            axTOCControl2.SetBuddyControl(axPageLayoutControl1);

            IPrincipal _principal = System.Threading.Thread.CurrentPrincipal;

            if (_principal.Identity.IsAuthenticated)
            {
                CurrentUser cu = (CurrentUser)_principal;
                MessageBox.Show("您已经登录，当前用户：" + _principal.Identity.Name + "     权限  " + cu.GetGetRight()+"---"+cu.GetQueryRight()+"\t\r\n"+
                     "当前角色：" + (_principal.IsInRole("管理员") ? "管理员" : "非管理员"));
            }
            else
            {
                MessageBox.Show( "您还没有登录");
            }

            transferTaskListControl1.Initialize();
        }

        #region 响应函数

        private void ribbon_SelectedPageChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(this.ribbon.SelectedPage.Tag);
            xtraTabControl1.SelectedTabPageIndex = index;
            xtraTabControl2.SelectedTabPageIndex = index;
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapZoomInTool as ICommand;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = m_pMapZoomInTool;
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapZoomOutTool as ICommand;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = m_pMapZoomOutTool;
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapPanTool as ICommand;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = m_pMapPanTool;
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapFixedZoomInCommand;
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapFixedZoomOutCommand;
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapFullExtentCommand;
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapPreExtentCommand;
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapNextExtentCommand;
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand pCommand = m_pMapRefreshCommand;
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ribbon.SelectedPage.PageIndex == 3)
            {
                ICommand pCommand = m_pMapAddDataCommand;
                pCommand.OnCreate(axPageLayoutControl1.Object);
                pCommand.OnClick();
            }
            else
            {
                ICommand pCommand = m_pMapAddDataCommand;
                pCommand.OnCreate(axMapControl1.Object);
                pCommand.OnClick();
            }
        }

        private void Registry11()
        {
            RegistryKey reg = Registry.LocalMachine;
            TreeNode node = new TreeNode("HKEY_LOCAL_MACHINE");
            ReadRegistry(reg, node, 0);
            reg.Close();
            treeView1.Nodes.Add(node);
        }

        internal void ReadRegistry(RegistryKey reg, TreeNode nodes, int depth)//递归注册表树
        {
            if (depth > 2) return;
            foreach (string names in reg.GetSubKeyNames())
            {
                TreeNode node = new TreeNode(names);
                if (!names.Equals("SECURITY"))
                {
                    try
                    {
                        RegistryKey r = reg.OpenSubKey(names, false);
                        if (r != null)
                        {
                            ReadRegistry(r, node, depth + 1);
                            r.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception err)
                    {
                        continue;
                    }
                }
                nodes.Nodes.Add(node);
            }
        }

        internal void ReadRegistry(RegistryKey reg, TreeNode nodes)//递归注册表树
        {
            foreach (string names in reg.GetSubKeyNames())
            {
                TreeNode node = new TreeNode(names);
                if (!names.Equals("SECURITY"))
                {
                    try
                    {
                        RegistryKey r = reg.OpenSubKey(names, false);
                        if (r != null)
                        {
                            ReadRegistry(r, node);
                            r.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception err)
                    {
                        continue;
                    }
                }
                nodes.Nodes.Add(node);
            }
        }

        internal void ReadRegistry11(RegistryKey reg, TreeNode nodes)//递归注册表树
        {
            foreach (string names in reg.GetSubKeyNames())
            {
                TreeNode node = new TreeNode(names);
                if (!names.Equals("SECURITY"))
                {
                    try
                    {
                        RegistryKey r = reg.OpenSubKey(names, false);
                        if (r != null)
                        {
                            ReadRegistry11(r, node);
                            r.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception err)
                    {
                        continue;
                    }
                }
                nodes.Nodes.Add(node);
            }
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Demo.FormMetaData frmMD = new Demo.FormMetaData();
            //frmMD.Show();
            ////Registry11();
            TreeNode rootNode = new TreeNode("数据库");
            TreeNode rasterNode  = TreeNodeOra.GetTreeNode("RASTER_LAYERS","影像库");
            if(rasterNode!=null)
                rootNode.Nodes.Add(rasterNode);
            TreeNode demNode = TreeNodeOra.GetTreeNode("RASTER_LAYERS", "DEM库");
            if(demNode!=null)
                rootNode.Nodes.Add(demNode);
            TreeNode vectorNode = TreeNodeOra.GetTreeNode("VECTOR_LAYERS", "矢量库");
            if (vectorNode != null)
                rootNode.Nodes.Add(vectorNode);
            TreeNode threeDNode = new TreeNode("三维库");
            rootNode.Nodes.Add(threeDNode);
            TreeNode testNode = new TreeNode("试验库");
            rootNode.Nodes.Add(testNode);
            TreeNode otherNode = new TreeNode("其他库");
            rootNode.Nodes.Add(otherNode);

            rootNode.ExpandAll();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(rootNode);

        }

        private TreeNode GetTreeNode(string DB,string DBName)
        { 
        OleDbConnection my_con;
            OleDbCommand dbcmd;
            OleDbDataReader dbrd;
            string SQL;
            my_con = DBCon.DBCon_Open();

            SQL =string.Format( "SELECT * FROM [{0}] WHERE DBNAME = @p1",DB);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            dbcmd = new OleDbCommand(SQL, my_con);
            dbcmd.Parameters.AddWithValue("@p1", DBName);

            adapter.SelectCommand = dbcmd;

            try { dbrd = dbcmd.ExecuteReader(); }
            catch { return null; }

            if (!dbrd.HasRows)
            {
                return null;
            }
            TreeNode nowNode = new TreeNode(DBName);
            nowNode.Tag = DB;

            string errMSG = "";
            while (dbrd.Read())
            {
                TreeNode childNode = new TreeNode();
                try { childNode.Tag = dbrd["ID"]; }
                catch { errMSG += "ID;"; }
                try { childNode.Text =Convert.ToString( dbrd["LAYERNAME"]); }
                catch { errMSG += "LAYERNAME;"; }
                nowNode.Nodes.Add(childNode);
            }
            my_con.Close();
            my_con.Dispose();

            return nowNode;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //List<string> names = new List<string>();
            //TreeNode n=e.Node;
            //while (n.Parent != null) 
            //{
            //    if (names.Count == 0)
            //        names.Add(n.Parent.Text);
            //    else names.Insert(0, n.Parent.Text);
            //    n = n.Parent;
            //}

            //names.Add(e.Node.Text);

            
            //RegistryKey reg = Registry.LocalMachine;
            //RegistryKey r = reg.OpenSubKey(names[1],false);
            //for (int i = 2; i < names.Count; i++) 
            //{
            //    r = r.OpenSubKey(names[i], false);
            //}


            //TreeNode node = new TreeNode(e.Node.Text);
            //ReadRegistry11(r, node);
            //reg.Close();
            //treeView2.Nodes.Add(node);
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            MetaDataQuery mdq = new MetaDataQuery(0);
            mdq.ShowDialog();
        }

        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
            MetaDataQuery mdq = new MetaDataQuery(1);
            mdq.ShowDialog();
        }

        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
            MetaDataQuery mdq = new MetaDataQuery(2);
            mdq.ShowDialog();
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            MetaDataQuery mdq = new MetaDataQuery(0);
            mdq.ShowDialog();
        }

        private void barButtonItem72_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImageEnhanceBright frmAdjustBright = new frmImageEnhanceBright();
            frmAdjustBright.ShowInTaskbar = false;
            frmAdjustBright.Owner = this;
            frmAdjustBright.ShowDialog();
        }

        private void barButtonItem73_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImageEnhanceContrast frmAdjustContrast = new frmImageEnhanceContrast();
            frmAdjustContrast.ShowInTaskbar = false;
            frmAdjustContrast.Owner = this;
            frmAdjustContrast.ShowDialog();
        }

        private void barButtonItem74_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmImageEnhanceTransparency frmAdjusTranspaency = new frmImageEnhanceTransparency();
            frmAdjusTranspaency.ShowInTaskbar = false;
            frmAdjusTranspaency.Owner = this;
            frmAdjusTranspaency.ShowDialog();
        }

        private void barButtonItem75_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frmImageEnhanceStretch = new Form_ImageEnhanceStretch();
            frmImageEnhanceStretch.Owner = this;
            frmImageEnhanceStretch.ShowInTaskbar = false;
            frmImageEnhanceStretch.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Resample frmResample = new Form_Resample() { ShowInTaskbar = false, ShowIcon = false };
            frmResample.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Resample frmResample = new Form_Resample() { ShowInTaskbar = false, ShowIcon = false };
            frmResample.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Clip frmClip = new Form_Clip() { ShowInTaskbar = false, ShowIcon = false };
            frmClip.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Moasic frmMosaic = new Form_Moasic() { ShowInTaskbar = false, ShowIcon = false };
            frmMosaic.ShowDialog();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand command = new CreateNewDocument();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
             string m_currentMapDocument = axMapControl1.DocumentFilename;

             //execute Save Document command

             if (axMapControl1.CheckMxFile(m_currentMapDocument))

             {

                 //create a new instance of a MapDocument

                 IMapDocument mapDoc = new MapDocumentClass();

                 mapDoc.Open(m_currentMapDocument, string.Empty);

 

                 //Make sure that the MapDocument is not readonly

                 if (mapDoc.get_IsReadOnly(m_currentMapDocument))

                 {

                     MessageBox.Show("Map document is read only!");

                     mapDoc.Close();

                     return;

                 }

 

                 //Replace its contents with the current map

                 mapDoc.ReplaceContents((IMxdContents)axMapControl1.Map);

 

                 //save the MapDocument in order to persist it

                 mapDoc.Save(mapDoc.UsesRelativePaths, false);

 

                 //close the MapDocument

                 mapDoc.Close();

 

                 MessageBox.Show("保存地图文档成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

             }

             else

             {

                 MessageBox.Show(m_currentMapDocument+"不是有效的地图文档！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

             }

        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
             {
                 ICommand command = new ControlsSaveAsDocCommandClass();
                 command.OnCreate(axMapControl1.Object);
                 command.OnClick();
                 MessageBox.Show("另存为地图文档成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
             }

             catch (Exception ex)

             {
                 MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
             }
        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Transfer");
            foreach (Process pc in processes) 
            {
                pc.Kill();
            }

            Application.Exit();
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object other = null;
            object index = null;

            //Determine what kind of item is selected
            axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);


            //Ensure the item gets selected 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
            {
                axTOCControl1.SelectItem(map, null);
            }
            else if (item == esriTOCControlItem.esriTOCControlItemLayer)
            {
                axTOCControl1.SelectItem(layer, null);
            }
            else
            { return; }

            //Set the layer into the CustomProperty (this is used by the custom layer commands)			
            m_mapControl.CustomProperty = layer;

            if (e.button != 2) return;

            //Popup the correct context menu--
            if (item == esriTOCControlItem.esriTOCControlItemMap) m_pTOCMapMenu.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer) m_pTOCLayerMenu.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
        }

        

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        private void SaveConfig(string strKey,string strValue)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = Application.StartupPath + "\\config.xml";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("cfg");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == strKey)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    att.Value = strValue;
                    break;
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        private void GetConfig(string strKey,out string strValue)
        {
            strValue = "";
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = Application.StartupPath+"\\config.xml";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("cfg");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == strKey)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    strValue=att.Value;
                    break;
                }
            }
        }

        #endregion

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

       

        #region 数据入库
        //**********************************************************************************************************************
        //**********************************************************************************************************************

        #region 消息接收（用于数据传输进程任务完成时接收反馈信息）
        private const int WM_COPYDATA = 0x004A;

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    {
                        COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                        Type mytype = mystr.GetType();
                        mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                        TransferTask currentTask = transferTaskListControl1.GetTaskByID(new Guid(mystr.lpData.Substring(0, mystr.cbData)));
                        if (currentTask != null)
                        {
                            transferTaskListControl1.CompleteTask(currentTask);

                            //写入元数据
                            TemplateMetaData metaData;
                            MetaDts.TryGetValue(currentTask.ID, out metaData);

                            if (currentTask.Type == TaskType.Upload)
                            {
                                switch (metaData.MetaDataType)
                                {
                                    case EnumMetaDataType.Raster:
                                        {
                                            Demo.Entities.RasterLayer rstLyr = metaData.LayerData as Demo.Entities.RasterLayer;
                                            rstLyr.LAYERSIZE = (long)currentTask.Size;
                                            if (Demo.Entities.RasterLayer.ImportRasterIndex(rstLyr) == 1)
                                            {
                                                //MessageBox.Show("元数据保存成功！");
                                            }
                                            else MessageBox.Show("元数据保存失败！");
                                            break;
                                        }
                                    case EnumMetaDataType.Features:
                                        {
                                            VectorLayer vecLyr = metaData.LayerData as VectorLayer;
                                            vecLyr.LAYERSIZE = (long)currentTask.Size;
                                            if (VectorLayer.ImportVectorIndex(vecLyr) == 1)
                                            {
                                                //MessageBox.Show("元数据保存成功！");
                                            }
                                            else MessageBox.Show("元数据保存失败！");
                                            break;
                                        }
                                    case EnumMetaDataType.Files:
                                        {
                                            FileLayer fileLyr = metaData.LayerData as FileLayer;
                                            fileLyr.LAYERSIZE = (long)currentTask.Size;
                                            if (FileLayer.ImportFileIndex(fileLyr) == 1)
                                            {
                                                
                                                //MessageBox.Show("元数据保存成功！");
                                            }
                                            else MessageBox.Show("元数据保存失败！");
                                            break;
                                        }
                                }
                            }
                            else 
                            {

                            }
                        }
                        break;
                    }
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
        #endregion

        private enum EnumMetaDataType { Features, Raster, Files }

        private class TemplateMetaData
        {
            public object LayerData;
            public EnumMetaDataType MetaDataType;
            public TaskType TaskType;

            public TemplateMetaData(object layerData, EnumMetaDataType type, TaskType taskType) 
            {
                this.LayerData = layerData;
                this.MetaDataType = type;
                this.TaskType = taskType;
            }
        }
        private Dictionary<Guid, TemplateMetaData> MetaDts = new Dictionary<Guid, TemplateMetaData>();

        /// <summary>
        /// 上传栅格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportToGDB itg = new ImportToGDB((int)enumDataType.raster);
            itg.Text = "导入影像数据";
            if (itg.ShowDialog() == DialogResult.OK) 
            {
                Guid taskID = Guid.NewGuid();
                MetaDts.Add(taskID, new TemplateMetaData(itg.rstLayer, EnumMetaDataType.Raster, TaskType.Upload));

                TransferTask transferTask = new TransferTask(taskID, itg.InputPath, itg.OutputPath, TaskType.Upload, itg.RenameMode, TaskCategory.Raster);
                transferTaskListControl1.AddTask(transferTask);
            }
        }

        /// <summary>
        /// 上传矢量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportToGDB itg = new ImportToGDB((int)enumDataType.vector);
            itg.Text = "导入矢量数据";
            if (itg.ShowDialog() == DialogResult.OK)
            {
                Guid taskID = Guid.NewGuid();
                MetaDts.Add(taskID, new TemplateMetaData(itg.vctLayer, EnumMetaDataType.Features, TaskType.Upload));

                TransferTask transferTask = new TransferTask(taskID, itg.InputPath, itg.OutputPath, TaskType.Upload, itg.RenameMode, TaskCategory.Features);
                transferTaskListControl1.AddTask(transferTask);
            }
        }

        /// <summary>
        /// 上传其他数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportToGDB itg = new ImportToGDB((int)enumDataType.other);
            itg.Text = "导入其他数据";
            if (itg.ShowDialog() == DialogResult.OK)
            {
                Guid taskID = Guid.NewGuid();
                MetaDts.Add(taskID, new TemplateMetaData(itg.fleLayer, EnumMetaDataType.Files, TaskType.Upload));

                TransferTask transferTask = new TransferTask(taskID, itg.InputPath, itg.OutputPath, TaskType.Upload, itg.RenameMode, TaskCategory.Files);
                transferTaskListControl1.AddTask(transferTask);

                //TransferProcess.TransferTask transferTask = new TransferProcess.TransferTask(taskID, itg.InputPath, itg.OutputPath, TransferProcess.TaskType.Upload, TransferProcess.RenameMode.Accumulate, TransferProcess.TaskCategory.Files);
                //transferManagerControl1.AddDownloadTask(transferTask);
            }
        }

        /// <summary>
        /// 导出到本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent.Tag.ToString().Equals("RASTER_LAYERS")) 
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK) 
                {
                    Demo.Entities.RasterLayer rstLyr = Demo.Entities.RasterLayer.GetRasterIndexById(treeView1.SelectedNode.Tag.ToString());
                    URI uri = new URI();
                    string sourceFileName = uri.GetServerTruePath(rstLyr.URI);
                    string destFileNickName = fbd.SelectedPath + "\\" + System.IO.Path.GetFileName(sourceFileName);
                    Transfer.RasterReadHelper destRstInfo = new Transfer.RasterReadHelper(destFileNickName);
                    string destFileName = fbd.SelectedPath + "\\" + destRstInfo.NameWithoutExtension + destRstInfo.Extension;

                    Guid taskID = Guid.NewGuid();
                    TransferTask transferTask = new TransferTask(taskID, sourceFileName, destFileName, TaskType.Download, RenameMode.Accumulate, TaskCategory.Raster, (int)rstLyr.LAYERSIZE);
                    transferTaskListControl1.AddTask(transferTask);
                }
            }
            else if (treeView1.SelectedNode.Parent.Tag.ToString().Equals("VECTOR_LAYERS")) 
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    VectorLayer vctLyr = VectorLayer.GetVectorIndexById(treeView1.SelectedNode.Tag.ToString());
                    URI uri = new URI();
                    string sourceFileName = uri.GetServerTruePath(vctLyr.URI);
                    string destFileNickName = fbd.SelectedPath + "\\" + System.IO.Path.GetFileName(sourceFileName);
                    Transfer.FeatureReadHelper destFeatInfo = new Transfer.FeatureReadHelper(destFileNickName);
                    string destFileName = fbd.SelectedPath + "\\" + destFeatInfo.NameWithoutExtension;

                    Guid taskID = Guid.NewGuid();
                    TransferTask transferTask = new TransferTask(taskID, sourceFileName, destFileName, TaskType.Download, RenameMode.Accumulate, TaskCategory.Features, (int)vctLyr.LAYERSIZE);
                    transferTaskListControl1.AddTask(transferTask);
                }
            }
        }

       
        #endregion
        //**********************************************************************************************************************
        //**********************************************************************************************************************

        /// <summary>
        /// 下载栅格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem84_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 下载矢量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem85_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 下载其他数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem86_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem83_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportToGDB itg = new ImportToGDB((int)enumDataType.dem);
            itg.Text = "导入地形数据";
            itg.ShowDialog();
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportToGDB itg = new ImportToGDB((int)enumDataType.threeD);
            itg.Text = "导入三维数据";
            itg.ShowDialog();
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportToGDB itg = new ImportToGDB((int)enumDataType.test);
            itg.Text = "导入试验数据";
            itg.ShowDialog();
        }

        

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            //string out1, out2;
            //GetConfig("Size", out out1);
            //GetConfig("Time", out out2);
            //MessageBox.Show(out1 + " " + out2);
        }

       

        private void barButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        #region 数据传输
        //**********************************************************************************************************************
        //**********************************************************************************************************************
        
        /// <summary>
        /// 查看当前上传任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            transferTaskListControl1.ChangeTaskView(TransferManager.TaskViewType.PendingOrLoadingTasksOfUpload);
        }

        /// <summary>
        /// 查看已完成上传任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            transferTaskListControl1.ChangeTaskView(TransferManager.TaskViewType.CompletedTasksOfUpload);
        }

        /// <summary>
        /// 查看当前下载任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            transferTaskListControl1.ChangeTaskView(TransferManager.TaskViewType.PendingOrLoadingTasksOfDownload);
        }

        /// <summary>
        /// 查看已完成下载任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            transferTaskListControl1.ChangeTaskView(TransferManager.TaskViewType.CompletedTasksOfDownload);
        }

        //**********************************************************************************************************************
        //**********************************************************************************************************************
        #endregion

        #region 响应函数

        private void treeView1_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.GetNodeAt(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
            if (tn != null)
            {
                treeView1.SelectedNode = tn;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            if (tn == null) return;

            string layerID = Convert.ToString(tn.Tag);

            if (tn.Parent.Tag!=null && tn.Parent.Tag.ToString() == "RASTER_LAYERS")
            {
                Demo.Entities.RasterLayer rl = Demo.Entities.RasterLayer.GetRasterIndexById(layerID);
                if (rl == null) return;
                FormMetaData fmd = new FormMetaData(rl);
                fmd.ShowDialog();
            }
            else if (tn.Parent.Tag != null && tn.Parent.Tag.ToString() == "VECTOR_LAYERS")
            {
                VectorLayer vl = VectorLayer.GetVectorIndexById(layerID);
                if (vl == null) return;
                FormMetaData fmd = new FormMetaData(vl);
                fmd.ShowDialog();
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Search fs = new Form_Search();
            if (fs.ShowDialog() == DialogResult.OK) 
            {
                dockPanel2.Show();
                gridControl1.DataSource = fs.SearchResult;
            }
        }

        

        

        
    }



 /// <summary>

       /// Summary description for CreateNewDocument.

       /// </summary>

       public class CreateNewDocument : BaseCommand
       {
           private IHookHelper m_hookHelper = null;
          //constructor
          public CreateNewDocument()
          {
              // the base properties
              base.m_category = ".NET Samples";
              base.m_caption = "NewDocument";
              base.m_message = "Create a new map";
              base.m_toolTip = "Create a new map";
             base.m_name = "DotNetTemplate_NewDocumentCommand";
          }

 
         /// <summary>

          /// Occurs when this command is created

          /// </summary>

         /// <param name="hook">Instance of the application</param>

          public override void OnCreate(object hook)

          {

              if (m_hookHelper == null)

                  m_hookHelper = new HookHelperClass();

  

              m_hookHelper.Hook = hook;

          }

  

          /// <summary>

          /// Occurs when this command is clicked

          /// </summary>

          public override void OnClick()

          {

              IMapControl3 mapControl = null;

  

              //get the MapControl  the hook in case the container is a ToolbarControl

              if (m_hookHelper.Hook is IToolbarControl)

              {

                  mapControl = (IMapControl3)((IToolbarControl)m_hookHelper.Hook).Buddy;

              }

              //In case the container is MapControl

              else if (m_hookHelper.Hook is IMapControl3)

              {

                  mapControl = (IMapControl3)m_hookHelper.Hook;

              }

              else

              {

                  //MessageBox.Show("Active control must be MapControl!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exction);

                  MessageBox.Show("必须是MapControl控件!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                  return;

              }

  

              //check to see if there is an active edit session and whether edits have been made

              DialogResult result;

              IEngineEditor engineEditor = new EngineEditorClass();

  

              if ((engineEditor.EditState == esriEngineEditState.esriEngineStateEditing) && (engineEditor.HasEdits() == true))

              {

                  //result = MessageBox.Show("Would you like to save your edits", "Save Edits", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                  result = MessageBox.Show("是否保存编辑", "保存编辑", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

  

  

                  switch (result)

                  {

  

                      case DialogResult.Cancel:

                          return;

  

                      case DialogResult.No:

                          engineEditor.StopEditing(false);

                          break;

  

                      case DialogResult.Yes:

                          engineEditor.StopEditing(true);

                          break;

  

                  }

              }

  

              //allow the user to save the current document

              //DialogResult res = MessageBox.Show("Would you like to save the current document？", "AoView", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

              DialogResult res = MessageBox.Show("是否保存当前地图文档？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

              if (res == DialogResult.Yes)

              {

                  //launch the save command

                  ICommand command = new ControlsSaveAsDocCommandClass();

                  command.OnCreate(m_hookHelper.Hook);

                  command.OnClick();

              }

  

              //create a new Map

              IMap map = new MapClass();

              map.Name = "Map";

  

              //assign the new map to the MapControl

             mapControl.DocumentFilename = string.Empty;

             mapControl.Map = map;



          }
        #endregion
       }
}