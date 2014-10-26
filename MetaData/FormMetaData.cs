using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetaData
{
    public partial class FormMetaData : Form
    {
        public string DataSource = "";
        public ILayer pLayer;

        public FormMetaData()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }

        private void Base2Control(BaseLayer BL)
        {
            if (BL == null) return;
            textBoxAuthor.Text = BL.AUTHOR;
            textBoxCreateTime.Text = BL.CREATETIME;
            textBoxDataType.Text = BL.DATATYPE;
            textBoxDescription.Text = BL.DESCRIPTION;
            textBoxLayerName.Text = BL.LAYERNAME;
            textBoxMapNum.Text = BL.MAPNUM;
            textBoxProjection.Text = BL.PROJECTION;
        }

        private void Control2Base(out RasterLayer BL)
        {
            BL = new RasterLayer();
            BL.AUTHOR = textBoxAuthor.Text;
            BL.CREATETIME = textBoxCreateTime.Text;
            BL.DATATYPE = textBoxDataType.Text;
            BL.DESCRIPTION = textBoxDescription.Text;
            BL.LAYERNAME = textBoxLayerName.Text;
            BL.MAPNUM = textBoxMapNum.Text;
            BL.PROJECTION = textBoxProjection.Text;
        }
        private void Control2Base(out VectorLayer BL)
        {
            BL = new VectorLayer();
            BL.AUTHOR = textBoxAuthor.Text;
            BL.CREATETIME = textBoxCreateTime.Text;
            BL.DATATYPE = textBoxDataType.Text;
            BL.DESCRIPTION = textBoxDescription.Text;
            BL.LAYERNAME = textBoxLayerName.Text;
            BL.MAPNUM = textBoxMapNum.Text;
            BL.PROJECTION = textBoxProjection.Text;
        }
        private void Control2Base(out FileLayer BL)
        {
            BL = new FileLayer();
            BL.AUTHOR = textBoxAuthor.Text;
            BL.CREATETIME = textBoxCreateTime.Text;
            BL.DATATYPE = textBoxDataType.Text;
            BL.DESCRIPTION = textBoxDescription.Text;
            BL.LAYERNAME = textBoxLayerName.Text;
            BL.MAPNUM = textBoxMapNum.Text;
            BL.PROJECTION = textBoxProjection.Text;
        }

        private void Raster2Control(RasterLayer RL)
        {
            Base2Control((BaseLayer)RL);

            RL.BANDCOUNT = Convert.ToInt64(textBoxRBandCount.Text);
            RL.HEIGHT = Convert.ToInt64(textBoxRHeight.Text);
            RL.ISVISIBLE = Convert.ToBoolean(textBoxRVisible.Text);
            RL.MAXX = Convert.ToSingle(textBoxRMaxX.Text);
            RL.MAXY = Convert.ToSingle(textBoxRMaxY.Text);
            RL.MINX = Convert.ToSingle(textBoxRMinX.Text);
            RL.MINY = Convert.ToSingle(textBoxRMinY.Text);
            RL.NODATAVALUE = Convert.ToSingle(textBoxRNoDataValue.Text);
            RL.WIDTH = Convert.ToInt64(textBoxRWidth.Text);
            RL.RESOLUTION = Convert.ToSingle(textBoxRResolution.Text);
        }

        private void Control2Raster(out RasterLayer RL)
        {
            RL = new RasterLayer();
            Control2Base(out RL);
            if (RL == null) return;

            RL.BANDCOUNT = Convert.ToInt64(textBoxRBandCount.Text);
            RL.HEIGHT = Convert.ToInt64(textBoxRHeight.Text);
            RL.ISVISIBLE = Convert.ToBoolean(textBoxRVisible.Text);
            RL.MAXX = Convert.ToSingle(textBoxRMaxX.Text);
            RL.MAXY = Convert.ToSingle(textBoxRMaxY.Text);
            RL.MINX = Convert.ToSingle(textBoxRMinX.Text);
            RL.MINY = Convert.ToSingle(textBoxRMinY.Text);
            RL.NODATAVALUE = Convert.ToSingle(textBoxRNoDataValue.Text);
            RL.WIDTH = Convert.ToInt64(textBoxRWidth.Text);
            RL.RESOLUTION = Convert.ToSingle(textBoxRResolution.Text);
        }

        private void Vector2Control(VectorLayer VL)
        { 
        
        }

        private void Control2Vector(out VectorLayer VL)
        {
            VL = new VectorLayer();
        }

        private void File2Control(FileLayer FL)
        { 
        
        }

        private void Control2File(out FileLayer FL)
        {
            FL = new FileLayer();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            RasterLayer RLTest = new RasterLayer();

            Control2Raster(out RLTest);

            MessageBox.Show(RLTest.LAYERNAME);
        }
    }
}
