using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using ModelConfigClsLib.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace ModelConfigClsLib
{
    public class ConfigService
    {
        public string ComponentFilePath = @"D:\GWData\ConfigFiles\ComponentConfig";
        //public string SubstratePath = @"D:\GWData\ConfigFiles\SubstrateConfig";
        //public string ChipPath = @"D:\GWData\ConfigFiles\ChipConfig";
        public string TranslateFilePath = @"D:\GWData\ConfigFiles\TranslateConfig";
        public string EutecticFilePath = @"D:\GWData\ConfigFiles\EutecticConfig";
        public string BondingPositionFilePath = @"D:\GWData\ConfigFiles\BondingPositionConfig";
        public string ProductFilePath = @"D:\GWData\ConfigFiles\ProductConfig";

        //加载物料配方文件
        public ComponentConfig loadComponentConfig(string fileName)
        {
            string allFileName = Path.Combine(ComponentFilePath, fileName + @".xml");
            ComponentConfig componentConfig = XmlSerializeHelper.XmlDeserializeFromFile<ComponentConfig>(allFileName, Encoding.UTF8);
            return componentConfig;
        }

        //保存物料配方文件
        public bool saveComponentConfig(ComponentConfig config)
        {
            string allFileName = Path.Combine(ComponentFilePath, config.ConfigName + @".xml");
            try
            {
                if (!Directory.Exists(ComponentFilePath))//如果路径不存在
                {
                    Directory.CreateDirectory(ComponentFilePath);//创建一个路径的文件夹
                }

                XmlSerializeHelper.XmlSerializeToFile(config, allFileName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, allFileName);
                LogRecorder.RecordLog(EnumLogContentType.Error, "创建 Component 出错\n", ex);
                return false;
            }

            return true;
        }

        /*
        //加载基底配方文件
        public ComponentConfig loadSubstrateConfig(string fileName)
        {
            string allFileName = Path.Combine(SubstratePath, fileName + @".xml");
            ComponentConfig componentConfig = XmlSerializeHelper.XmlDeserializeFromFile<ComponentConfig>(allFileName, Encoding.UTF8);
            return componentConfig;
        }

        //保存基底配方文件
        public bool saveSubstrateConfig(ComponentConfig config)
        {
            string allFileName = Path.Combine(SubstratePath, config.ConfigName + @".xml");
            try
            {  
                if (!Directory.Exists(SubstratePath))//如果路径不存在
                {
                    Directory.CreateDirectory(SubstratePath);//创建一个路径的文件夹
                }

                XmlSerializeHelper.XmlSerializeToFile(config, allFileName, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, allFileName);
                LogRecorder.RecordLog(EnumLogContentType.Error, "创建 Substrate 出错\n", ex);
                return false;
            }

            return true;
        }

        //加载芯片配方文件
        public ComponentConfig loadChipConfig(string fileName)
        {
            string allFileName = Path.Combine(ChipPath, fileName + @".xml");
            ComponentConfig componentConfig = XmlSerializeHelper.XmlDeserializeFromFile<ComponentConfig>(allFileName, Encoding.UTF8);
            return componentConfig;
        }

        //保存芯片配方文件
        public bool saveChipConfig(ComponentConfig config)
        {
            string allFileName = Path.Combine(SubstratePath, config.ConfigName + @".xml");
            try
            {
                if (!Directory.Exists(ChipPath))//如果路径不存在
                {
                    Directory.CreateDirectory(ChipPath);//创建一个路径的文件夹
                }

                XmlSerializeHelper.XmlSerializeToFile(config, allFileName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, allFileName);
                LogRecorder.RecordLog(EnumLogContentType.Error, "创建 Chip 出错\n", ex);
                return false;
            }

            return true;
        }
        */

        //加载BondingPositionConfig文件
        public BondingPositionConfig loadBondingPositionConfig(string fileName)
        {
            string allFileName = Path.Combine(BondingPositionFilePath, fileName + @".xml");
            BondingPositionConfig retConfig = XmlSerializeHelper.XmlDeserializeFromFile<BondingPositionConfig>(allFileName, Encoding.UTF8);
            return retConfig;
        }

        //保存BondingPositionConfig文件
        public bool saveBondingPositionConfig(BondingPositionConfig config)
        {
            string allFileName = Path.Combine(BondingPositionFilePath, config.ConfigName + @".xml");
            try
            {
                if (!Directory.Exists(BondingPositionFilePath))//如果路径不存在
                {
                    Directory.CreateDirectory(BondingPositionFilePath);//创建一个路径的文件夹
                }

                XmlSerializeHelper.XmlSerializeToFile(config, allFileName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, allFileName);
                LogRecorder.RecordLog(EnumLogContentType.Error, "创建 BondingPositionConfig 出错\n", ex);
                return false;
            }

            return true;
        }

        //加载 EutecticConfig 文件
        public EutecticConfig loadEutecticConfig(string fileName)
        {
            string allFileName = Path.Combine(EutecticFilePath, fileName + @".xml");
            EutecticConfig retConfig = XmlSerializeHelper.XmlDeserializeFromFile<EutecticConfig>(allFileName, Encoding.UTF8);
            return retConfig;
        }

        //保存 EutecticConfig 文件
        public bool saveEutecticConfig(EutecticConfig config)
        {
            string allFileName = Path.Combine(EutecticFilePath, config.ConfigName + @".xml");
            try
            {
                if (!Directory.Exists(EutecticFilePath))//如果路径不存在
                {
                    Directory.CreateDirectory(EutecticFilePath);//创建一个路径的文件夹
                }

                XmlSerializeHelper.XmlSerializeToFile(config, allFileName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, allFileName);
                LogRecorder.RecordLog(EnumLogContentType.Error, "创建 EutecticConfig 出错\n", ex);
                return false;
            }

            return true;
        }

        //加载 ProductConfig 文件
        public ProductConfig loadProductConfig(string fileName)
        {
            string allFileName = Path.Combine(ProductFilePath, fileName + @".xml");
            ProductConfig retConfig = XmlSerializeHelper.XmlDeserializeFromFile<ProductConfig>(allFileName, Encoding.UTF8);
            return retConfig;
        }

        //保存 ProductConfig 文件
        public bool saveProductConfig(ProductConfig config)
        {
            string allFileName = Path.Combine(ProductFilePath, config.ProductName + @".xml");
            try
            {
                if (!Directory.Exists(ProductFilePath))//如果路径不存在
                {
                    Directory.CreateDirectory(ProductFilePath);//创建一个路径的文件夹
                }

                XmlSerializeHelper.XmlSerializeToFile(config, allFileName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, allFileName);
                LogRecorder.RecordLog(EnumLogContentType.Error, "创建 ProductFilePath 出错\n", ex);
                return false;
            }

            return true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------


        public bool testCreateSubstrateConfig(string name)
        {
            ComponentConfig config = new ComponentConfig();
            config.ConfigName = name;
            config.ContainerType = EnumContainerType.GelPack;
            config.ColSpace = 2.3f;
            config.ColsNum = 3;
            config.ComponentType = EnumComponentType.Substrate;
            config.FeatureImg = "a.png";
            config.FinishAction = EnumFinishAction.ThrowOut;

            return this.saveComponentConfig(config);
        }

        public bool testCreateChipConfig(string name)
        {
            ComponentConfig config = new ComponentConfig();
            config.ConfigName = name;
            config.ContainerType = EnumContainerType.GelPack;
            config.ColSpace = 2.3f;
            config.ColsNum = 3;
            config.ComponentType = EnumComponentType.Chip;
            config.FeatureImg = "a.png";
            config.FinishAction = EnumFinishAction.ThrowOut;

            return this.saveComponentConfig(config);
        }

        public bool testCreatePosConfig(string name)
        {
            BondingPositionConfig config = new BondingPositionConfig();
            config.ConfigName = name;
            config.OffsetX = 2.3f;
            config.OffsetY = 2.4f;
            config.OffsetAngle = 5.3f;

            return this.saveBondingPositionConfig(config);
        }

        public bool testCreateEutecticConfig(string name)
        {
            EutecticConfig config = new EutecticConfig();
            config.ConfigName = name;
            config.HoldingTime = 30;

            return this.saveEutecticConfig(config);
        }

        public bool testCreateProductConfig(string name)
        {
            ProductConfig config = new ProductConfig();
            config.ProductName = name;
            List <ProductStep> steps = new List<ProductStep>();

            ProductStep step1 = new ProductStep();
            step1.StepName = "物料移至共晶台";
            step1.BondingPositionName = "基底至共晶台";
            step1.ComponentName = "基底1";
            step1.EutecticName = "";
            step1.productStepType = EnumProductStepType.Translate;
            steps.Add(step1);

            ProductStep step2 = new ProductStep();
            step2.StepName = "芯片移至共晶台";
            step2.BondingPositionName = "芯片至共晶台";
            step2.ComponentName = "芯片1";
            step2.EutecticName = "";
            step2.productStepType = EnumProductStepType.Translate;
            steps.Add(step2);

            ProductStep step3 = new ProductStep();
            step3.StepName = "共晶步骤";
            step3.BondingPositionName = "";
            step3.ComponentName = "";
            step3.EutecticName = "共晶参数1";
            step3.productStepType = EnumProductStepType.Eutectic;
            steps.Add(step3);

            config.ProductSteps = steps;

            return this.saveProductConfig(config);
        }

        //---------------------------------------------------------------------------------------------------------------------------

        //product文件名列表
        public List<ProductConfig> getProductConfigList()
        {
            string[] files = Directory.GetFiles(ProductFilePath)
            .Select(f => Path.GetFileNameWithoutExtension(f))
            .ToArray();

            List<ProductConfig> retList = new List<ProductConfig>();
            foreach (string name in files)
            {
                ProductConfig conf = loadProductConfig(name);
                retList.Add(conf);
            }
            return retList;
        }

        //根据product文件名获取product类
        public ProductConfig GetProductConfigByName(string prodName)
        {
            string pathName = Path.Combine(ProductFilePath, prodName + @".xml");
            LogRecorder.RecordLog(EnumLogContentType.Info, pathName);
            return loadProductConfig(pathName);
        }
    }
}
