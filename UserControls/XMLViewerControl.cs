using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UserControls
{
    public partial class XMLViewerControl : UserControl
    {

        public XmlDocument xmlDoc = new XmlDocument();
        public XMLViewerControl()
        {
            InitializeComponent();
        }

        public void LoadXML(string xmlPath) 
        {
            xmlDoc.Load(xmlPath);
            XmlNodeList childNodes = xmlDoc.ChildNodes;
            XmlNodeList cns = childNodes[1].ChildNodes;

            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Value");

            for (int i = 0; i < cns.Count; i++) 
            {
                DataRow dr = dt.NewRow();
                dr["Key"] = cns[i].Name;
                dr["Value"] = cns[i].InnerText;
                dt.Rows.Add(dr);
            }
            treeList1.DataSource = dt;
        }
    }
}
