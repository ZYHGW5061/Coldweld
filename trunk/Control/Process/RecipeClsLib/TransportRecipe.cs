using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
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
    public class TransportRecipe
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public TransportRecipe()
        {
            RecipeName = string.Empty;


            OverBox1Param = new EnumOverBoxParam();
            OverBox2Param = new EnumOverBoxParam();

            MaterialboxHookSafePosition = new XYZTCoordinateConfig();
            MaterialboxHooktoMaterialboxPosition1 = new XYZTCoordinateConfig();
            MaterialboxHooktoMaterialboxPosition2 = new XYZTCoordinateConfig();
            MaterialboxHookPickupMaterialboxPosition = new XYZTCoordinateConfig();
            MaterialboxHookPickupMaterialboxPosition2 = new XYZTCoordinateConfig();
            MaterialboxHooktoTarget1Position = new XYZTCoordinateConfig();
            MaterialboxHooktoTarget2Position = new XYZTCoordinateConfig();
            MaterialboxHooktoTarget3Position = new XYZTCoordinateConfig();
            MaterialboxHooktoTarget4Position = new XYZTCoordinateConfig();
            MaterialboxHooktoWeldPosition = new XYZTCoordinateConfig();
            MaterialboxHookPutdownMaterialboxPosition = new XYZTCoordinateConfig();

            MaterialHookSafePosition = new XYZTCoordinateConfig();
            MaterialHooktoMaterialPosition = new List<XYZTCoordinateConfig>();
            MaterialHookPickupMaterialPosition = new XYZTCoordinateConfig();
            MaterialHooktoTargetPosition = new List<XYZTCoordinateConfig>();
            MaterialHookPutdownMaterialPosition = new XYZTCoordinateConfig();

            

        }


        #region Recipe结构

        [XmlAttribute("RecipeName")]
        public string RecipeName { get; set; }


        #region 物料属性


        /// <summary>
        /// 烘箱1参数
        /// </summary>
        [XmlElement("OverBox1Param")]
        public EnumOverBoxParam OverBox1Param { get; set; }

        /// <summary>
        /// 烘箱2参数
        /// </summary>
        [XmlElement("OverBox2Param")]
        public EnumOverBoxParam OverBox2Param { get; set; }



        #endregion




        #region 料盒搬送


        //料盒钩爪到空闲位置

        /// <summary>
        /// 料盒钩爪空闲位置
        /// </summary>
        [XmlElement("MaterialboxHookSafePosition")]
        public XYZTCoordinateConfig MaterialboxHookSafePosition { get; set; }

        /// <summary>
        /// 料盒钩爪开
        /// </summary>
        [XmlElement("MaterialboxHookOpen")]
        public float MaterialboxHookOpen { get; set; }


        //料盒出烘箱

        /// <summary>
        /// 料盒出烘箱位置
        /// </summary>
        [XmlElement("OverTrackMaterialboxOutofoven")]
        public float OverTrackMaterialboxOutofoven { get; set; }

        /// <summary>
        /// 料盒出烘箱2位置
        /// </summary>
        [XmlElement("OverTrack2MaterialboxOutofoven")]
        public float OverTrack2MaterialboxOutofoven { get; set; }


        //料盒钩爪到料盒上方

        /// <summary>
        /// 料盒钩爪到料盒上方
        /// </summary>
        [XmlElement("MaterialboxHooktoMaterialboxPosition1")]
        public XYZTCoordinateConfig MaterialboxHooktoMaterialboxPosition1 { get; set; }

        /// <summary>
        /// 料盒钩爪到料盒上方
        /// </summary>
        [XmlElement("MaterialboxHooktoMaterialboxPosition2")]
        public XYZTCoordinateConfig MaterialboxHooktoMaterialboxPosition2 { get; set; }


        //料盒钩爪拾起料盒

        /// <summary>
        /// 料盒钩爪拾起料盒位置
        /// </summary>
        [XmlElement("MaterialboxHookPickupMaterialboxPosition")]
        public XYZTCoordinateConfig MaterialboxHookPickupMaterialboxPosition { get; set; }

        /// <summary>
        /// 料盒钩爪拾起料盒位置
        /// </summary>
        [XmlElement("MaterialboxHookPickupMaterialboxPosition2")]
        public XYZTCoordinateConfig MaterialboxHookPickupMaterialboxPosition2 { get; set; }

        /// <summary>
        /// 料盒钩爪合
        /// </summary>
        [XmlElement("MaterialboxHookClose")]
        public float MaterialboxHookClose { get; set; }

        /// <summary>
        /// 料盒钩爪合
        /// </summary>
        [XmlElement("MaterialboxHookClose2")]
        public float MaterialboxHookClose2 { get; set; }

        /// <summary>
        /// 料盒抬升距离
        /// </summary>
        [XmlElement("MaterialboxHookUp")]
        public float MaterialboxHookUp { get; set; }

        /// <summary>
        /// 料盒抬升距离
        /// </summary>
        [XmlElement("MaterialboxHookUp2")]
        public float MaterialboxHookUp2 { get; set; }


        //料盒钩爪到目标位置

        /// <summary>
        /// 料盒钩爪到目标位置1
        /// </summary>
        [XmlElement("MaterialboxHooktoTarget1Position")]
        public XYZTCoordinateConfig MaterialboxHooktoTarget1Position { get; set; }

        /// <summary>
        /// 料盒钩爪到目标位置2
        /// </summary>
        [XmlElement("MaterialboxHooktoTarget2Position")]
        public XYZTCoordinateConfig MaterialboxHooktoTarget2Position { get; set; }

        /// <summary>
        /// 料盒钩爪到目标位置3
        /// </summary>
        [XmlElement("MaterialboxHooktoTarget3Position")]
        public XYZTCoordinateConfig MaterialboxHooktoTarget3Position { get; set; }

        /// <summary>
        /// 料盒钩爪到目标位置4
        /// </summary>
        [XmlElement("MaterialboxHooktoTarget4Position")]
        public XYZTCoordinateConfig MaterialboxHooktoTarget4Position { get; set; }


        //料盒钩爪到焊接位置

        /// <summary>
        /// 料盒钩爪到目标位置
        /// </summary>
        [XmlElement("MaterialboxHooktoWeldPosition")]
        public XYZTCoordinateConfig MaterialboxHooktoWeldPosition { get; set; }


        //料盒钩爪放下料盒

        /// <summary>
        /// 料盒钩爪放下料盒位置
        /// </summary>
        [XmlElement("MaterialboxHookPutdownMaterialboxPosition")]
        public XYZTCoordinateConfig MaterialboxHookPutdownMaterialboxPosition { get; set; }


        //料盒进烘箱

        /// <summary>
        /// 料盒进烘箱位置
        /// </summary>
        [XmlElement("OverTrack1MaterialboxInofoven")]
        public float OverTrack1MaterialboxInofoven { get; set; }

        /// <summary>
        /// 料盒进烘箱2位置
        /// </summary>
        [XmlElement("OverTrack2MaterialboxInofoven")]
        public float OverTrack2MaterialboxInofoven { get; set; }


        #endregion

        #region 物料搬送


        //物料钩爪到空闲位置

        /// <summary>
        /// 物料钩爪到空闲位置
        /// </summary>
        [XmlElement("MaterialHookSafePosition")]
        public XYZTCoordinateConfig MaterialHookSafePosition { get; set; }

        /// <summary>
        /// 物料钩爪开
        /// </summary>
        [XmlElement("MaterialHookOpen")]
        public float MaterialHookOpen { get; set; }


        //物料钩爪到物料上方

        /// <summary>
        /// 物料钩爪到物料上方
        /// </summary>
        [XmlArray("MaterialHooktoMaterialPosition"), XmlArrayItem(typeof(XYZTCoordinateConfig))]
        public List<XYZTCoordinateConfig> MaterialHooktoMaterialPosition { get; set; }


        //物料钩爪拾起物料

        /// <summary>
        /// 物料钩爪拾起物料位置
        /// </summary>
        [XmlElement("MaterialHookPickupMaterialPosition")]
        public XYZTCoordinateConfig MaterialHookPickupMaterialPosition { get; set; }

        /// <summary>
        /// 物料钩爪合
        /// </summary>
        [XmlElement("MaterialHookClose")]
        public float MaterialHookClose { get; set; }

        /// <summary>
        /// 物料抬升距离
        /// </summary>
        [XmlElement("MaterialHookUp")]
        public float MaterialHookUp { get; set; }

        /// <summary>
        /// 物料补偿角度(左下角物料为基准，料盘顺时针旋转为正)
        /// </summary>
        [XmlElement("MaterialCompensationAngle")]
        public float MaterialCompensationAngle { get; set; }
        /// <summary>
        /// 物料补偿高度(左下角物料为基准，向下是负)
        /// </summary>
        [XmlElement("MaterialCompensationZ")]
        public float MaterialCompensationZ { get; set; }


        //物料钩爪到目标位置

        /// <summary>
        /// 膜具配方
        /// </summary>
        [XmlElement("FixtureRecipeName")]
        public string FixtureRecipeName { get; set; }

        /// <summary>
        /// 焊台物料个数
        /// </summary>
        [XmlElement("WeldNum")]
        public int WeldNum { get; set; }

        /// <summary>
        /// 物料钩爪到目标位置
        /// </summary>
        [XmlArray("MaterialHooktoTargetPosition"), XmlArrayItem(typeof(XYZTCoordinateConfig))]
        public List<XYZTCoordinateConfig> MaterialHooktoTargetPosition { get; set; }


        //物料钩爪放下物料

        /// <summary>
        /// 物料钩爪放下物料位置
        /// </summary>
        [XmlElement("MaterialHookPutdownMaterialPosition")]
        public XYZTCoordinateConfig MaterialHookPutdownMaterialPosition { get; set; }

        /// <summary>
        /// 物料抬升距离
        /// </summary>
        [XmlElement("MaterialHookUp2")]
        public float MaterialHookUp2 { get; set; }

        /// <summary>
        /// 顶升机构到空闲位置
        /// </summary>
        [XmlElement("PressliftingSafePosition")]
        public float PressliftingSafePosition { get; set; }

        /// <summary>
        /// 顶升机构到工作位置
        /// </summary>
        [XmlElement("PressliftingWorkPosition")]
        public float PressliftingWorkPosition { get; set; }



        #endregion

        #region 物料焊接

        /// <summary>
        /// 物料焊接时间
        /// </summary>
        [XmlElement("WeldTime")]
        public float WeldTime { get; set; }

        /// <summary>
        /// 物料焊接压力
        /// </summary>
        [XmlElement("WeldPessure")]
        public float WeldPessure { get; set; }



        #endregion


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
        /// 删除Recipe
        /// </summary>
        public void Delete()
        {
            try
            {
                Directory.Delete(string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", EnumRecipeType.Transport.ToString(), this.RecipeName), true);
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
                if (FileOperationHelper.CopyDirectory(string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", EnumRecipeType.Transport.ToString(), this.RecipeName),
                    string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", EnumRecipeType.Transport.ToString(), newRecipeName)))
                {
                    var srcFileName = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}\{2}.xml", EnumRecipeType.Transport.ToString(), newRecipeName, RecipeName);
                    var dstFileName = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}\{2}.xml", EnumRecipeType.Transport.ToString(), newRecipeName, newRecipeName);
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
        public static TransportRecipe LoadRecipe(string recipeFullName)
        {
            if (!File.Exists(recipeFullName))
            {
                throw new FileNotFoundException(string.Format("recipe {0} is not found.", recipeFullName));
            }
            TransportRecipe loadedRecipe = new TransportRecipe();
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
        public static TransportRecipe LoadRecipe(string recipeName, EnumRecipeType recipeType, string recipePath = null)
        {
            var recipeDirectory = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", recipeType.ToString(), recipeName);
            if (!Directory.Exists(recipePath ?? recipeDirectory))
            {
                throw new FileNotFoundException(string.Format("recipe {0} is not found.", recipeName));
            }
            _recipeFolderFullName = recipeDirectory;
            _recipeFullName = string.Format(recipeDirectory + @"\{0}.xml", recipeName);
            TransportRecipe loadedRecipe = new TransportRecipe();
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

        public static TransportRecipe CreateRecipe(string recipeName, EnumRecipeType recipeType)
        {
            var recipeDirectory = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", recipeType.ToString(), recipeName);
            CommonProcess.EnsureFolderExist(recipeDirectory);
            _recipeFolderFullName = recipeDirectory;
            _recipeFullName = string.Format(recipeDirectory + @"\{0}.xml", recipeName);
            TransportRecipe loadedRecipe = new TransportRecipe();
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

        private static TransportRecipe LoadMainParameters(string fullName)
        {
            return XmlSerializeHelper.XmlDeserializeFromFile<TransportRecipe>(fullName, Encoding.UTF8);
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
