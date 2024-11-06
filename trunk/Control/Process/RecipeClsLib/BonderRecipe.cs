using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
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
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot("RecipeBody")]
    public class BondRecipe
    {
        #region Recipe结构
        [XmlAttribute("MachineID")]
        public string MachineID { get; set; }

        [XmlAttribute("RecipeName")]
        public string RecipeName { get; set; }
        //[XmlArray("ComponentList"), XmlArrayItem(typeof(MaterialSettings))]
        [XmlIgnore]
        public List<MaterialSettings> ComponentList { get; set; }
        //[XmlElement("ComponentInfos")]
        //public MaterialInfos ComponentInfos { get; set; }
        [XmlElement("SubmonutInfos")]
        public MaterialSettings SubmonutInfos { get; set; }
        //[XmlArray("BondingPositionList"), XmlArrayItem(typeof(BondingPositionSettings))]
        [XmlIgnore]
        public List<BondingPositionSettings> BondingPositionList { get; set; }
        //[XmlElement("BondingPositionSettings")]
        //public BondingPositionSettings BondingPositionSettings { get; set; }

        [XmlElement("EutecticParameters")]
        public EutecticParameters EutecticParameters { get; set; }
        
        [XmlArray("ProcessingList"), XmlArrayItem(typeof(ProcessingStep))]
        public List<ProcessingStep> ProcessingList { get; set; }

        
        [XmlElement("BlankingParameters")]
        public BlankingParameters BlankingParameters { get; set; }
        #endregion
        /// <summary>
        /// Recipe存放系统默认路径
        /// </summary>
        [XmlIgnore]
        private static string SystemDefaultDirectory = SystemConfiguration.Instance.SystemDefaultDirectory;
        [XmlIgnore]
        private static string _componentsSavePath = string.Format(@"{0}Recipes\{1}\Components\", SystemDefaultDirectory, EnumRecipeType.Bonder.ToString());
        [XmlIgnore]
        private static string _bondPositionSavePath = string.Format(@"{0}Recipes\{1}\BondPositions\", SystemDefaultDirectory, EnumRecipeType.Bonder.ToString());
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
        [XmlIgnore]
        public static EnumRecipeType RecipeType
        {
            get { return EnumRecipeType.Bonder; }
        }
        [XmlIgnore]
        public string CurrentPositionSettingsName { get; set; }
        [XmlIgnore]
        public string CurrentComponentInfosName { get; set; }
        [XmlIgnore]
        public MaterialSettings CurrentComponent
        {
            get
            {
                MaterialSettings ret = null;
                if (!string.IsNullOrEmpty(CurrentComponentInfosName))
                {
                    ret = ComponentList.FirstOrDefault(i => i.Name == CurrentComponentInfosName);
                }
                return ret;
            }
        }
        [XmlIgnore]
        public BondingPositionSettings CurrentBondPosition
        {
            get
            {
                BondingPositionSettings ret = null;
                if (!string.IsNullOrEmpty(CurrentPositionSettingsName))
                {
                    ret = BondingPositionList.FirstOrDefault(i => i.Name == CurrentPositionSettingsName);
                }
                return ret;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BondRecipe()
        {
            RecipeName = string.Empty;
            ComponentList = new List<MaterialSettings>();
            //ComponentInfos = new MaterialInfos();
            SubmonutInfos = new MaterialSettings();
            BondingPositionList = new List<BondingPositionSettings>();
            //BondingPositionSettings = new BondingPositionSettings();
            EutecticParameters = new EutecticParameters();
            ProcessingList = new List<ProcessingStep>();
            BlankingParameters = new BlankingParameters();
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
        public static BondRecipe LoadRecipe(string recipeName)
        {
            var recipeDirectory = string.Format(SystemDefaultDirectory + @"Recipes\{0}\{1}", RecipeType.ToString(), recipeName);
            _recipeFullName = string.Format(recipeDirectory + @"\{0}.xml", recipeName);
            _recipeFolderFullName = _recipeFullName.Substring(0, _recipeFullName.LastIndexOf("\\"));
            if (!File.Exists(_recipeFullName))
            {
                throw new FileNotFoundException(string.Format("recipe {0} is not found.", _recipeFullName));
            }
            BondRecipe loadedRecipe = new BondRecipe();
            try
            {
                loadedRecipe = LoadMainParameters(_recipeFullName);
                //此处需根据ProcessList加载Component和BondPosition
                //loadedRecipe.ComponentInfos.ComponentMapInfos = LoadComponentMap();
                loadedRecipe.SubmonutInfos.SubmountMapInfos = LoadSubmountMap();
                loadedRecipe.ComponentList = LoadComponents(loadedRecipe.RecipeName);
                loadedRecipe.BondingPositionList = LoadBondPositions(loadedRecipe.RecipeName);
                //foreach (var item in loadedRecipe.ComponentList)
                //{
                //    item.ComponentMapInfos = LoadComponentMap(item.Name);
                //}
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error,string.Format("Load Recipe {0} 信息异常", _recipeFullName), ex);
                loadedRecipe = null;
            }
            return loadedRecipe;
        }



        /// <summary>
        /// 保存recipe到xml文件
        /// </summary>
        //public void SaveRecipe()
        //{
        //}

        public void NewRecipe(string fullFileName,EnumRecipeStep recipeStep = EnumRecipeStep.None)
        {
            _recipeFullName = fullFileName;
            _recipeFolderFullName = fullFileName.Substring(0, fullFileName.LastIndexOf("\\"));
            SaveRecipe(recipeStep);
        }
        public void SaveRecipe(EnumRecipeStep recipeStep = EnumRecipeStep.None)
        {
            SaveMianParameters();
            switch (recipeStep)
            {
                case EnumRecipeStep.Create:
                    break;
                case EnumRecipeStep.Configuration:
                    break;
                case EnumRecipeStep.Submount_InformationSettings:
                    break;
                case EnumRecipeStep.Submount_PositionSettings:
                    break;
                case EnumRecipeStep.Submount_MaterialMap:
                    SaveSubmountMap();
                    break;
                case EnumRecipeStep.Submount_PPSettings:
                    break;
                case EnumRecipeStep.Submount_Accuracy:
                    break;
                case EnumRecipeStep.BondPosition:
                    SaveBondPosition();
                    break;
                case EnumRecipeStep.Component_InformationSettings:
                    SaveComponent();
                    break;
                case EnumRecipeStep.Component_PositionSettings:
                    SaveComponent();
                    break;
                case EnumRecipeStep.Component_MaterialMap:
                    SaveComponent();
                    break;
                case EnumRecipeStep.Component_PPSettings:
                    SaveComponent();
                    break;
                case EnumRecipeStep.Component_Accuracy:
                    SaveComponent();
                    break;
                case EnumRecipeStep.EutecticSettings:
                    break;
                case EnumRecipeStep.ProcessList:
                    break;
                case EnumRecipeStep.BlankingSetting:
                    break;
                case EnumRecipeStep.None:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 保存主要参数，保存其他如Map数据需单独调用其他方法
        /// </summary>
        private void SaveMianParameters()
        {
            XmlSerializeHelper.XmlSerializeToFile(this, _recipeFullName, Encoding.UTF8);

        }
        private static BondRecipe LoadMainParameters(string fullName)
        {
            var ret= XmlSerializeHelper.XmlDeserializeFromFile<BondRecipe>(fullName, Encoding.UTF8);

            return ret;
        }
        public static List<MaterialSettings> LoadComponents(List<ProcessingStep> ProcessingList)
        {
            var ret = new List<MaterialSettings>();
            foreach (var item in ProcessingList)
            {
                var xmlFile = $@"{_componentsSavePath}\{item.RelatedComponentName}.xml";
                var comp = XmlSerializeHelper.XmlDeserializeFromFile<MaterialSettings>(xmlFile, Encoding.UTF8);
                comp.ComponentMapInfos = LoadComponentMap(item.RelatedComponentName);
                ret.Add(comp);
            }
            return ret;
        }
        public static List<BondingPositionSettings> LoadBondPositions(List<ProcessingStep> ProcessingList)
        {
            var ret = new List<BondingPositionSettings>();
            foreach (var item in ProcessingList)
            {
                var xmlFile = $@"{_bondPositionSavePath}\{item.BondingPositionSettingsName}.xml";
                var bp = XmlSerializeHelper.XmlDeserializeFromFile<BondingPositionSettings>(xmlFile, Encoding.UTF8);
                ret.Add(bp);
            }
            return ret;
        }
        public static List<MaterialSettings> LoadComponents(string recipeName)
        {
            var ret = new List<MaterialSettings>();
            var childFiles = Directory.GetDirectories(_componentsSavePath);
            for (int index = 0; index < childFiles.Length; index++)
            {
                var fileName = Path.GetFileName(childFiles[index]);
                if (fileName.EndsWith($"_{recipeName}"))
                {
                    var xmlFile = $@"{_componentsSavePath}\{fileName}\{fileName}.xml";
                    var comp = XmlSerializeHelper.XmlDeserializeFromFile<MaterialSettings>(xmlFile, Encoding.UTF8);
                    comp.ComponentMapInfos = LoadComponentMap(fileName);
                    ret.Add(comp);
                }
            }

            return ret;
        }
        public static List<BondingPositionSettings> LoadBondPositions(string recipeName)
        {
            var ret = new List<BondingPositionSettings>();
            var childDir = Directory.GetDirectories(_bondPositionSavePath);
            for (int index = 0; index < childDir.Length; index++)
            {
                var fileName = Path.GetFileName(childDir[index]);
                if (fileName.EndsWith($"_{recipeName}"))
                {

                    var xmlFile = $@"{_bondPositionSavePath}{fileName}\{fileName}.xml";
                    var bp = XmlSerializeHelper.XmlDeserializeFromFile<BondingPositionSettings>(xmlFile, Encoding.UTF8);
                    ret.Add(bp);
                }

            }

            return ret;
        }
        private void SaveComponent()
        {
            if (CurrentComponent != null)
            {
                var xmlFile = $@"{_componentsSavePath}{CurrentComponent.Name}\{CurrentComponent.Name}.xml";
                XmlSerializeHelper.XmlSerializeToFile(CurrentComponent, xmlFile, Encoding.UTF8);
                SaveComponentMap();
            }
        }
        private void SaveComponentMap()
        {
            if (CurrentComponent != null)
            {
                var xmlFile = $@"{_componentsSavePath}{CurrentComponent.Name}\ComponentMap_{CurrentComponentInfosName}.xml";
                XmlSerializeHelper.XmlSerializeToFile(CurrentComponent.ComponentMapInfos, xmlFile, Encoding.UTF8);
            }
        }
        private void SaveBondPosition()
        {
            if (CurrentBondPosition != null)
            {
                var xmlFile = $@"{_bondPositionSavePath}{CurrentBondPosition.Name}\{CurrentBondPosition.Name}.xml";
                XmlSerializeHelper.XmlSerializeToFile(CurrentBondPosition, xmlFile, Encoding.UTF8);
            }
        }
        private void SaveSubmountMap()
        {
            var submountXmlFile = $@"{_recipeFolderFullName}\SubmountMap.xml";
            XmlSerializeHelper.XmlSerializeToFile(this.SubmonutInfos.SubmountMapInfos, submountXmlFile, Encoding.UTF8);
        }
        private static List<MaterialMapInformation> LoadComponentMap(string componentName)
        {
            var materialMapData = new List<MaterialMapInformation>();
            try
            {
                var xmlFile = $@"{_componentsSavePath}{componentName}\ComponentMap_{componentName}.xml";
                materialMapData = XmlSerializeHelper.XmlDeserializeFromFile<List<MaterialMapInformation>>(xmlFile, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, string.Format("Load Recipe {0} LoadComponentMap 信息异常", _recipeFullName), ex);
            }
            return materialMapData;
        }
        private static List<MaterialMapInformation> LoadSubmountMap()
        {
            var materialMapData = new List<MaterialMapInformation>();
            try
            {
                var xmlFile = $@"{_recipeFolderFullName}\SubmountMap.xml";
                materialMapData = XmlSerializeHelper.XmlDeserializeFromFile<List<MaterialMapInformation>>(xmlFile, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, string.Format("Load Recipe {0} LoadComponentMap 信息异常", _recipeFullName), ex);
            }
            return materialMapData;
        }
        public bool IsStepComplete_Submount()
        {
            var ret = false;
            if (IsStepComplete_SubmountInfo() && IsStepComplete_SubmountPosition() && IsStepComplete_SubmountMap()
                    && IsStepComplete_SubmountPPSettings() && IsStepComplete_SubmountAccuracy())
            {
                ret = true;
            }
            return ret;
        }
        public bool IsStepComplete_SubmountInfo()
        {
            var ret = false;
            ret = SubmonutInfos.IsMaterialInfoSettingsComplete;
            return ret;
        }
        public bool IsStepComplete_SubmountPosition()
        {
            var ret = false;
            ret = SubmonutInfos.IsMaterialPositionSettingsComplete;
            return ret;
        }
        public bool IsStepComplete_SubmountMap()
        {
            var ret = false;
            ret = SubmonutInfos.IsMaterialMapSettingsComplete;
            return ret;
        }
        public bool IsStepComplete_SubmountPPSettings()
        {
            var ret = false;
            ret = SubmonutInfos.IsMaterialPPSettingsComplete;
            return ret;
        }
        public bool IsStepComplete_SubmountAccuracy()
        {
            var ret = false;
            ret = SubmonutInfos.IsMaterialAccuracySettingsComplete;
            return ret;
        }
        public bool IsStepComplete_Components()
        {
            var ret = true;
            foreach (var item in ComponentList)
            {
                if(!IsStepComplete_Component(item.Name))
                {
                    ret = false;
                    break;
                }
            }
            if (ComponentList.Count == 0)
            {
                ret = false;
            }
            return ret;
        }
        public bool IsStepComplete_Component(string componentName)
        {
            var ret = false;
            if(IsStepComplete_ComponentInfo(componentName)&& IsStepComplete_ComponentPosition(componentName) && IsStepComplete_ComponentMap(componentName) 
                && IsStepComplete_ComponentPPSettings(componentName) && IsStepComplete_ComponentAccuracy(componentName))
            {
                ret = true;
            }

            return ret;
        }
        public bool IsStepComplete_ComponentInfo(string componentName)
        {
            var ret = false;
            var material = ComponentList.FirstOrDefault(i => i.Name == componentName);
            if(material != null)
            {
                ret = material.IsMaterialInfoSettingsComplete;
            }
            return ret;
        }
        public bool IsStepComplete_ComponentPosition(string componentName)
        {
            var ret = false;
            var material = ComponentList.FirstOrDefault(i => i.Name == componentName);
            if (material != null)
            {
                ret = material.IsMaterialPositionSettingsComplete;
            }
            return ret;
        }
        public bool IsStepComplete_ComponentMap(string componentName)
        {
            var ret = false;
            var material = ComponentList.FirstOrDefault(i => i.Name == componentName);
            if (material != null)
            {
                ret = material.IsMaterialMapSettingsComplete;
            }
            return ret;
        }
        public bool IsStepComplete_ComponentPPSettings(string componentName)
        {
            var ret = false;
            var material = ComponentList.FirstOrDefault(i => i.Name == componentName);
            if (material != null)
            {
                ret = material.IsMaterialPPSettingsComplete;
            }
            return ret;
        }
        public bool IsStepComplete_ComponentAccuracy(string componentName)
        {
            var ret = false;
            var material = ComponentList.FirstOrDefault(i => i.Name == componentName);
            if (material != null)
            {
                ret = material.IsMaterialAccuracySettingsComplete;
            }
            return ret;
        }
        public bool IsStepComplete_BondPositions()
        {
            var ret = true;
            foreach (var item in BondingPositionList)
            {
                if (!IsStepComplete_BondPosition(item.Name))
                {
                    ret = false;
                    break;
                }
            }
            if (BondingPositionList.Count == 0)
            {
                ret = false;
            }
            return ret;
        }
        public bool IsStepComplete_BondPosition(string name)
        {
            var ret = false;
            var material = BondingPositionList.FirstOrDefault(i => i.Name == name);
            if (material != null)
            {
                ret = material.IsComplete;
            }
            return ret;
        }
        public bool IsStepComplete_EutecticSettings()
        {
            var ret = false;
            ret = EutecticParameters.IsCompleted;
            return ret;
        }
        public bool IsStepComplete_ProcessList()
        {
            var ret = false;
            ret = ProcessingList.Count > 0;
            return ret;
        }
        public bool IsStepComplete_BlankingSettings()
        {
            var ret = false;
            ret = BlankingParameters.IsCompleted;
            return ret;
        }
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
        [XmlElement("RelatedComponentName")]
        public string RelatedComponentName { get; set; }
        [XmlElement("RelatedSubmountName")]
        public string RelatedSubmountName { get; set; }

        [XmlElement("Source")]
        public string Source { get; set; }
        [XmlElement("Destination")]
        public string Destination { get; set; }

    }



}
