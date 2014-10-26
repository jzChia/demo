using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Demo.Entities;
using Demo.Utility;

namespace Demo
{
    public partial class Form_Search : Form
    {
        public Form_Search()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize() 
        {
            //国家
            List<Area> countries = Area.getNationList();
            foreach (Area country in countries) 
            {
                Region_Country.Items.Add(country);
            }
            Region_Country.ValueMember = "areacode";
            Region_Country.DisplayMember = "areaname";
            Region_Country.Items.Add("全部");
            Region_Country.SelectedIndex = 0;
           
            //数据源
            List<string> dsNames = Demo.Utility.DataSource.getDataSource();
            foreach (string dsName in dsNames)
            {
                DataSource.Items.Add(dsName);
            }
            DataSource.Items.Add("全部");
            DataSource.SelectedIndex = 0;
        }

        private object searchResult = null;
        public object SearchResult 
        {
            get 
            {
                return searchResult;
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            SearchConditions sc = new SearchConditions();

            string areacode = string.Empty;
            if (Region_Country.SelectedItem.GetType().Name.Equals("Area"))
            {
                areacode += ((Area)Region_Country.SelectedItem).areacode;
                if (Region_Province.SelectedItem.GetType().Name.Equals("Area"))
                {
                    areacode += ((Area)Region_Province.SelectedItem).areacode.PadRight(6, '_');
                }
                else areacode += "______";
                sc.area.Add(areacode);
            }

            if (DataSource.SelectedItem.ToString() != "全部")
                sc.datasource.Add(DataSource.SelectedItem.ToString());

            if (!String.IsNullOrEmpty(Scale_From.Text) && !String.IsNullOrEmpty(Scale_To.Text))
            {
                sc.scale.Add(Convert.ToInt64(Scale_From.Text));
                sc.scale.Add(Convert.ToInt64(Scale_To.Text));
            }

            if (!String.IsNullOrEmpty(Resolution_From.Text) && !String.IsNullOrEmpty(Resolution_To.Text))
            {
                sc.resolution.Add(Convert.ToSingle(Resolution_From.Text));
                sc.resolution.Add(Convert.ToSingle(Resolution_To.Text));
            }

            sc.keywords = Keywords.Text;

            if (DB_Raster.Checked) searchResult = RasterLayer.GetRasterLayerBySearchConditions("影像库", sc);
            else if (DB_DEM.Checked) searchResult = RasterLayer.GetRasterLayerBySearchConditions("DEM库", sc);
            else if (DB_Vector.Checked) searchResult = VectorLayer.GetVectorLayerBySearchConditions("矢量库", sc);
            else if (DB_3D.Checked) searchResult = FileLayer.GetFileLayerBySearchConditions("三维库", sc);
            else if (DB_Test.Checked) searchResult = FileLayer.GetFileLayerBySearchConditions("试验库", sc);
            else if (DB_Others.Checked) searchResult = FileLayer.GetFileLayerBySearchConditions("其他库", sc);
        }

        private void Region_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            Region_Province.Items.Clear();
            if (Region_Country.SelectedItem.GetType().Name.Equals("Area"))
            {
                Area area = (Area)Region_Country.SelectedItem;
                List<Area> provinces = Area.getProvinceList(area.areacode);
                foreach (Area province in provinces)
                {
                    Region_Province.Items.Add(province);
                }
                Region_Province.DisplayMember = "areaname";
                Region_Province.ValueMember = "areacode";
                Region_Province.Items.Add("全部");
                Region_Province.SelectedIndex = 0;
                Region_Province.Enabled = true;
            }
            else
            {
                Region_Province.Text = "全部";
                Region_Province.Enabled = false;
            }
        }
    }
}
