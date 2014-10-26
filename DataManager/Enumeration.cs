using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resee.DataManager
{
    public enum ImportDataType
    {
        Data5W = 0,
        DataWaterProject,
        DataDangerEvaluate
    }

    public enum TextElementEditType
    {
        New = 0,//新建
        Modify//编辑
    }
    /// <summary>
    /// 风险方案类型
    /// </summary>
    public enum RiskSchemeType
    {
        None = 0,
        Ddsj,//到达时间
        Hsls,//洪水流速
        Ymfw,//淹没范围
        Ymls,//淹没历时
        Ymss//淹没水深
    }
    /// <summary>
    /// 风险图层类型
    /// </summary>
    public enum RiskLayerType
    {
        None = 0,
        FeatureLayer,
        RasterLayer
    }

    public enum LayerType
    {
        FeatureLayer = 0,
        RasterLayer
    }
    /// <summary>
    /// 颜色带优先权枚举，当枚举值为“随机颜色带优先”时随机颜色带优先列出，而当枚举值为“分级颜色带优先”时渐变颜色带优先列出
    /// </summary>
    public enum ColorRampsPriority
    {
        /// <summary>
        /// 随机颜色带优先
        /// </summary>
        RandomColorRamps = 0,
        /// <summary>
        /// 分级颜色带优先
        /// </summary>
        GradualColorRamps
    }

    public enum RandomColorStatus
    {
        /// <summary>
        /// HSV颜色
        /// </summary>
        HsvColor = 0,
        /// <summary>
        /// 随机颜色
        /// </summary>
        ColorRamp
    }

    public enum ValueType
    {
        String = 0,
        Int
    }
}
