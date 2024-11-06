using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using IOUtilityClsLib;
using PositioningSystemClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace JobClsLib
{
    public class ChipPPUtility
    {
        private static readonly object _lockObj = new object();
        private static volatile ChipPPUtility _instance = null;
        public static ChipPPUtility Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ChipPPUtility();
                        }
                    }
                }
                return _instance;
            }
        }
        private ChipPPUtility()
        {
        }
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positioningSystem
        {
            get { return PositioningSystem.Instance; }
        }
        public bool Pick(PPWorkParameters param, bool isSingleStepRun = false, Action beforeAct = null)
        {
            var ret = false;
            try
            {
                SingleStepRunUtility.Instance.EnableSingleStep = isSingleStepRun;
                if (param.UsedPP == EnumUsedPP.SubmountPP)
                {
                    SingleStepRunUtility.Instance.RunAction(new Action(() =>
                    {
                        //衬底PP移动到工作位
                        _positioningSystem.MoveAixsToStageCoord(EnumStageAxis.SubmountPPZ, _systemConfig.PositioningConfig.SubmountPPWorkZ, EnumCoordSetType.Absolute);
                    }));
                }
                if(param.IsUseNeedle)
                {
                    SingleStepRunUtility.Instance.RunAction(new Action(() =>
                    {
                        _positioningSystem.SetAxisSpeed(EnumStageAxis.NeedleZ, param.NeedleSpeed);
                        _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.NeedleZ, param.NeedleUpHeight, EnumCoordSetType.Relative);
                    }));
                }
                var terminal = param.WorkHeight - param.PickupStress;
                var quickTravelTarget = terminal + param.SlowTravelAfterPickupMM;
                SingleStepRunUtility.Instance.RunAction(new Action(() =>
                {
                    //快速移动
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, quickTravelTarget, EnumCoordSetType.Absolute);
                    //慢速移动
                    _positioningSystem.SetAxisSpeed(EnumStageAxis.BondZ, param.SlowSpeedBeforePickup);
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, terminal, EnumCoordSetType.Absolute);
                }));
                SingleStepRunUtility.Instance.RunAction(new Action(() =>
                {
                    //开真空
                    IOUtilityHelper.Instance.OpenChipPPVaccum();
                    Thread.Sleep((int)param.DelayMSForVaccum);
                }));
                //慢速上升
                var slowTravelTargetAfterPickup = terminal + param.SlowTravelAfterPickupMM;
                SingleStepRunUtility.Instance.RunAction(new Action(() =>
                {
                    _positioningSystem.SetAxisSpeed(EnumStageAxis.BondZ, param.SlowSpeedAfterPickup);
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, slowTravelTargetAfterPickup,EnumCoordSetType.Absolute);
                    //快速上升
                    var TargetAfterPickup = terminal + param.UpDistanceMMAfterPicked;
                    _positioningSystem.SetAxisSpeed(EnumStageAxis.BondZ, param.SlowSpeedAfterPickup);
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, TargetAfterPickup, EnumCoordSetType.Absolute);
                }));
                if (param.IsUseNeedle)
                {
                    SingleStepRunUtility.Instance.RunAction(new Action(() =>
                    {
                        _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.NeedleZ, -param.NeedleUpHeight, EnumCoordSetType.Relative);
                    }));
                }
                if (param.UsedPP == EnumUsedPP.SubmountPP)
                {
                    SingleStepRunUtility.Instance.RunAction(new Action(() =>
                    {
                        //衬底PP移动到空闲位
                        _positioningSystem.MoveAixsToStageCoord(EnumStageAxis.SubmountPPZ, _systemConfig.PositioningConfig.SubmountPPFreeZ, EnumCoordSetType.Absolute);
                    }));
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, "PP Pickup Error.", ex);
                ret= false;
            }
            finally
            {

            }
            return ret;
        }
        public bool Place(PPWorkParameters param, bool isSingleStepRun = false, Action afterAct = null)
        {
            var ret = false;
            try
            {

                SingleStepRunUtility.Instance.EnableSingleStep = isSingleStepRun;
                if (param.UsedPP == EnumUsedPP.SubmountPP)
                {
                    SingleStepRunUtility.Instance.RunAction(new Action(() =>
                    {
                        //衬底PP移动到工作位
                        _positioningSystem.MoveAixsToStageCoord(EnumStageAxis.SubmountPPZ, _systemConfig.PositioningConfig.SubmountPPWorkZ, EnumCoordSetType.Absolute);
                    }));
                }
                var terminal = param.WorkHeight;
                var quickTravelTarget = terminal + param.SlowTravelAfterPickupMM;
                SingleStepRunUtility.Instance.RunAction(new Action(() =>
                {
                    //快速移动
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, quickTravelTarget, EnumCoordSetType.Absolute);
                    //慢速移动
                    _positioningSystem.SetAxisSpeed(EnumStageAxis.BondZ, param.SlowSpeedBeforePickup);
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, terminal, EnumCoordSetType.Absolute);
                }));
                SingleStepRunUtility.Instance.RunAction(new Action(() =>
                {
                    //关真空
                    IOUtilityHelper.Instance.CloseChipPPVaccum();
                    Thread.Sleep((int)param.DelayMSForVaccum);
                }));
                //慢速上升
                var slowTravelTargetAfterPickup = terminal + param.SlowTravelAfterPickupMM;
                SingleStepRunUtility.Instance.RunAction(new Action(() =>
                {
                    _positioningSystem.SetAxisSpeed(EnumStageAxis.BondZ, param.SlowSpeedAfterPickup);
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, slowTravelTargetAfterPickup, EnumCoordSetType.Absolute);
                    //快速上升
                    var TargetAfterPickup = terminal + param.UpDistanceMMAfterPicked;
                    _positioningSystem.SetAxisSpeed(EnumStageAxis.BondZ, param.SlowSpeedAfterPickup);
                    _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.BondZ, TargetAfterPickup, EnumCoordSetType.Absolute);
                }));
                if (param.IsUseNeedle)
                {
                    SingleStepRunUtility.Instance.RunAction(new Action(() =>
                    {
                        _positioningSystem.MoveAxisToSystemCoord(EnumStageAxis.NeedleZ, -param.NeedleUpHeight, EnumCoordSetType.Relative);
                    }));
                }

                ret = true;
            }
            catch (Exception ex)
            {
                LogRecorder.RecordLog(EnumLogContentType.Error, "PP Pickup Error.", ex);
                ret = false;
            }
            finally
            {

            }
            return ret;
        }
    }
}
