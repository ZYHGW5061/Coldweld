using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WestDragon.Framework.UtilityHelper;

namespace RecipeClsLib
{
    /// <summary>
    /// Recipe根节点步骤
    /// </summary>
    public enum EnumRecipeRootStep { None, WaferInfo, MaterialInfo, BrightField, DarkField, Process }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot("RecipeBody")]
    public class ProcessRecipe
    {
        #region Recipe结构
        [XmlAttribute("MachineID")]
        public string MachineID { get; set; }

        [XmlAttribute("RecipeName")]
        public string RecipeName { get; set; }
        [XmlElement("ComponentInfos")]
        public MaterialInfos ComponentInfos { get; set; }
        [XmlElement("SubmonutInfos")]
        public MaterialInfos SubmonutInfos { get; set; }
        [XmlArray("ProcessingList"), XmlArrayItem(typeof(ProcessingStep))]
        List<ProcessingStep> ProcessingList { get; set; }
        #endregion

        /// <summary>
        /// Recipe xml完整的路径
        /// </summary>
        private static string _recipeFullName = string.Empty;

        /// <summary>
        /// Recip文件夹全路径
        /// </summary>
        public static string _recipeFolderFullName = string.Empty;

        /// <summary>
        /// Recipe文件夹全路径
        /// </summary>
        [XmlIgnore]
        public string RecipeFolderFullName
        {
            get { return _recipeFolderFullName; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcessRecipe()
        {
            RecipeName = string.Empty;
            ProcessingList = new List<ProcessingStep>();
        }

        /// <summary>
        /// 删除Recipe
        /// </summary>
        public void Delete()
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 复制Recipe
        /// </summary>
        public bool Copy(string newRecipeName)
        {
            return false;
        }

        /// <summary>
        /// 验证recipe是否完整有效
        /// </summary>
        /// <param name="recipeName"></param>
        /// <param name="waferSize"></param>
        /// <returns></returns>
        public static bool Validate(string recipeName)
        {
            return false;
        }

        /// <summary>
        /// 加载Recipe
        /// </summary>
        /// <param name="recipeFullName"></param>
        /// <returns></returns>
        public static ProcessRecipe LoadRecipe(string recipeFullName)
        {
            if (!File.Exists(recipeFullName))
            {
                throw new FileNotFoundException(string.Format("recipe {0} is not found.", recipeFullName));
            }
            ProcessRecipe loadedRecipe = new ProcessRecipe();
            //try
            //{
            //    loadedRecipe = LoadMainParameters(recipeFullName);
            //}
            //catch (Exception ex)
            //{
            //    _systemLogger.AddErrorContent(string.Format("Load Recipe {0} 信息异常", recipeFullName), ex);
            //    loadedRecipe = null;
            //}
            return loadedRecipe;
        }




        /// <summary>
        /// 保存recipe到xml文件
        /// </summary>
        public void SaveRecipe()
        {
        }
        public void SaveRecipe(EnumRecipeStep recipeStep = EnumRecipeStep.None)
        {
        }
        private static ProcessRecipe LoadMainParameters(string fullName)
        {
            return XmlSerializeHelper.XmlDeserializeFromFile<ProcessRecipe>(fullName, Encoding.UTF8);
        }

    }
    [Serializable]
    public class MaterialInfos
    {
        public MaterialInfos()
        {
            ShapeMatchConfigs = new List<ShapeMatchConfiguration>();
            CircleSearchConfigs = new List<CircleSearchConfiguration>();
            EdgeSearchConfigs = new List<EdgeSearchConfiguration>();
        }
        [XmlElement("Name")]
        public string Name { get; set; }
        #region 
        [XmlElement("WidthMM")]
        public float WidthMM { get; set; }
        [XmlElement("HeightMM")]
        public float HeightMM { get; set; }
        [XmlElement("PitchWidthMM")]
        public float PitchWidthMM { get; set; }
        [XmlElement("PitchHeightMM")]
        public float PitchHeightMM { get; set; }

        [XmlElement("ThicknessMM")]
        public float ThicknessMM { get; set; }
        [XmlElement("RowCount")]
        public int RowCount { get; set; }
        [XmlElement("ColumnCount")]
        public int ColumnCount { get; set; }
        [XmlElement("CarrierType")]
        public EnumCarrierType CarrierType { get; set; }
        [XmlElement("CarrierThickness")]
        public float CarrierThickness { get; set; }
        [XmlElement("VisionPositionintMethod")]
        public EnumVisionPositioningMethod VisionPositionintMethod { get; set; }
        [XmlElement("AccuracyMethod")]
        public EnumAccuracyMethod AccuracyMethod { get; set; }
        [XmlElement("AccuracyVisionPositionintMethod")]
        public EnumVisionPositioningMethod AccuracyVisionPositionintMethod { get; set; }
        [XmlElement("RelatedESToolName")]
        public string RelatedESToolName { get; set; }
        [XmlElement("RelatedPPToolName")]
        public string RelatedPPToolName { get; set; }

        [XmlElement("BrightFieldForLid")]
        public OpticalSettings BrightFieldSettingsForLid { get; set; }
        [XmlElement("RingLightSetting")]
        public OpticalSettings RingLightSetting { get; set; }
        [XmlElement("DirectLightSetting")]
        public OpticalSettings DirectLightSetting { get; set; }
        [XmlArray("ShapeMatchConfigs"), XmlArrayItem(typeof(ShapeMatchConfiguration))]
        public List<ShapeMatchConfiguration> ShapeMatchConfigs { get; set; }
        [XmlArray("CircleSearchConfigs"), XmlArrayItem(typeof(CircleSearchConfiguration))]
        public List<CircleSearchConfiguration> CircleSearchConfigs { get; set; }
        [XmlArray("EdgeSearchConfigs"), XmlArrayItem(typeof(EdgeSearchConfiguration))]
        public List<EdgeSearchConfiguration> EdgeSearchConfigs { get; set; }


        //Bond头相机工作高度
        //wafer相机工作高度
        //仰视相机工作高度
        //芯片吸嘴工作高度
        //substrate吸嘴工作高度
        //顶针座高度
        //顶针高度
        #endregion

        //[XmlIgnore]
        //public List<MaterialInformation> LidMapInfos { get; set; }

        //[XmlIgnore]
        //public List<MaterialInformation> ShellMapInfos { get; set; }

    }
    [Serializable]
    public class BondingPositionSettings
    {
        [XmlElement("Name")]
        public bool Name { get; set; }
    }
    [Serializable]
    public class ProcessingStep
    {
        [XmlElement("Name")]
        public bool Name { get; set; }
        [XmlElement("Index")]
        public int Index { get; set; }
        [XmlElement("BondingPositionSettingsName")]
        public string BondingPositionSettingsName { get; set; }
        [XmlElement("ComponentName")]
        public string ComponentName { get; set; }
    }
    [Serializable]
    public class OpticalSettings
    {
        [XmlAttribute("Active")]
        public bool Active { get; set; }

        [XmlAttribute("Brightness")]
        public float Brightness { get; set; }
    }
}
