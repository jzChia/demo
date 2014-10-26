using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Resee.DataManager
{
    public class Common
    {
        /// <summary>
        /// 实现将Symbol转换为Bitmap
        /// </summary>
        /// <param name="Symbol">符号对象</param>
        /// <param name="Width">宽度值</param>
        /// <param name="Height">高度值</param>
        /// <returns>图像对象</returns>
        public static Bitmap PreviewItem(ISymbol Symbol, int Width, int Height)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            Graphics mGraphics = Graphics.FromImage(bitmap);
            double dpi = mGraphics.DpiY;
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(0, 0, bitmap.Width, bitmap.Height);
            tagRECT myRect = new tagRECT();
            myRect.bottom = bitmap.Height;
            myRect.left = 0;
            myRect.right = bitmap.Width;
            myRect.top = 0;
            IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
            pDisplayTransformation.VisibleBounds = pEnvelope;
            pDisplayTransformation.Bounds = pEnvelope;
            pDisplayTransformation.set_DeviceFrame(ref myRect);
            pDisplayTransformation.Resolution = dpi;
            IntPtr pIntPtr = mGraphics.GetHdc();
            int hDC = pIntPtr.ToInt32();
            Symbol.SetupDC(hDC, pDisplayTransformation);
            IGeometry pGeometry = GetSymbolGeometry(Symbol, pEnvelope);
            Symbol.Draw(pGeometry);
            Symbol.ResetDC();
            mGraphics.ReleaseHdc(pIntPtr);
            mGraphics.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 根据符号类型得到相应的几何形体
        /// </summary>
        /// <param name="Symbol">符号对象</param>
        /// <param name="Envelop">矩形对象</param>
        /// <returns>几何形体对象</returns>
        private static IGeometry GetSymbolGeometry(ISymbol Symbol, IEnvelope Envelop)
        {
            IGeometry pGeometry = null;
            if (Symbol is IMarkerSymbol)
            {
                IArea pArea = Envelop as IArea;
                pGeometry = pArea.Centroid;
            }
            else if (Symbol is ILineSymbol)
            {
                IPolyline pPolyline = new PolylineClass();
                IPoint pFromPoint = new PointClass();
                pFromPoint.PutCoords(Envelop.XMin, (Envelop.YMax + Envelop.YMin) / 2);
                IPoint pToPoint = new PointClass();
                pToPoint.PutCoords(Envelop.XMax, (Envelop.YMax + Envelop.YMin) / 2);
                pPolyline.FromPoint = pFromPoint;
                pPolyline.ToPoint = pToPoint;
                pGeometry = pPolyline;
            }
            else if (Symbol is IFillSymbol)
            {
                pGeometry = Envelop;
            }
            else if (Symbol is ITextSymbol)
            {
                IArea pArea = Envelop as IArea;
                pGeometry = pArea.Centroid;
            }
            return pGeometry;
        }

    }
}
