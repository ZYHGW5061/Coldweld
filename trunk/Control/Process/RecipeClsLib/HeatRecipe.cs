using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace RecipeClsLib
{
    [Serializable]
    [XmlRoot("RecipeBody")]
    public class HeatRecipe : ACloneable<HeatRecipe>
    {
        #region Recipe结构

        [XmlAttribute("RecipeName")]
        public string RecipeName { get; set; }

        //[XmlElement("WaferInfo")]
        //public WaferInfo WaferInfo { get; set; }
        [XmlElement("TargetTemperature")]
        public int TargetTemperature { get; set; }
        [XmlElement("HeatPreservationMinute")]
        public int HeatPreservationMinute { get; set; }
        [XmlElement("OverTemperatureThreshold")]
        public int OverTemperatureThreshold { get; set; }

        [XmlElement("OverPurgeTime")]
        public int OverPurgeTime { get; set; }
        [XmlElement("OvenPurgePressureUp")]
        public int OvenPurgePressureUp { get; set; }
        [XmlElement("OvenPurgePressureLower")]
        public int OvenPurgePressureLower { get; set; }
        [XmlElement("OvenPurgeInterval")]
        public int OvenPurgeInterval { get; set; }

        [XmlElement("IsPurge")]
        public bool IsPurge { get; set; }

        [XmlElement("OvenBoxnum")]
        public int OvenBoxnum { get; set; }


        #endregion

        /// <summary>
        /// Recipe xml完成的路径
        /// </summary>
        private static string _recipeFullName = string.Empty;

        /// <summary>
        /// Recip文件夹全路径
        /// </summary>
        public static string _recipeFolderFullName = string.Empty;

        /// <summary>
        /// Recipe存放系统默认路径
        /// </summary>
        [XmlIgnore]
        private static string SystemDefaultDirectory = SystemConfiguration.Instance.SystemDefaultDirectory;

        /// <summary>
        /// 异常日志记录器
        /// </summary>
        private static IBaseLogger _systemLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParameterSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }

        [XmlIgnore]
        public EnumRecipeType Type
        {
            get { return EnumRecipeType.Heat; }
        }
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
        public HeatRecipe()
        {
            RecipeName = string.Empty;
        }

        /// <summary>
        /// 删除Recipe
        /// </summary>
        public void Delete()
        {
            try
            {
                Directory.Delete(string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", EnumRecipeType.Heat.ToString(), this.RecipeName), true);
            }
            catch (Exception ex)
            {
                _systemLogger.AddErrorContent(string.Format("Delete Recipe {0} 信息异常", this.RecipeName), ex);
            }
        }

        /// <summary>
        /// 复制Recipe
        /// </summary>
        public bool Copy(string newRecipeName)
        {
            try
            {
                if (FileOperationHelper.CopyDirectory(string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", EnumRecipeType.Heat.ToString(), this.RecipeName),
                    string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", EnumRecipeType.Heat.ToString(), newRecipeName)))
                {
                    var srcFileName = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}\{2}.xml", EnumRecipeType.Heat.ToString(), newRecipeName, RecipeName);
                    var dstFileName = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}\{2}.xml", EnumRecipeType.Heat.ToString(), newRecipeName, newRecipeName);
                    if (File.Exists(srcFileName))
                    {
                        File.Move(srcFileName, dstFileName);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _systemLogger.AddErrorContent(string.Format("Copy Recipe {0} Error!", this.RecipeName), ex);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 验证recipe是否完整有效
        /// </summary>
        /// <param name="recipeName"></param>
        /// <param name="waferSize"></param>
        /// <returns></returns>
        public static bool Validate(string recipeName, EnumRecipeType recipeType)
        {
            var recipe = LoadRecipe(recipeName, recipeType);
            if (recipe != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 加载Recipe
        /// </summary>
        /// <param name="recipeFullName"></param>
        /// <returns></returns>
        public static HeatRecipe LoadRecipe(string recipeFullName)
        {
            if (!File.Exists(recipeFullName))
            {
                throw new FileNotFoundException(string.Format("recipe {0} is not found.", recipeFullName));
            }
            HeatRecipe loadedRecipe = new HeatRecipe();
            try
            {
                loadedRecipe = LoadMainParameters(recipeFullName);
                _recipeFullName = recipeFullName;
            }
            catch (Exception ex)
            {
                _systemLogger.AddErrorContent(string.Format("Load Recipe {0} 信息异常", recipeFullName), ex);
                loadedRecipe = null;
            }
            return loadedRecipe;
        }

        /// <summary>
        /// 加载Recipe
        /// </summary>
        public static HeatRecipe LoadRecipe(string recipeName, EnumRecipeType recipeType, string recipePath = null)
        {
            var recipeDirectory = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", recipeType.ToString(), recipeName);
            if (!Directory.Exists(recipePath ?? recipeDirectory))
            {
                throw new FileNotFoundException(string.Format("recipe {0} is not found.", recipeName));
            }
            _recipeFolderFullName = recipeDirectory;
            _recipeFullName = string.Format(recipeDirectory + @"\{0}.xml", recipeName);
            HeatRecipe loadedRecipe = new HeatRecipe();
            try
            {
                loadedRecipe = LoadMainParameters(_recipeFullName);
            }
            catch (Exception ex)
            {
                _systemLogger.AddErrorContent(string.Format("Load Recipe {0} 信息异常", recipeName), ex);
                loadedRecipe = null;
            }
            return loadedRecipe;
        }
        public static HeatRecipe CreateRecipe(string recipeName, EnumRecipeType recipeType)
        {
            var recipeDirectory = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", recipeType.ToString(), recipeName);
            CommonProcess.EnsureFolderExist(recipeDirectory);
            _recipeFolderFullName = recipeDirectory;
            _recipeFullName = string.Format(recipeDirectory + @"\{0}.xml", recipeName);
            HeatRecipe loadedRecipe = new HeatRecipe();
            return loadedRecipe;
        }
        /// <summary>

        /// 保存recipe到xml文件中
        /// </summary>
        /// <param name="recipeFullName"></param>
        public void SaveRecipe(string recipeFullName, string recipeFullFolder = "", EnumRecipeStep recipeStep = EnumRecipeStep.None)
        {
            if (string.IsNullOrEmpty(recipeFullName))
            {
                throw new Exception("Recipe full name can't be empty or null.");
            }
            if (!string.IsNullOrEmpty(recipeFullName))
            {
                _recipeFullName = recipeFullName;
            }
            if (!string.IsNullOrEmpty(recipeFullFolder))
            {
                _recipeFolderFullName = recipeFullFolder;
            }
            SaveRecipe(recipeStep);
        }

        /// <summary>
        /// 保存recipe到xml文件
        /// </summary>
        public void SaveRecipe(EnumRecipeStep recipeStep = EnumRecipeStep.None)
        {
            SaveMianParameters();
        }

        private static HeatRecipe LoadMainParameters(string fullName)
        {
            return XmlSerializeHelper.XmlDeserializeFromFile<HeatRecipe>(fullName, Encoding.UTF8);
        }
        /// <summary>
        /// 保存主要参数，保存其他如Map数据需单独调用其他方法
        /// </summary>
        private void SaveMianParameters()
        {
            XmlSerializeHelper.XmlSerializeToFile(this, _recipeFullName, Encoding.UTF8);
        }
    }
}
