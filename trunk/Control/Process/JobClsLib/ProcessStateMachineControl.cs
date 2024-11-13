using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using PositioningSystemClsLib;
using RecipeClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using thinger.cn.DataConvertHelper;
using VisionClsLib;
using VisionControlAppClsLib;

namespace JobClsLib
{
    public static class ProcessStateMachineControl
    {

        #region 参数


        public static object GetParameterByCode(int code)
        {
            // 示例：根据 code 设置不同的参数  
            switch (code)
            {
                case 1:
                    return new EnumParameter(EnumOvenBoxNum.Oven1, code);
                case 2:
                    return new EnumParameter(EnumOvenBoxNum.Oven2, code);
                case 3:
                    return new ProcessTargetPositionParam(TransportControl.Instance.TransportRecipe.MaterialboxHooktoMaterialboxPosition1, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 4:
                    return new ProcessTargetPositionParam(TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget1Position, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 5:
                    return new ProcessTargetPositionParam(TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget2Position, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 6:
                    return new ProcessTargetPositionParam(TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget3Position, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 7:
                    return new ProcessTargetPositionParam(TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget4Position, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 8:
                    return new ProcessTargetPositionParam(new XYZTCoordinateConfig() 
                    {
                        X = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget2Position.X,
                        Y = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget2Position.Y,
                        Z = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget2Position.Z + TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialboxSize.Z,
                        Theta = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget2Position.Theta,
                    }, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 9:
                    return new ProcessTargetPositionParam(new XYZTCoordinateConfig()
                    {
                        X = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget4Position.X,
                        Y = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget4Position.Y,
                        Z = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget4Position.Z + TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialboxSize.Z,
                        Theta = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget4Position.Theta,
                    }, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 10:
                    return new ProcessTargetPositionParam(TransportControl.Instance.TransportRecipe.MaterialboxHooktoMaterialboxPosition2, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 11:
                    return new ProcessTrainsportMaterialboxParam(TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0], code);
                case 12:
                    return new ProcessTrainsportMaterialboxParam(TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[1], code);
                case 13:
                    return new ProcessTrainsportMaterialboxParam(TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[0], code);
                case 14:
                    return new ProcessTrainsportMaterialboxParam(TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[1], code);
                case 15:
                    return new ProcessTargetPositionParam(new XYZTCoordinateConfig()
                    {
                        X = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget1Position.X,
                        Y = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget1Position.Y,
                        Z = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget1Position.Z + TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialboxSize.Z,
                        Theta = TransportControl.Instance.TransportRecipe.MaterialboxHooktoTarget1Position.Theta,
                    }, TransportControl.Instance.TransportRecipe.MaterialboxHookUp, code);
                case 16:
                    return new EnumParameter(EnumOvenBoxNum.Oven1, code, true);
                case 17:
                    return new EnumParameter(EnumOvenBoxNum.Oven1, code, false);
                case 18:
                    return new EnumParameter(EnumOvenBoxNum.Oven2, code, true);
                case 19:
                    return new EnumParameter(EnumOvenBoxNum.Oven2, code, false);
                default:
                    throw new InvalidOperationException($"Unsupported generic parameter code: {code}");
            }
        }

        public static object GetPositionByCode(int code)
        {

            // 示例：根据 code 设置不同的参数  
            switch (code)
            {
                case 1:
                    return new EnumPosition(EnumOvenBoxNum.Oven1, 0);
                case 2:
                    return new EnumPosition(EnumOvenBoxNum.Oven1, 1);
                case 3:
                    return new EnumPosition(EnumOvenBoxNum.Oven1, 2);
                case 4:
                    return new EnumPosition(EnumOvenBoxNum.Oven2, 0);
                case 5:
                    return new EnumPosition(EnumOvenBoxNum.Oven2, 1);
                case 6:
                    return new EnumPosition(EnumOvenBoxNum.Oven2, 2);
                case 7:
                    return new EnumOvenStates(EnumOvenBoxState.Oven1In);
                case 8:
                    return new EnumOvenStates(EnumOvenBoxState.Oven1Out);
                case 9:
                    return new EnumOvenStates(EnumOvenBoxState.Oven2In);
                case 10:
                    return new EnumOvenStates(EnumOvenBoxState.Oven2Out);
                case 11:
                    return new EnumOvenStates(EnumOvenBoxState.Oven1Work);
                case 12:
                    return new EnumOvenStates(EnumOvenBoxState.Oven2Work);
                default:
                    return new EnumPosition(EnumOvenBoxNum.Oven1, 0);
            }
        }



        #endregion


        #region 方法对应

        private static readonly Dictionary<int, Func<object, XktResult<string>>> methodFactory = new Dictionary<int, Func<object, XktResult<string>>>
        {
            { 0, param => SimulateAction(0,0) },
            { 1, param => InitAction(1,0) },
            { 2, param => MaterialboxHooktoSafePositionAction(2,0) },
            { 3, param => OpenOvenBoxInteriorDoor(3,((EnumParameter)param).code,(EnumParameter)param) },
            { 4, param => MaterialboxOutofovenAction(4,((EnumParameter)param).code,(EnumParameter)param) },
            { 5, param => MaterialboxHooktoMaterialboxAction(5,((EnumParameter)param).code,(EnumParameter)param) },
            { 6, param => MaterialboxHookPickupMaterialboxAction(6,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 7, param => MaterialboxHooktoProcessTargetPositionParamAction(7,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 8, param => MaterialboxHookPutdownMaterialboxAction(8,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 9, param => MaterialboxInofovenAction(9,((EnumParameter)param).code,(EnumParameter)param) },
            { 10, param => CloseOvenBoxInteriorDoor(10,((EnumParameter)param).code,(EnumParameter)param) },

            { 11, param => InitWeldAction(11,0) },
            { 12, param => MaterialHooktoSafePositionAction(12,0) },
            { 13, param => PressliftingLiftUpMaterialAction(13,0) },
            { 14, param => PressliftingPutdownMaterialAction(14,0) },
            { 15, param => MaterialHooktoMaterialAction(15,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 16, param => MaterialHookPickupMaterialAction(16,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 17, param => MaterialHooktoProcessTargetPositionParamAction(17,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 18, param => MaterialHookPutdownMaterialAction(18,((ProcessTargetPositionParam)param).code,(ProcessTargetPositionParam)param) },
            { 19, param => WeldMaterialAction(19,0) },
            { 20, param => TrackCameraIdentificationMaterialBoxAction(20,0,(MatchIdentificationParam)param) },
            { 21, param => TrackCameraIdentificationMaterialAction(21,0,(MatchIdentificationParam)param) },
            { 22, param => WeldCameraIdentificationMaterialAction(22,0,(MatchIdentificationParam)param) },

            { 24, param => MaterialboxHooktoAvoidPositionAction(24,0) },
            { 25, param => MaterialHooktoAvoidPositionAction(25,0) },

            { 26, param => MaterialBoxOutofovenRemindAction(26,((EnumParameter)param).code,(EnumParameter)param) },
            { 27, param => MaterialBoxIntoovenRemindAction(26,((EnumParameter)param).code,(EnumParameter)param) },

        };


        #endregion




        /// <summary>  
        /// 方法调用  
        /// </summary>  
        /// <param name="state"></param>  
        /// <returns></returns>  
        public static XktResult<string> ExecuteState(string state, object parameter = null)
        {
            if (state.Length != 9)
                throw new ArgumentException("State must be a 9-character string.");

            int variableCode = int.Parse(state.Substring(0, 3));
            int methodCode = int.Parse(state.Substring(3, 3));

            if (variableCode != 0)
            {
                parameter = GetParameterByCode(variableCode);
            }

            if (parameter == null)
            {
                //throw new ArgumentNullException(nameof(parameter), "Parameter cannot be null");
            }


            if (methodFactory.TryGetValue(methodCode, out var method))
            {
                XktResult<string> result = new XktResult<string>();
                var runningType = SystemConfiguration.Instance.JobConfig.RunningType;
                if (runningType == EnumRunningType.Actual)
                {
                    result = method(parameter);
                }
                else
                {
                    result = SimulateAction(methodCode, variableCode);
                }

                
                return result;
            }
            else
            {
                throw new InvalidOperationException($"Method not found {methodCode}");
            }
        }





        #region 方法

        /// <summary>
        /// 模拟方法
        /// </summary>
        /// <param name="methodCode"></param>
        /// <param name="variableCode"></param>
        /// <returns></returns>
        private static XktResult<string> SimulateAction(int methodCode, int variableCode)
        {
            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            Thread.Sleep(1000);

            Task.Factory.StartNew(new Action(() =>
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 状态机:" + variableCodestr + methodCodestr + "000" + " 完成";
            }));



            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "完成" };
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> InitAction(int methodCode, int variableCode)
        {
            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            Console.WriteLine("Materialbox hooked to safe position.");
            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "初始化：料盒钩爪移动到空闲中";
            int Done = TransportControl.Instance.MaterialboxHooktoSafePositionAction(1);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "初始化：料盒钩爪移动到空闲位置失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "初始化：料盒钩爪移动到空闲位置失败" };
            }
            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "初始化：物料钩爪移动到空闲中";
            Done = TransportControl.Instance.MaterialHooktoSafePositionAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "初始化：物料钩爪移动到空闲位置失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "初始化：物料钩爪移动到空闲位置失败" };
            }


            Console.WriteLine("开始执行烘箱1.");

            TransportRecipe recipe = TransportControl.Instance.TransportRecipe;

            string Str = "";

            if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 0)
            {
                Str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"\n配方：{TransportControl.Instance.TransportRecipe.RecipeName}\n."
                + $"烘箱1：{recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber}层";
            }
            else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
            {
                Str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"\n配方：{TransportControl.Instance.TransportRecipe.RecipeName}\n."
                + $"烘箱1：{recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber}层; 料盘1：{recipe.OverBox1Param.MaterialboxParam[0].MaterialNumber}颗物料";
            }
            else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
            {
                Str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"\n配方：{TransportControl.Instance.TransportRecipe.RecipeName}\n."
                + $"烘箱1：{recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber}层; 料盘1：{recipe.OverBox1Param.MaterialboxParam[0].MaterialNumber}颗物料; 料盘2：{recipe.OverBox1Param.MaterialboxParam[1].MaterialNumber}颗物料";
            }

            if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 0)
            {
                Str += $"烘箱1：{recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber}层";
            }
            else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
            {
                Str += $"烘箱1：{recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber}层; 料盘1：{recipe.OverBox2Param.MaterialboxParam[0].MaterialNumber}颗物料";
            }
            else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
            {
                Str += $"烘箱1：{recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber}层; 料盘1：{recipe.OverBox2Param.MaterialboxParam[0].MaterialNumber}颗物料; 料盘2：{recipe.OverBox2Param.MaterialboxParam[1].MaterialNumber}颗物料";
            }

            DataModel.Instance.JobLogText = Str;

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "初始化完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "初始化完成" };

        }

        /// <summary>
        /// 料盒钩爪移动到避让位置
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialboxHooktoAvoidPositionAction(int methodCode, int variableCode)
        {
            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒钩爪移动到避让位置中";

            int Done = TransportControl.Instance.MaterialboxHooktoAvoidPositionAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒钩爪移动到避让位置失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：料盒钩爪移动到避让位置失败" };
            }
            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒钩爪到避让位置完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "料盒搬送：料盒钩爪到避让位置完成" };
        }

        /// <summary>
        /// 物料钩爪移动到避让位置
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialHooktoAvoidPositionAction(int methodCode, int variableCode)
        {
            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料搬送：压机复位中";
            int Done = TransportControl.Instance.WeldResetAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料搬送：压机复位失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料搬送：压机复位失败" };
            }
            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料搬送：物料钩爪移动到避让位置";
            Done = TransportControl.Instance.MaterialHooktoAvoidPositionAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料搬送：物料钩爪移动到避让位置失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料搬送：物料钩爪移动到避让位置失败" };
            }
            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料搬送：物料钩爪到避让位置完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料搬送：物料钩爪到避让位置完成" };
        }


        /// <summary>
        /// 料盒钩爪移动到空闲位置
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialboxHooktoSafePositionAction(int methodCode, int variableCode)
        {
            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒钩爪移动到空闲位置";
            int Done = TransportControl.Instance.MaterialboxHooktoSafePositionAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒钩爪移动到空闲位置失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：料盒钩爪移动到空闲位置失败" };
            }
            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒钩爪到空闲位置完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "料盒搬送：料盒钩爪到空闲位置完成" };
        }

        /// <summary>
        /// 打开烘箱内门
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> OpenOvenBoxInteriorDoor(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;
            double Oven1Vacuum = 0;
            double Oven2Vacuum = 0;
            double BoxVacuum = 0;
            double VacuumD = TransportControl.Instance.VacuumD;
            double VacuumC = TransportControl.Instance.VacuumC;

            int singleDelay = TransportControl.Instance.singleDelay;
            int Delay = TransportControl.Instance.Delay;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");


            EnumSensor sensor = EnumSensor.Oven1InteriorDoorOpen;
            EnumSensor sensor2 = EnumSensor.Oven1BakeOvenAerate;

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门进行中 等待烘烤到结束";

            //判断是否可以打开烘箱内门

            if (ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
            {
                while (DataModel.Instance.OvenBox1Heating)
                {
                    if (!DataModel.Instance.OvenBox1Heating)
                    {
                        DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门进行中 烘烤已结束";
                        break;
                    }

                }


            }
            else if (ovenBoxNum.Num == EnumOvenBoxNum.Oven2)
            {
                while (DataModel.Instance.OvenBox2Heating)
                {
                    if (!DataModel.Instance.OvenBox2Heating)
                    {
                        DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门进行中 烘烤已结束";
                        break;
                    }

                }
            }

            if (ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
            {
                Oven1Vacuum = DataModel.Instance.BakeOvenVacuum;
            }
            else
            {
                Oven1Vacuum = DataModel.Instance.BakeOven2Vacuum;
            }
            BoxVacuum = DataModel.Instance.BoxVacuum;

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门进行中 等待真空度到达条件";

            //int wnum = 0;
            while (!(((Oven1Vacuum / BoxVacuum) < SystemConfiguration.Instance.OvenBoxConfig.OvenBoxBoxVacuumRatio && (Oven1Vacuum / BoxVacuum) > (1 / SystemConfiguration.Instance.OvenBoxConfig.OvenBoxBoxVacuumRatio) && (BoxVacuum < SystemConfiguration.Instance.OvenBoxConfig.BoxVacuumThreshold)) && BoxVacuum > 0 && Oven1Vacuum > 0))
            {
                //Oven1Vacuum = TransportControl.Instance.ReadOvenVacuum(ovenBoxNum.Num);
                //BoxVacuum = TransportControl.Instance.ReadBoxVacuum();
                if (ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
                {
                    Oven1Vacuum = DataModel.Instance.BakeOvenVacuum;
                }
                else
                {
                    Oven1Vacuum = DataModel.Instance.BakeOven2Vacuum;
                }
                BoxVacuum = DataModel.Instance.BoxVacuum;
                //Thread.Sleep(singleDelay);

                //if ((wnum * singleDelay) > Delay)
                //{
                //    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：打开烘箱内门失败";
                //    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：打开烘箱内门失败" };
                //}

                Thread.Sleep(500);
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门进行中 真空度已达到";

            Done = TransportControl.Instance.OpenOvenBoxInteriorDoor(ovenBoxNum.Num);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：打开烘箱{ovenBoxNum.Num}内门失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}内门打开";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}内门打开" };

        }

        /// <summary>
        /// 料盒出烘箱
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialboxOutofovenAction(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            EnumSensor sensor = EnumSensor.Oven1InteriorDoorOpen;
            if (ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
            {
                sensor = EnumSensor.Oven1InteriorDoorOpen;
            }
            else if (ovenBoxNum.Num == EnumOvenBoxNum.Oven2)
            {
                sensor = EnumSensor.Oven2InteriorDoorOpen;
            }
            if (TransportControl.Instance.Readsensor(sensor))
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}向方舱出料";

                Done = TransportControl.Instance.MaterialboxOutofovenAction(ovenBoxNum.Num);
                if (Done != 0)
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}出料失败";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}出料失败" };
                }
            }
            else
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}出料失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}出料失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}出料完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}出料完成" };

        }

        /// <summary>
        /// 钩爪到烘箱料盒上方
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialboxHooktoMaterialboxAction(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            EnumSensor sensor = EnumSensor.Oven1InteriorDoorOpen;
            if (ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
            {
                sensor = EnumSensor.Oven1InteriorDoorOpen;
            }
            else if (ovenBoxNum.Num == EnumOvenBoxNum.Oven2)
            {
                sensor = EnumSensor.Oven2InteriorDoorOpen;
            }
            if (TransportControl.Instance.Readsensor(sensor))
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方";
                Done = TransportControl.Instance.MaterialboxHooktoMaterialboxAction(ovenBoxNum.Num);
                if (Done != 0)
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方失败";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方失败" };
                }
            }
            else
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：钩爪到烘箱{ovenBoxNum.Num}出料盒上方完成" };

        }

        /// <summary>
        /// 料盒钩爪拾取料盒
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialboxHookPickupMaterialboxAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：钩爪拾取料盒中";

            Done = TransportControl.Instance.MaterialboxHookPickupMaterialboxAction(position.XYZT.Z, position.UpPostion);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：钩爪拾取料盒失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：钩爪拾取料盒失败" };
            }



            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：钩爪拾取料盒完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "料盒搬送：钩爪拾取料盒完成" };

        }

        /// <summary>
        /// 料盒钩爪到目标位置上方
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialboxHooktoProcessTargetPositionParamAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");
            int L = 0;
            if (variableCode == 4 || variableCode == 15)
            {
                L = 1;
            }
            else if (variableCode == 5 || variableCode == 8)
            {
                L = 2;
            }
            else if (variableCode == 6)
            {
                L = 3;
            }
            else if (variableCode == 7 || variableCode == 9)
            {
                L = 4;
            }
            int L2 = 0;
            if (variableCode == 4 || variableCode == 5 || variableCode == 6 || variableCode == 7)
            {
                L2 = 1;
            }
            else if (variableCode == 15 || variableCode == 8 || variableCode == 9)
            {
                L2 = 2;
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪移动到目标位置{L}.{L2}层中";
            Done = TransportControl.Instance.MaterialboxHooktoTargetPositionAction(position.XYZT, position.UpPostion);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪移动到目标位置{L}.{L2}层失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：钩爪移动到目标位置{L}.{L2}层失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：钩爪移动到目标位置{L}.{L2}层完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：钩爪移动到目标位置{L}.{L2}层完成" };

        }

        /// <summary>
        /// 料盒钩爪放下料盒
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialboxHookPutdownMaterialboxAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：钩爪放下料盒中";
            Done = TransportControl.Instance.MaterialboxHookPutdownMaterialboxAction(position.XYZT.Z, position.UpPostion);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：钩爪放下料盒失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：钩爪放下料盒失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：钩爪放下料盒完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "料盒搬送：钩爪放下料盒完成" };

        }

        /// <summary>
        /// 料盒进烘箱
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialboxInofovenAction(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：方舱向烘箱{ovenBoxNum.Num}进料中";
            Done = TransportControl.Instance.MaterialboxInofovenAction(ovenBoxNum.Num);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}进料失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}进料进料失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}进料进料完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}进料进料完成" };

        }

        /// <summary>
        /// 关闭烘箱内门
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> CloseOvenBoxInteriorDoor(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;

            int singleDelay = TransportControl.Instance.singleDelay;
            int Delay = TransportControl.Instance.Delay;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");


            EnumSensor sensor = EnumSensor.Oven1InteriorDoorClose;

            if (ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
            {
                sensor = EnumSensor.Oven1InteriorDoorClose;
            }
            else if (ovenBoxNum.Num == EnumOvenBoxNum.Oven2)
            {
                sensor = EnumSensor.Oven2InteriorDoorClose;
            }
            else
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：关闭烘箱{ovenBoxNum.Num}内门失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：关闭烘箱内门失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：关闭烘箱{ovenBoxNum.Num}内门中";

            Done = TransportControl.Instance.CloseOvenBoxInteriorDoor(ovenBoxNum.Num);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：关闭烘箱{ovenBoxNum.Num}内门失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：关闭烘箱{ovenBoxNum.Num}内门失败" };
            }

            int wnum = 0;
            while (!TransportControl.Instance.Readsensor(sensor))
            {
                Thread.Sleep(singleDelay);

                if ((wnum * singleDelay) > Delay)
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：关闭烘箱内门失败";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：关闭烘箱内门失败" };
                }
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：关闭烘箱内门";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "料盒搬送：关闭烘箱内门" };

        }


        /// <summary>
        /// 料盒出烘箱提醒
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialBoxOutofovenRemindAction(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            if (ovenBoxNum.OvenBoxInRemind && ovenBoxNum.Num == EnumOvenBoxNum.Oven1)
            {
                DataModel.Instance.OvenBox1InRemind = true;
            }
            else if (ovenBoxNum.OvenBoxInRemind && ovenBoxNum.Num == EnumOvenBoxNum.Oven2)
            {
                DataModel.Instance.OvenBox2InRemind = true;
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}进料完成，关闭真空中";

            Done = TransportControl.Instance.MaterialBoxOutofovenRemindAction(ovenBoxNum.Num);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}出料失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}出料失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}出料完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}出料完成" };

        }

        /// <summary>
        /// 料盒进烘箱提醒
        /// </summary>
        /// <param name="ovenBoxNum"></param>
        /// <returns></returns>
        private static XktResult<string> MaterialBoxIntoovenRemindAction(int methodCode, int variableCode, EnumParameter ovenBoxNum)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}外部进料中";
            Done = TransportControl.Instance.MaterialBoxIntoovenRemindAction(ovenBoxNum.Num);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}外部进料失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}外部进料失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"料盒搬送：烘箱{ovenBoxNum.Num}外部进料进行中";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = $"料盒搬送：烘箱{ovenBoxNum.Num}外部进料进行中" };

        }



        /// <summary>
        /// 初始化焊台
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> InitWeldAction(int methodCode, int variableCode)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：压机复位中";
            Done = TransportControl.Instance.WeldResetAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：压机复位失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接压机复位失败" };
            }


            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：压机初始化完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：压机初始化完成" };

        }

        /// <summary>
        /// 物料钩爪移动到安全位置
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialHooktoSafePositionAction(int methodCode, int variableCode)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪到空闲位置中";
            Done = TransportControl.Instance.MaterialHooktoSafePositionAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪到空闲位置失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料钩爪到空闲位置失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪到空闲位置";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料钩爪到空闲位置" };

        }

        /// <summary>
        /// 焊台顶升升
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> PressliftingLiftUpMaterialAction(int methodCode, int variableCode)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：顶升上升中";
            Done = TransportControl.Instance.PressliftingLiftUpMaterialAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：顶升上升失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：顶升上升失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：顶升上升完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：顶升上升完成" };

        }

        /// <summary>
        /// 焊台顶升降
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> PressliftingPutdownMaterialAction(int methodCode, int variableCode)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：顶升下降中";
            Done = TransportControl.Instance.PressliftingPutdownMaterialAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：顶升下降失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：顶升下降失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：顶升下降完成";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：顶升下降完成" };

        }

        /// <summary>
        /// 钩爪到物料上方
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialHooktoMaterialAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪移动到物料上方中";
            Done = TransportControl.Instance.MaterialHooktoMaterialAction(position.XYZT, position.UpPostion);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪移动到物料上方失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料钩爪移动到物料上方失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪移动到物料上方";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料钩爪移动到物料上方" };

        }

        /// <summary>
        /// 物料钩爪拾取物料
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialHookPickupMaterialAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪拾取物料中";
            Done = TransportControl.Instance.MaterialHookPickupMaterialAction(position.XYZT.Z, position.UpPostion);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪拾取物料失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料钩爪拾取物料失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪拾取物料";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料钩爪拾取物料" };

        }

        /// <summary>
        /// 物料钩爪到目标位置
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialHooktoProcessTargetPositionParamAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");
            if (position.target == EnumMaterialHooktargetNum.Material)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"物料焊接：物料钩爪到：{position.rowNum}行{position.columnNum}列物料上方中";
            }
            else
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"物料焊接：物料钩爪到：焊台位置{position.PostionNum}上方中";
            }
            Done = TransportControl.Instance.MaterialHooktoTargetPositionAction(position.XYZT, position.UpPostion);
            if (Done != 0)
            {
                if (position.target == EnumMaterialHooktargetNum.Material)
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"物料焊接：物料钩爪到：{position.rowNum}行{position.columnNum}列物料上方失败";
                }
                else
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"物料焊接：物料钩爪到：焊台位置{position.PostionNum}上方失败";
                }
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料钩爪到目标位置上方失败" };
            }

            if (position.target == EnumMaterialHooktargetNum.Material)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"物料焊接：物料钩爪到：{position.rowNum}行{position.columnNum}列物料上方";
            }
            else
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"物料焊接：物料钩爪到：焊台位置{position.PostionNum}上方";
            }
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料钩爪到目标位置上方" };

        }


        /// <summary>
        /// 物料钩爪放下物料
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> MaterialHookPutdownMaterialAction(int methodCode, int variableCode, ProcessTargetPositionParam position)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪放下物料中";
            Done = TransportControl.Instance.MaterialHookPutdownMaterialAction(position.XYZT.Z, position.UpPostion);
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪放下物料失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料钩爪放下物料失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料钩爪放下物料";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料钩爪放下物料" };

        }

        /// <summary>
        /// 焊接
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> WeldMaterialAction(int methodCode, int variableCode)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料焊接中";
            Done = TransportControl.Instance.WeldMaterialAction();
            if (Done != 0)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料焊接失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料焊接失败" };
            }

            DataModel.Instance.PressWorkNumber++;
            SystemConfiguration.Instance.StatisticalDataConfig.PressWorkNumber = DataModel.Instance.PressWorkNumber;

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料焊接";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料焊接" };

        }


        /// <summary>
        /// 搬送相机识别料盘
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> TrackCameraIdentificationMaterialBoxAction(int methodCode, int variableCode, MatchIdentificationParam param)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            XYZTCoordinateConfig xyzt = null;

            EnumVisionRunningType type = SystemConfiguration.Instance.JobConfig.MaterialBoxIdentType;
            if (type == EnumVisionRunningType.Nonidentification)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：不识别料盒";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：不识别料盒" };
            }
            else if (type == EnumVisionRunningType.IdentifyButNotLocate)
            {
                xyzt = VisionControlclass.Instance.IdentificationAsync2(EnumCameraType.TrackCamera, param);

                if (xyzt == null)
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒识别失败";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：料盒识别失败" };
                }
                else
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：识别到料盒";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：识别到料盒" };
                }
            }
            else if (type == EnumVisionRunningType.Identification)
            {
                xyzt = VisionControlclass.Instance.IdentificationAsync2(EnumCameraType.TrackCamera, param);
            }



            if (xyzt == null)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "料盒搬送：料盒识别失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "料盒搬送：料盒识别失败" };
            }

            XYZTCoordinateConfig xyztofset = new XYZTCoordinateConfig()
            {
                X = xyzt.X + SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.X,
                Y = xyzt.Y + SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.Y,

            };

            if (variableCode == 11)
            {
                TransportControl.Instance.Ovennum = 0;
                TransportControl.Instance.Ovenlayer = 0;
                TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].SetMaterialMat(TransportControl.Instance.TransportRecipe.MaterialHookPickupMaterialPosition,
                    TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialboxCenter, xyztofset);

                List<PointF> array0 = new List<PointF>();

                foreach (var row in TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialMat)
                {
                    foreach (var item in row)
                    {
                        PointF point = new PointF()
                        {
                            X = (float)item.MaterialPosition.X,
                            Y = (float)item.MaterialPosition.Y,
                        };
                        array0.Add(point);
                    }
                }

                VisionControlclass.Instance.ShowArray(array0, new PointF((float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.X, (float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.Y));
            }
            else if (variableCode == 12)
            {
                TransportControl.Instance.Ovennum = 0;
                TransportControl.Instance.Ovenlayer = 1;
                TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[1].SetMaterialMat(TransportControl.Instance.TransportRecipe.MaterialHookPickupMaterialPosition,
                   TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[1].MaterialboxCenter, xyztofset);

                List<PointF> array0 = new List<PointF>();

                foreach (var row in TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[1].MaterialMat)
                {
                    foreach (var item in row)
                    {
                        PointF point = new PointF()
                        {
                            X = (float)item.MaterialPosition.X,
                            Y = (float)item.MaterialPosition.Y,
                        };
                        array0.Add(point);
                    }
                }

                VisionControlclass.Instance.ShowArray(array0, new PointF((float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.X, (float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.Y));
            }
            else if (variableCode == 13)
            {
                TransportControl.Instance.Ovennum = 1;
                TransportControl.Instance.Ovenlayer = 0;
                TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[0].SetMaterialMat(TransportControl.Instance.TransportRecipe.MaterialHookPickupMaterialPosition,
                   TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[0].MaterialboxCenter, xyztofset);

                List<PointF> array0 = new List<PointF>();

                foreach (var row in TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[0].MaterialMat)
                {
                    foreach (var item in row)
                    {
                        PointF point = new PointF()
                        {
                            X = (float)item.MaterialPosition.X,
                            Y = (float)item.MaterialPosition.Y,
                        };
                        array0.Add(point);
                    }
                }

                VisionControlclass.Instance.ShowArray(array0, new PointF((float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.X, (float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.Y));
            }
            else if (variableCode == 14)
            {
                TransportControl.Instance.Ovennum = 1;
                TransportControl.Instance.Ovenlayer = 1;
                TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[1].SetMaterialMat(TransportControl.Instance.TransportRecipe.MaterialHookPickupMaterialPosition,
                   TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[1].MaterialboxCenter, xyztofset);

                List<PointF> array0 = new List<PointF>();

                foreach (var row in TransportControl.Instance.TransportRecipe.OverBox2Param.MaterialboxParam[1].MaterialMat)
                {
                    foreach (var item in row)
                    {
                        PointF point = new PointF()
                        {
                            X = (float)item.MaterialPosition.X,
                            Y = (float)item.MaterialPosition.Y,
                        };
                        array0.Add(point);
                    }
                }

                VisionControlclass.Instance.ShowArray(array0, new PointF((float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.X, (float)SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.Y));
            }



            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：识别料盒成功";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：识别料盒成功" };

        }

        /// <summary>
        /// 搬送相机识别物料
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> TrackCameraIdentificationMaterialAction(int methodCode, int variableCode, MatchIdentificationParam param)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            XYZTCoordinateConfig xyzt = null;

            EnumVisionRunningType type = SystemConfiguration.Instance.JobConfig.MaterialIdentType;
            if (type == EnumVisionRunningType.Nonidentification)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：不识别物料";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：不识别物料" };
            }
            else if (type == EnumVisionRunningType.IdentifyButNotLocate)
            {
                xyzt = VisionControlclass.Instance.IdentificationAsync2(EnumCameraType.TrackCamera, param);

                if (xyzt == null)
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料识别失败";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料识别失败" };
                }
                else
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：识别到物料";
                    return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：识别到物料" };
                }
            }
            else if (type == EnumVisionRunningType.Identification)
            {
                xyzt = VisionControlclass.Instance.IdentificationAsync2(EnumCameraType.TrackCamera, param);
            }


            xyzt = VisionControlclass.Instance.IdentificationAsync2(EnumCameraType.TrackCamera, param);

            if (xyzt == null)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料识别失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：物料识别失败" };
            }

            XYZTCoordinateConfig xyztofset = new XYZTCoordinateConfig()
            {
                X = xyzt.X + SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.X,
                Y = xyzt.Y + SystemConfiguration.Instance.PositioningConfig.TrackCameraCenterMaterialHook.Y,

            };

            int i = param.MaterialRow;
            int j = param.MaterialCol;
            TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialMat[i][j].MaterialPosition = xyztofset;

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：物料识别成功";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：物料识别成功" };

        }

        /// <summary>
        /// 焊接相机识别物料
        /// </summary>
        /// <returns></returns>
        private static XktResult<string> WeldCameraIdentificationMaterialAction(int methodCode, int variableCode, MatchIdentificationParam param)
        {
            int Done;

            string variableCodestr = "000";
            variableCodestr = variableCode.ToString("D3");
            string methodCodestr = "000";
            methodCodestr = methodCode.ToString("D3");

            EnumVisionRunningType type = SystemConfiguration.Instance.JobConfig.WeldIdentType;
            if (type == EnumVisionRunningType.Nonidentification)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：焊台位置不识别物料";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：焊台位置不识别物料" };
            }


            MatchResult result = VisionControlclass.Instance.IdentificationAsync(EnumCameraType.WeldCamera, param);

            if (result == null)
            {
                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：焊台位置物料识别失败";
                return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = false, Message = "物料焊接：焊台位置物料识别失败" };
            }

            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "物料焊接：焊台位置物料识别成功";
            return new XktResult<string> { Content = variableCodestr + methodCodestr + "000", IsSuccess = true, Message = "物料焊接：焊台位置物料识别成功" };

        }



        #endregion



    }






}
