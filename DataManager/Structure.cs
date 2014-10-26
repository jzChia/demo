using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Resee.DataManager
{
    public class UniqueValueRecordClass
    {
        public UniqueValueRecordClass()
        { }

        public UniqueValueRecordClass(ISymbol symbol,string value, string label)
        {
            this.symbol = symbol;
            this.value = value;
            this.label = label;
        }

        public Bitmap Bitmap
        {
            get { return Common.PreviewItem(symbol, 60, 20); }
        }

        private string value;
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private string label = "";
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private ISymbol symbol = null;
        public ISymbol Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

    }
}
