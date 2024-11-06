using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlobalDataDefineClsLib;

using CommonPanelClsLib;
using GlobalToolClsLib;
using TechnologicalClsLib;
using System.Threading;
using TemperatureControllerClsLib;
using ConfigurationClsLib;

namespace ControlPanelClsLib
{
    public partial class OvenBoxControlPanel : ViewBase
    {

        /// <summary>
        /// 烘箱箱体控制
        /// </summary>
        private OvenBoxProcessControl _plc
        {
            get { return OvenBoxProcessControl.Instance; }
        }
        private TemperatureControllerManager _TemperatureControllerManager
        {
            get { return TemperatureControllerManager.Instance; }
        }

        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }

        public int CurrencyOvenBox = 0;

        public OvenBoxControlPanel()
        {
            InitializeComponent();
            //Initialize();

            OvenBoxNumcomboBox.SelectedIndex = 0;

            RegisterIOChangedAct();
        }



        private void Initialize()
        {
            #region 烘箱工艺设置

            OvenBoxNumcomboBox.SelectedIndex = 0;

            //烘箱加热参数
            float BakeOvenTargettemp = _systemConfig.OvenBoxConfig.HeatTargetTemperature;
            seHeatTargetTemperature.Text = BakeOvenTargettemp.ToString();

            int BakeOvenHoldingTimeM = _systemConfig.OvenBoxConfig.HeatPreservationMinute;
            seHeatPreservationMinute.Text = BakeOvenHoldingTimeM.ToString();

            short BakeOvenPassedTimeM = 0;
            teHeatPreservationResidueMinute.Text = BakeOvenPassedTimeM.ToString();

            float BakeOvenAlarmtemp = _systemConfig.OvenBoxConfig.OverTemperatureThreshold;
            seOverTemperatureThreshold.Text = BakeOvenAlarmtemp.ToString();


            //烘箱抽充参数
            float BakeOvenAlarmPressure = _systemConfig.OvenBoxConfig.OvenOverPressureThreshold;
            seOvenOverPressureThreshold.Text = BakeOvenAlarmPressure.ToString();

            float BakeOvenPFUpPressure = _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit;
            seOvenPurgePressureUpperLimit.Text = BakeOvenPFUpPressure.ToString();

            float BakeOvenPFDownPressure = _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit;
            seOvenPurgePressureLowerLimit.Text = BakeOvenPFDownPressure.ToString();

            int BakeOvenPFnum = _systemConfig.OvenBoxConfig.OvenPurgeTime;
            seOvenPurgeTime.Text = BakeOvenPFnum.ToString();

            int BakeOvenPFCompletednum = 0;
            teOvenPurgeResidueTime.Text = BakeOvenPFCompletednum.ToString();

            int BakeOvenPFinterval = _systemConfig.OvenBoxConfig.OvenPurgeInterval;
            seOvenPurgeInterval.Text = BakeOvenPFinterval.ToString();


            



            #endregion


            #region 烘箱数据 int float

            //烘箱1
            float BakeOvenPressure = _plc.Read<float>(EnumBoardcardDefineInputIO.BakeOvenPressure);
            this.teOvenPressure.Text = BakeOvenPressure.ToString();
            if(BakeOvenPressure > _systemConfig.OvenBoxConfig.OvenOverPressureThreshold)
            {
                labelOvenOverPressureAlarmStatus.ForeColor = Color.Red;
            }
            else
            {
                labelOvenOverPressureAlarmStatus.ForeColor = Color.Green;
            }

            float BakeOvenVacuum = _plc.Read<float>(EnumBoardcardDefineInputIO.BakeOvenVacuum);
            this.teOvenVacuumDegree.Text = BakeOvenVacuum.ToString();


            float BakeOvenUPtemp = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Up).Read(TemperatureRtuAdd.PV);
            this.teTopZoneTemperature.Text = BakeOvenUPtemp.ToString();

            float BakeOvenDowntemp = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox1Low).Read(TemperatureRtuAdd.PV);
            this.teBottomZoneTemperature.Text = BakeOvenDowntemp.ToString();

            if ((BakeOvenUPtemp > _systemConfig.OvenBoxConfig.OverTemperatureThreshold) || (BakeOvenDowntemp > _systemConfig.OvenBoxConfig.OverTemperatureThreshold))
            {
                labelOverTempAlarmStatus.ForeColor = Color.Red;
            }
            else
            {
                labelOverTempAlarmStatus.ForeColor = Color.Green;
            }

            //烘箱2
            float BakeOven2Pressure = _plc.Read<float>(EnumBoardcardDefineInputIO.BakeOven2Pressure);
            this.teOvenPressure2.Text = BakeOven2Pressure.ToString();
            if (BakeOven2Pressure > _systemConfig.OvenBoxConfig.Oven2OverPressureThreshold)
            {
                labelOvenOverPressureAlarmStatus2.ForeColor = Color.Red;
            }
            else
            {
                labelOvenOverPressureAlarmStatus2.ForeColor = Color.Green;
            }

            float BakeOven2Vacuum = _plc.Read<float>(EnumBoardcardDefineInputIO.BakeOven2Vacuum);
            this.teOvenVacuumDegree2.Text = BakeOven2Vacuum.ToString();


            float BakeOven2UPtemp = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Low).Read(TemperatureRtuAdd.PV);
            this.teTopZoneTemperature2.Text = BakeOven2UPtemp.ToString();

            float BakeOven2Downtemp = _TemperatureControllerManager.GetTemperatureController(EnumTemperatureType.OvenBox2Up).Read(TemperatureRtuAdd.PV);
            this.teBottomZoneTemperature2.Text = BakeOven2Downtemp.ToString();

            if ((BakeOven2UPtemp > _systemConfig.OvenBoxConfig.OverTemperatureThreshold2) || (BakeOven2Downtemp > _systemConfig.OvenBoxConfig.OverTemperatureThreshold2))
            {
                labelOverTempAlarmStatus2.ForeColor = Color.Red;
            }
            else
            {
                labelOverTempAlarmStatus2.ForeColor = Color.Green;
            }

            #endregion

            #region 烘箱状态

            //烘箱1
            bool BakeOvenOuterdoorClosestatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOvenOuterdoorClosestatus);
            bool BakeOvenOuterdoorOpenstatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOvenOuterdoorOpenstatus);
            if (BakeOvenOuterdoorOpenstatus)
            {
                this.teOvenOutDoorOpenStatus.BackColor = Color.Red;
                this.teOvenOutDoorOpenStatus.Text = "打开";
            }
            else if(BakeOvenOuterdoorClosestatus)
            {
                this.teOvenOutDoorOpenStatus.BackColor = Color.Green;
                this.teOvenOutDoorOpenStatus.Text = "关闭";
            }
            if(!BakeOvenOuterdoorOpenstatus && !BakeOvenOuterdoorClosestatus)
            {
                this.teOvenOutDoorOpenStatus.BackColor = Color.Yellow;
                this.teOvenOutDoorOpenStatus.Text = "中间";
            }

            bool BakeOvenInnerdoorClosestatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorClosestatus);
            bool BakeOvenInnerdoorOpenstatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOvenInnerdoorOpenstatus);
            if (BakeOvenInnerdoorOpenstatus)
            {
                this.teOvenInnerDoorOpenStatus.BackColor = Color.Red;
                this.teOvenInnerDoorOpenStatus.Text = "打开";
            }
            else if (BakeOvenInnerdoorClosestatus)
            {
                this.teOvenInnerDoorOpenStatus.BackColor = Color.Green;
                this.teOvenInnerDoorOpenStatus.Text = "关闭";
            }
            if (!BakeOvenInnerdoorOpenstatus && !BakeOvenInnerdoorClosestatus)
            {
                this.teOvenInnerDoorOpenStatus.BackColor = Color.Yellow;
                this.teOvenInnerDoorOpenStatus.Text = "中间";
            }

            bool BakeOvenBleedstatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOvenBleed);
            if (!BakeOvenBleedstatus)
            {
                this.teOvenExtractionValveStatus.BackColor = Color.Green;
                this.teOvenExtractionValveStatus.Text = "关闭";
            }
            else
            {
                this.teOvenExtractionValveStatus.BackColor = Color.Red;
                this.teOvenExtractionValveStatus.Text = "打开";
            }

            bool BakeOvenExhauststatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOvenExhaust);
            if (!BakeOvenExhauststatus)
            {
                this.teOvenExhaustValveStatus.BackColor = Color.Green;
                this.teOvenExhaustValveStatus.Text = "关闭";
            }
            else
            {
                this.teOvenExhaustValveStatus.BackColor = Color.Red;
                this.teOvenExhaustValveStatus.Text = "打开";
            }

            bool BakeOvenAeratestatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOvenAerate);
            if (!BakeOvenAeratestatus)
            {
                this.teOvenMakeupValveStatus.BackColor = Color.Green;
                this.teOvenMakeupValveStatus.Text = "关闭";
            }
            else
            {
                this.teOvenMakeupValveStatus.BackColor = Color.Red;
                this.teOvenMakeupValveStatus.Text = "打开";
            }

            bool BakeOvenAutoHeatstatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOvenAutoHeat);
            if (!BakeOvenAutoHeatstatus)
            {
                this.teHeatState.BackColor = Color.Green;
                this.teHeatState.Text = "关闭";
            }
            else
            {
                this.teHeatState.BackColor = Color.Red;
                this.teHeatState.Text = "打开";
            }


            //烘箱2
            bool BakeOven2OuterdoorClosestatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOven2OuterdoorClosestatus);
            bool BakeOven2OuterdoorOpenstatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOven2OuterdoorOpenstatus);
            if (BakeOven2OuterdoorOpenstatus)
            {
                this.teOvenOutDoorOpenStatus2.BackColor = Color.Red;
                this.teOvenOutDoorOpenStatus2.Text = "打开";
            }
            else if (BakeOven2OuterdoorClosestatus)
            {
                this.teOvenOutDoorOpenStatus2.BackColor = Color.Green;
                this.teOvenOutDoorOpenStatus2.Text = "关闭";
            }
            if (!BakeOven2OuterdoorOpenstatus && !BakeOven2OuterdoorClosestatus)
            {
                this.teOvenOutDoorOpenStatus2.BackColor = Color.Yellow;
                this.teOvenOutDoorOpenStatus2.Text = "中间";
            }

            bool BakeOven2InnerdoorClosestatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOven2InnerdoorClosestatus);
            bool BakeOven2InnerdoorOpenstatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BakeOven2InnerdoorOpenstatus);
            if (BakeOven2InnerdoorOpenstatus)
            {
                this.teOvenInnerDoorOpenStatus2.BackColor = Color.Red;
                this.teOvenInnerDoorOpenStatus2.Text = "打开";
            }
            else if (BakeOven2InnerdoorClosestatus)
            {
                this.teOvenInnerDoorOpenStatus2.BackColor = Color.Green;
                this.teOvenInnerDoorOpenStatus2.Text = "关闭";
            }
            if (!BakeOven2InnerdoorOpenstatus && !BakeOven2InnerdoorClosestatus)
            {
                this.teOvenInnerDoorOpenStatus2.BackColor = Color.Yellow;
                this.teOvenInnerDoorOpenStatus2.Text = "中间";
            }

            bool BakeOven2Bleedstatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOven2Bleed);
            if (!BakeOven2Bleedstatus)
            {
                this.teOvenExtractionValveStatus2.BackColor = Color.Green;
                this.teOvenExtractionValveStatus2.Text = "关闭";
            }
            else
            {
                this.teOvenExtractionValveStatus2.BackColor = Color.Red;
                this.teOvenExtractionValveStatus2.Text = "打开";
            }

            bool BakeOven2Exhauststatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOven2Exhaust);
            if (!BakeOven2Exhauststatus)
            {
                this.teOvenExhaustValveStatus2.BackColor = Color.Green;
                this.teOvenExhaustValveStatus2.Text = "关闭";
            }
            else
            {
                this.teOvenExhaustValveStatus2.BackColor = Color.Red;
                this.teOvenExhaustValveStatus2.Text = "打开";
            }

            bool BakeOven2Aeratestatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOven2Aerate);
            if (!BakeOven2Aeratestatus)
            {
                this.teOvenMakeupValveStatus2.BackColor = Color.Green;
                this.teOvenMakeupValveStatus2.Text = "关闭";
            }
            else
            {
                this.teOvenMakeupValveStatus2.BackColor = Color.Red;
                this.teOvenMakeupValveStatus2.Text = "打开";
            }

            bool BakeOven2AutoHeatstatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BakeOven2AutoHeat);
            if (!BakeOven2AutoHeatstatus)
            {
                this.teHeatState2.BackColor = Color.Green;
                this.teHeatState2.Text = "关闭";
            }
            else
            {
                this.teHeatState2.BackColor = Color.Red;
                this.teHeatState2.Text = "打开";
            }


            #endregion


            #region 交换箱工艺设置

            //工作箱抽充参数
            float ExchangeAlarmPressure = _systemConfig.OvenBoxConfig.BoxOverPressureThreshold;
            seExchangeOverPressureThreshold.Text = ExchangeAlarmPressure.ToString();

            float ExchangePFUpPressure = _systemConfig.OvenBoxConfig.BoxPurgePressureUpperLimit;
            seExchangePurgePressureUpperLimit.Text = ExchangePFUpPressure.ToString();

            float ExchangePFDownPressure = _systemConfig.OvenBoxConfig.BoxPurgePressureLowerLimit;
            seExchangePurgePressureLowerLimit.Text = ExchangePFDownPressure.ToString();

            int ExchangePFnum = _systemConfig.OvenBoxConfig.BoxPurgeTime;
            seExchangePurgeTimes.Text = ExchangePFnum.ToString();

            int ExchangePFCompletednum = 0;
            teExchangePurgeResidueTime.Text = ExchangePFCompletednum.ToString();

            int ExchangePFinterval = _systemConfig.OvenBoxConfig.BoxPurgeInterval;
            seExchangePurgeInterval.Text = ExchangePFinterval.ToString();


            #endregion

            #region 交换箱数据 int float


            float BoxPressure = _plc.Read<float>(EnumBoardcardDefineInputIO.BoxPressure);
            this.teExchangeBoxPressure.Text = BoxPressure.ToString();
            if (BoxPressure > _systemConfig.OvenBoxConfig.BoxOverPressureThreshold)
            {
                labelExchangeBoxOverPressureAlarmStatus.ForeColor = Color.Red;
            }
            else
            {
                labelExchangeBoxOverPressureAlarmStatus.ForeColor = Color.Green;
            }

            float BoxVacuum = _plc.Read<float>(EnumBoardcardDefineInputIO.BoxVacuum);
            this.teExchangeBoxVacuumDegree.Text = BoxVacuum.ToString();


            #endregion

            #region 交换箱状态

            bool BoxOuterdoorClosestatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BoxOuterdoorCloseSta);
            bool BoxOuterdoorOpenstatus = _plc.Read<bool>(EnumBoardcardDefineInputIO.BoxOuterdoorOpenSta);
            if (BoxOuterdoorOpenstatus)
            {
                this.teExchangeOuterDoorloosenStatus.BackColor = Color.Red;
                this.teExchangeOuterDoorloosenStatus.Text = "打开";
            }
            else if (BoxOuterdoorClosestatus)
            {
                this.teExchangeOuterDoorloosenStatus.BackColor = Color.Green;
                this.teExchangeOuterDoorloosenStatus.Text = "关闭";
            }
            if (!BoxOuterdoorOpenstatus && !BoxOuterdoorClosestatus)
            {
                this.teExchangeOuterDoorloosenStatus.BackColor = Color.Yellow;
                this.teExchangeOuterDoorloosenStatus.Text = "中间";
            }



            bool BoxBleedstatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BoxBleed);
            if (!BoxBleedstatus)
            {
                this.teExchangeExtractionValveStatus.BackColor = Color.Green;
                this.teExchangeExtractionValveStatus.Text = "关闭";
            }
            else
            {
                this.teExchangeExtractionValveStatus.BackColor = Color.Red;
                this.teExchangeExtractionValveStatus.Text = "打开";
            }

            bool BoxExhauststatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BoxExhaust);
            if (!BoxExhauststatus)
            {
                this.teExchangeExhaustValveStatus.BackColor = Color.Green;
                this.teExchangeExhaustValveStatus.Text = "关闭";
            }
            else
            {
                this.teExchangeExhaustValveStatus.BackColor = Color.Red;
                this.teExchangeExhaustValveStatus.Text = "打开";
            }

            bool BoxAeratestatus = _plc.Read<bool>(EnumBoardcardDefineOutputIO.BoxAerate);
            if (!BoxAeratestatus)
            {
                this.teExchangeMakeupValveStatus.BackColor = Color.Green;
                this.teExchangeMakeupValveStatus.Text = "关闭";
            }
            else
            {
                this.teExchangeMakeupValveStatus.BackColor = Color.Red;
                this.teExchangeMakeupValveStatus.Text = "打开";
            }

            #endregion



           


        }



        private void RegisterIOChangedAct()
        {
            //输入
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenUPtemp", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenDowntemp", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2UPtemp", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2Downtemp", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenPressure", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenVacuum", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenInnerdoorOpenstatus", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenOuterdoorOpenstatus", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenInnerdoorClosestatus", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenOuterdoorClosestatus", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2Pressure", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2Vacuum", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2InnerdoorOpenstatus", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2OuterdoorOpenstatus", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2InnerdoorClosestatus", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2OuterdoorClosestatus", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BoxPressure", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BoxVacuum", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BoxOuterdoorCloseSta", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BoxOuterdoorOpenSta", IOChanged);

            //输出
            //IOManager.Instance.RegisterIOChannelChangedEvent("TowerRedLight", IOChanged);
            //IOManager.Instance.RegisterIOChannelChangedEvent("TowerYellowLight", IOChanged);
            //IOManager.Instance.RegisterIOChannelChangedEvent("TowerGreenLight", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenBleed", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenExhaust", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenAerate", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenInnerdoorUp", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenInnerdoorDown", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOvenAutoHeat", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2Bleed", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2Exhaust", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2Aerate", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2InnerdoorUp", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2InnerdoorDown", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BakeOven2AutoHeat", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("BoxBleed", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BoxExhaust", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BoxAerate", IOChanged);

            IOManager.Instance.RegisterIOChannelChangedEvent("OvenPurgeResidueTime", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("HeatPreservationResidueMinute", IOChanged);
            IOManager.Instance.RegisterIOChannelChangedEvent("BoxPurgeResidueTime", IOChanged);
        }

        private void IOChanged(string ioName, object preValue, object newValue)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(() => IOChanged(ioName, preValue, newValue)));
                return;
            }
            try
            {
                bool BakeOvenInnerdoorIsOpen = false;
                bool BakeOvenInnerdoorIsClose = false;

                bool BakeOvenOuterdoorIsOpen = false;
                bool BakeOvenOuterdoorIsClose = false;

                bool BakeOvenInnerdoorIsOpen2 = false;
                bool BakeOvenInnerdoorIsClose2 = false;

                bool BakeOvenOuterdoorIsOpen2 = false;
                bool BakeOvenOuterdoorIsClose2 = false;

                bool BoxOuterdoorIsOpen = false;
                bool BoxOuterdoorIsClose = false;

                ExclusionHelper.Instance.IOChangedActWaitEvent.WaitOne();
                if (ioName == "BakeOvenPressure")
                {
                    this.teOvenPressure.Text = newValue.ToString();
                    if ((int)newValue > _systemConfig.OvenBoxConfig.OvenOverPressureThreshold)
                    {
                        labelOvenOverPressureAlarmStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelOvenOverPressureAlarmStatus.ForeColor = Color.Green;
                    }
                }
                else if (ioName == "BakeOvenVacuum")
                {
                    this.teOvenVacuumDegree.Text = newValue.ToString();
                }
                else if (ioName == "BakeOvenInnerdoorOpenstatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenInnerDoorOpenStatus.BackColor = Color.Red;
                        this.teOvenInnerDoorOpenStatus.Text = "打开";
                    }
                    else
                    {
                        BakeOvenInnerdoorIsOpen = true;
                    }
                }
                else if (ioName == "BakeOvenInnerdoorClosestatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenInnerDoorOpenStatus.BackColor = Color.Green;
                        this.teOvenInnerDoorOpenStatus.Text = "关闭";
                    }
                    else
                    {
                        BakeOvenInnerdoorIsClose = true;
                    }
                }
                else if (ioName == "BakeOvenOuterdoorOpenstatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenOutDoorOpenStatus.BackColor = Color.Red;
                        this.teOvenOutDoorOpenStatus.Text = "打开";
                    }
                    else
                    {
                        BakeOvenOuterdoorIsOpen = true;
                    }
                }
                else if (ioName == "BakeOvenOuterdoorClosestatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenOutDoorOpenStatus.BackColor = Color.Green;
                        this.teOvenOutDoorOpenStatus.Text = "关闭";
                    }
                    else
                    {
                        BakeOvenOuterdoorIsClose = true;
                    }
                }
                if (ioName == "BakeOven2Pressure")
                {
                    this.teOvenPressure2.Text = newValue.ToString();
                    if ((int)newValue > _systemConfig.OvenBoxConfig.Oven2OverPressureThreshold)
                    {
                        labelOvenOverPressureAlarmStatus2.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelOvenOverPressureAlarmStatus2.ForeColor = Color.Green;
                    }
                }
                else if (ioName == "BakeOven2Vacuum")
                {
                    this.teOvenVacuumDegree2.Text = newValue.ToString();
                }
                else if (ioName == "BakeOven2InnerdoorOpenstatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenInnerDoorOpenStatus2.BackColor = Color.Red;
                        this.teOvenInnerDoorOpenStatus2.Text = "打开";
                    }
                    else
                    {
                        BakeOvenInnerdoorIsOpen2 = true;
                    }
                }
                else if (ioName == "BakeOven2InnerdoorClosestatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenInnerDoorOpenStatus2.BackColor = Color.Green;
                        this.teOvenInnerDoorOpenStatus2.Text = "关闭";
                    }
                    else
                    {
                        BakeOvenInnerdoorIsClose2 = true;
                    }
                }
                else if (ioName == "BakeOven2OuterdoorOpenstatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenOutDoorOpenStatus2.BackColor = Color.Red;
                        this.teOvenOutDoorOpenStatus2.Text = "打开";
                    }
                    else
                    {
                        BakeOvenOuterdoorIsOpen2 = true;
                    }
                }
                else if (ioName == "BakeOven2OuterdoorClosestatus")
                {
                    if ((bool)newValue)
                    {
                        this.teOvenOutDoorOpenStatus2.BackColor = Color.Green;
                        this.teOvenOutDoorOpenStatus2.Text = "关闭";
                    }
                    else
                    {
                        BakeOvenOuterdoorIsClose2 = true;
                    }
                }
                else if (ioName == "BoxOuterdoorOpenSta")
                {
                    if ((bool)newValue)
                    {
                        this.teExchangeOuterDoorloosenStatus.BackColor = Color.Red;
                        this.teExchangeOuterDoorloosenStatus.Text = "打开";
                    }
                    else
                    {
                        BoxOuterdoorIsOpen = true;
                    }
                }
                else if (ioName == "BoxOuterdoorCloseSta")
                {
                    if ((bool)newValue)
                    {
                        this.teExchangeOuterDoorloosenStatus.BackColor = Color.Green;
                        this.teExchangeOuterDoorloosenStatus.Text = "关闭";
                    }
                    else
                    {
                        BoxOuterdoorIsClose = true;
                    }
                }
                else if (ioName == "BoxPressure")
                {
                    this.teExchangeBoxPressure.Text = newValue.ToString();
                }
                else if (ioName == "BoxVacuum")
                {
                    this.teExchangeBoxVacuumDegree.Text = newValue.ToString();
                }

                else if (ioName == "BakeOvenBleed")
                {
                    if (!(bool)newValue)
                    {
                        this.teOvenExtractionValveStatus.BackColor = Color.Green;
                        this.teOvenExtractionValveStatus.Text = "关闭";
                    }
                    else
                    {
                        this.teOvenExtractionValveStatus.BackColor = Color.Red;
                        this.teOvenExtractionValveStatus.Text = "打开";
                    }
                }
                else if (ioName == "BakeOvenExhaust")
                {
                    if (!(bool)newValue)
                    {
                        this.teOvenMakeupValveStatus.BackColor = Color.Green;
                        this.teOvenMakeupValveStatus.Text = "关闭";
                    }
                    else
                    {
                        this.teOvenMakeupValveStatus.BackColor = Color.Red;
                        this.teOvenMakeupValveStatus.Text = "打开";
                    }
                }
                else if (ioName == "BakeOvenAerate")
                {
                    if (!(bool)newValue)
                    {
                        this.teOvenExhaustValveStatus.BackColor = Color.Green;
                        this.teOvenExhaustValveStatus.Text = "关闭";
                    }
                    else
                    {
                        this.teOvenExhaustValveStatus.BackColor = Color.Red;
                        this.teOvenExhaustValveStatus.Text = "打开";
                    }
                }
                else if (ioName == "BakeOvenAutoHeat")
                {
                    if (!(bool)newValue)
                    {
                        this.teHeatState.BackColor = Color.Green;
                        this.teHeatState.Text = "关闭";
                    }
                    else
                    {
                        this.teHeatState.BackColor = Color.Red;
                        this.teHeatState.Text = "打开";
                    }
                }
                else if (ioName == "BakeOven2Bleed")
                {
                    if (!(bool)newValue)
                    {
                        this.teOvenExtractionValveStatus2.BackColor = Color.Green;
                        this.teOvenExtractionValveStatus2.Text = "关闭";
                    }
                    else
                    {
                        this.teOvenExtractionValveStatus2.BackColor = Color.Red;
                        this.teOvenExtractionValveStatus2.Text = "打开";
                    }
                }
                else if (ioName == "BakeOven2Exhaust")
                {
                    if (!(bool)newValue)
                    {
                        this.teOvenMakeupValveStatus2.BackColor = Color.Green;
                        this.teOvenMakeupValveStatus2.Text = "关闭";
                    }
                    else
                    {
                        this.teOvenMakeupValveStatus2.BackColor = Color.Red;
                        this.teOvenMakeupValveStatus2.Text = "打开";
                    }
                }
                else if (ioName == "BakeOven2Aerate")
                {
                    if (!(bool)newValue)
                    {
                        this.teOvenExhaustValveStatus2.BackColor = Color.Green;
                        this.teOvenExhaustValveStatus2.Text = "关闭";
                    }
                    else
                    {
                        this.teOvenExhaustValveStatus2.BackColor = Color.Red;
                        this.teOvenExhaustValveStatus2.Text = "打开";
                    }
                }
                else if (ioName == "BakeOven2AutoHeat")
                {
                    if (!(bool)newValue)
                    {
                        this.teHeatState2.BackColor = Color.Green;
                        this.teHeatState2.Text = "关闭";
                    }
                    else
                    {
                        this.teHeatState2.BackColor = Color.Red;
                        this.teHeatState2.Text = "打开";
                    }
                }

                else if (ioName == "BoxBleed")
                {
                    if (!(bool)newValue)
                    {
                        this.teExchangeExtractionValveStatus.BackColor = Color.Green;
                        this.teExchangeExtractionValveStatus.Text = "关闭";
                    }
                    else
                    {
                        this.teExchangeExtractionValveStatus.BackColor = Color.Red;
                        this.teExchangeExtractionValveStatus.Text = "打开";
                    }
                }
                else if (ioName == "BoxExhaust")
                {
                    if (!(bool)newValue)
                    {
                        this.teExchangeMakeupValveStatus.BackColor = Color.Green;
                        this.teExchangeMakeupValveStatus.Text = "关闭";
                    }
                    else
                    {
                        this.teExchangeMakeupValveStatus.BackColor = Color.Red;
                        this.teExchangeMakeupValveStatus.Text = "打开";
                    }
                }
                else if (ioName == "BoxAerate")
                {
                    if (!(bool)newValue)
                    {
                        this.teExchangeExhaustValveStatus.BackColor = Color.Green;
                        this.teExchangeExhaustValveStatus.Text = "关闭";
                    }
                    else
                    {
                        this.teExchangeExhaustValveStatus.BackColor = Color.Red;
                        this.teExchangeExhaustValveStatus.Text = "打开";
                    }
                }
                else if (ioName == "OvenPurgeResidueTime")
                {
                    this.teOvenPurgeResidueTime.Text = newValue.ToString();
                }
                else if (ioName == "HeatPreservationResidueMinute")
                {
                    this.teHeatPreservationResidueMinute.Text = newValue.ToString();
                }
                else if (ioName == "BoxPurgeResidueTime")
                {
                    this.teExchangePurgeResidueTime.Text = newValue.ToString();
                }
                else if (ioName == "BakeOvenUPtemp")
                {
                    this.teTopZoneTemperature.Text = newValue.ToString();
                    if ((int)newValue > _systemConfig.OvenBoxConfig.OverTemperatureThreshold)
                    {
                        labelOverTempAlarmStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelOverTempAlarmStatus.ForeColor = Color.Green;
                    }
                }
                else if (ioName == "BakeOvenDowntemp")
                {
                    this.teBottomZoneTemperature.Text = newValue.ToString();
                    if ((int)newValue > _systemConfig.OvenBoxConfig.OverTemperatureThreshold)
                    {
                        labelOverTempAlarmStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelOverTempAlarmStatus.ForeColor = Color.Green;
                    }
                }
                else if (ioName == "BakeOven2UPtemp")
                {
                    this.teTopZoneTemperature2.Text = newValue.ToString();
                    if ((int)newValue > _systemConfig.OvenBoxConfig.OverTemperatureThreshold2)
                    {
                        labelOverTempAlarmStatus2.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelOverTempAlarmStatus2.ForeColor = Color.Green;
                    }
                }
                else if (ioName == "BakeOven2Downtemp")
                {
                    this.teBottomZoneTemperature2.Text = newValue.ToString();
                    if ((int)newValue > _systemConfig.OvenBoxConfig.OverTemperatureThreshold2)
                    {
                        labelOverTempAlarmStatus2.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelOverTempAlarmStatus2.ForeColor = Color.Green;
                    }
                }



                if (!BakeOvenInnerdoorIsOpen && !BakeOvenInnerdoorIsClose)
                {
                    this.teOvenInnerDoorOpenStatus.BackColor = Color.Yellow;
                    this.teOvenInnerDoorOpenStatus.Text = "中间";
                }
                if (!BakeOvenOuterdoorIsOpen && !BakeOvenOuterdoorIsClose)
                {
                    this.teOvenOutDoorOpenStatus.BackColor = Color.Yellow;
                    this.teOvenOutDoorOpenStatus.Text = "中间";
                }
                if (!BakeOvenInnerdoorIsOpen2 && !BakeOvenInnerdoorIsClose2)
                {
                    this.teOvenInnerDoorOpenStatus2.BackColor = Color.Yellow;
                    this.teOvenInnerDoorOpenStatus2.Text = "中间";
                }
                if (!BakeOvenOuterdoorIsOpen2 && !BakeOvenOuterdoorIsClose2)
                {
                    this.teOvenOutDoorOpenStatus2.BackColor = Color.Yellow;
                    this.teOvenOutDoorOpenStatus2.Text = "中间";
                }
                if (!BoxOuterdoorIsOpen && !BoxOuterdoorIsClose)
                {
                    this.teExchangeOuterDoorloosenStatus.BackColor = Color.Yellow;
                    this.teExchangeOuterDoorloosenStatus.Text = "中间";
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                ExclusionHelper.Instance.IOChangedActWaitEvent.Set();
            }
        }



        private void btnOpenOvenExtractionValve_Click(object sender, EventArgs e)
        {
            if(_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenBleed, btnOpenOvenExtractionValve.Checked);
            }
        }

        private void btnOpenOvenMakeupValve_Click(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenAerate, btnOpenOvenMakeupValve.Checked);
            }
        }

        private void btnOpenOvenExhaustValve_Click(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenExhaust, btnOpenOvenExhaustValve.Checked);
            }
        }

        private void btnOpenOvenExtractionValve2_Click(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2Bleed, btnOpenOvenExtractionValve.Checked);
            }
        }

        private void btnOpenOvenMakeupValve2_Click(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2Aerate, btnOpenOvenMakeupValve.Checked);
            }
        }

        private void btnOpenOvenExhaustValve2_Click(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2Exhaust, btnOpenOvenExhaustValve.Checked);
            }
        }

        private void btnOpenOvenInnerDoor_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUp, true);

            }
        }
        private void btnOpenOvenInnerDoor_MouseUp(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorUp, false);

            }
        }
        private void btnCloseOvenInnerDoor_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorDown, true);

            }
        }

        private void btnCloseOvenInnerDoor_MouseUp(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOvenInnerdoorDown, false);

            }
        }

        private void btnLinkageOpenOvenInnerDoor_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                float BakeOvenPressure = _plc.Read<float>(EnumBoardcardDefineInputIO.BakeOvenPressure);
                if (BakeOvenPressure > 90000)
                {
                    _plc.OpenOvenboxInnerDoor(EnumOvenBoxNum.Oven1);
                }
                    
            }
        }

        private void btnLinkageOpenOvenInnerDoor_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void btnLinkageCloseOvenInnerDoor_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.CloseOvenboxInnerDoor(EnumOvenBoxNum.Oven1);
            }
        }

        private void btnLinkageCloseOvenInnerDoor_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnLinkageOpenOvenInnerDoor2_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                float BakeOvenPressure = _plc.Read<float>(EnumBoardcardDefineInputIO.BakeOven2Pressure);
                if (BakeOvenPressure > 90000)
                {
                    _plc.OpenOvenboxInnerDoor(EnumOvenBoxNum.Oven2);
                }

            }
        }


        private void btnLinkageCloseOvenInnerDoor2_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.CloseOvenboxInnerDoor(EnumOvenBoxNum.Oven2);
            }
        }


        private void btnOpenOvenInnerDoor2_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUp, true);

            }
        }
        private void btnOpenOvenInnerDoor2_MouseUp(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorUp, false);

            }
        }
        private void btnCloseOvenInnerDoor2_MouseDown(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorDown, true);

            }
        }

        private void btnCloseOvenInnerDoor2_MouseUp(object sender, MouseEventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BakeOven2InnerdoorDown, false);

            }
        }





        private void btnOvenBoxPurge_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                if(btnOvenBoxPurge.Checked)
                {
                    if(OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
                    {
                        float BakeOvenPFUpPressure = float.Parse(seOvenPurgePressureUpperLimit.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit = (int)BakeOvenPFUpPressure;
                        float BakeOvenPFDownPressure = float.Parse(seOvenPurgePressureLowerLimit.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit = (int)BakeOvenPFDownPressure;
                        short BakeOvenPFnum = short.Parse(seOvenPurgeTime.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgeTime = BakeOvenPFnum;
                        short BakeOvenPFinterval = short.Parse(seOvenPurgeInterval.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgeInterval = BakeOvenPFinterval;

                        _plc.Purge( EnumOvenBoxNum.Oven1, _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit, _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit, _systemConfig.OvenBoxConfig.OvenPurgeTime, _systemConfig.OvenBoxConfig.OvenPurgeInterval);

                    }
                    else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
                    {
                        float BakeOvenPFUpPressure = float.Parse(seOvenPurgePressureUpperLimit.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgePressureUpperLimit = (int)BakeOvenPFUpPressure;
                        float BakeOvenPFDownPressure = float.Parse(seOvenPurgePressureLowerLimit.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgePressureLowerLimit = (int)BakeOvenPFDownPressure;
                        short BakeOvenPFnum = short.Parse(seOvenPurgeTime.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgeTime = BakeOvenPFnum;
                        short BakeOvenPFinterval = short.Parse(seOvenPurgeInterval.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgeInterval = BakeOvenPFinterval;

                        _plc.Purge(EnumOvenBoxNum.Oven2, _systemConfig.OvenBoxConfig.Oven2PurgePressureUpperLimit, _systemConfig.OvenBoxConfig.Oven2PurgePressureLowerLimit, _systemConfig.OvenBoxConfig.Oven2PurgeTime, _systemConfig.OvenBoxConfig.Oven2PurgeInterval);

                    }
                }
                else
                {
                    _plc.StopPurge();
                }
            }
        }

        private void btnManualHeat_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                if (btnManualHeat.Checked)
                {
                    if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
                    {
                        float BakeOvenTargettemp = float.Parse(seHeatTargetTemperature.Text);
                        _systemConfig.OvenBoxConfig.HeatTargetTemperature = (int)BakeOvenTargettemp;

                        float BakeOvenAlarmtemp = float.Parse(seOverTemperatureThreshold.Text);
                        _systemConfig.OvenBoxConfig.OverTemperatureThreshold = (int)BakeOvenAlarmtemp;

                        short BakeOvenHoldingTimeM = short.Parse(seHeatPreservationMinute.Text);
                        _systemConfig.OvenBoxConfig.HeatPreservationMinute = (int)BakeOvenHoldingTimeM;


                        _plc.ManualHeat(EnumOvenBoxNum.Oven1, _systemConfig.OvenBoxConfig.HeatTargetTemperature, _systemConfig.OvenBoxConfig.HeatPreservationMinute, _systemConfig.OvenBoxConfig.OverTemperatureThreshold);

                    }
                    else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
                    {
                        float BakeOvenTargettemp = float.Parse(seHeatTargetTemperature.Text);
                        _systemConfig.OvenBoxConfig.HeatTargetTemperature2 = (int)BakeOvenTargettemp;

                        float BakeOvenAlarmtemp = float.Parse(seOverTemperatureThreshold.Text);
                        _systemConfig.OvenBoxConfig.OverTemperatureThreshold2 = (int)BakeOvenAlarmtemp;

                        short BakeOvenHoldingTimeM = short.Parse(seHeatPreservationMinute.Text);
                        _systemConfig.OvenBoxConfig.HeatPreservationMinute2 = (int)BakeOvenHoldingTimeM;


                        _plc.ManualHeat(EnumOvenBoxNum.Oven2, _systemConfig.OvenBoxConfig.HeatTargetTemperature2, _systemConfig.OvenBoxConfig.HeatPreservationMinute2, _systemConfig.OvenBoxConfig.OverTemperatureThreshold2);

                    }
                }
                else
                {
                    _plc.StopManualHeat();
                }
            }
        }

        private void btnAutoHeat_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                if (btnAutoHeat.Checked)
                {
                    if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
                    {
                        float BakeOvenPFUpPressure = float.Parse(seOvenPurgePressureUpperLimit.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit = (int)BakeOvenPFUpPressure;
                        float BakeOvenPFDownPressure = float.Parse(seOvenPurgePressureLowerLimit.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit = (int)BakeOvenPFDownPressure;
                        short BakeOvenPFnum = short.Parse(seOvenPurgeTime.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgeTime = BakeOvenPFnum;
                        short BakeOvenPFinterval = short.Parse(seOvenPurgeInterval.Text);
                        _systemConfig.OvenBoxConfig.OvenPurgeInterval = BakeOvenPFinterval;

                        float BakeOvenTargettemp = float.Parse(seHeatTargetTemperature.Text);
                        _systemConfig.OvenBoxConfig.HeatTargetTemperature = (int)BakeOvenTargettemp;

                        float BakeOvenAlarmtemp = float.Parse(seOverTemperatureThreshold.Text);
                        _systemConfig.OvenBoxConfig.OverTemperatureThreshold = (int)BakeOvenAlarmtemp;

                        short BakeOvenHoldingTimeM = short.Parse(seHeatPreservationMinute.Text);
                        _systemConfig.OvenBoxConfig.HeatPreservationMinute = (int)BakeOvenHoldingTimeM;


                        _plc.AutoHeat(EnumOvenBoxNum.Oven1, _systemConfig.OvenBoxConfig.HeatTargetTemperature, _systemConfig.OvenBoxConfig.HeatPreservationMinute, _systemConfig.OvenBoxConfig.OverTemperatureThreshold, _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit, _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit, _systemConfig.OvenBoxConfig.OvenPurgeTime, _systemConfig.OvenBoxConfig.OvenPurgeInterval);

                    }
                    else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
                    {
                        float BakeOvenPFUpPressure = float.Parse(seOvenPurgePressureUpperLimit.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgePressureUpperLimit = (int)BakeOvenPFUpPressure;
                        float BakeOvenPFDownPressure = float.Parse(seOvenPurgePressureLowerLimit.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgePressureLowerLimit = (int)BakeOvenPFDownPressure;
                        short BakeOvenPFnum = short.Parse(seOvenPurgeTime.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgeTime = BakeOvenPFnum;
                        short BakeOvenPFinterval = short.Parse(seOvenPurgeInterval.Text);
                        _systemConfig.OvenBoxConfig.Oven2PurgeInterval = BakeOvenPFinterval;

                        float BakeOvenTargettemp = float.Parse(seHeatTargetTemperature.Text);
                        _systemConfig.OvenBoxConfig.HeatTargetTemperature2 = (int)BakeOvenTargettemp;

                        float BakeOvenAlarmtemp = float.Parse(seOverTemperatureThreshold.Text);
                        _systemConfig.OvenBoxConfig.OverTemperatureThreshold2 = (int)BakeOvenAlarmtemp;

                        short BakeOvenHoldingTimeM = short.Parse(seHeatPreservationMinute.Text);
                        _systemConfig.OvenBoxConfig.HeatPreservationMinute2 = (int)BakeOvenHoldingTimeM;


                        _plc.AutoHeat(EnumOvenBoxNum.Oven2, _systemConfig.OvenBoxConfig.HeatTargetTemperature2, _systemConfig.OvenBoxConfig.HeatPreservationMinute2, _systemConfig.OvenBoxConfig.OverTemperatureThreshold2, _systemConfig.OvenBoxConfig.Oven2PurgePressureUpperLimit, _systemConfig.OvenBoxConfig.Oven2PurgePressureLowerLimit, _systemConfig.OvenBoxConfig.Oven2PurgeTime, _systemConfig.OvenBoxConfig.Oven2PurgeInterval);

                    }
                }
                else
                {
                    _plc.StopAutoHeat();
                }
            }
        }

        private void btnOpenExchangeExtractionValve_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BoxBleed, btnOpenExchangeExtractionValve.Checked);
            }
        }

        private void btnOpenExchangeMakeupValve_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BoxAerate, btnOpenExchangeMakeupValve.Checked);
            }
        }

        private void btnOpenExchangeExhaustValve_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                _plc.Write<bool>(EnumBoardcardDefineOutputIO.BoxExhaust, btnOpenExchangeExhaustValve.Checked);
            }
        }


        private void btnExchangeBoxPurge_CheckedChanged(object sender, EventArgs e)
        {
            if (_plc.IsConnected)
            {
                if (btnExchangeBoxPurge.Checked)
                {

                    float BoxPFUpPressure = float.Parse(seExchangePurgePressureUpperLimit.Text);
                    _systemConfig.OvenBoxConfig.BoxPurgePressureUpperLimit = (int)BoxPFUpPressure;
                    float BoxPFDownPressure = float.Parse(seExchangePurgePressureLowerLimit.Text);
                    _systemConfig.OvenBoxConfig.BoxPurgePressureLowerLimit = (int)BoxPFDownPressure;
                    short BoxPFnum = short.Parse(seExchangePurgeTimes.Text);
                    _systemConfig.OvenBoxConfig.BoxPurgeTime = BoxPFnum;
                    short BoxPFinterval = short.Parse(seExchangePurgeInterval.Text);
                    _systemConfig.OvenBoxConfig.BoxPurgeInterval = BoxPFinterval;

                    _plc.BoxPurge(_systemConfig.OvenBoxConfig.BoxPurgePressureUpperLimit, _systemConfig.OvenBoxConfig.BoxPurgePressureLowerLimit, _systemConfig.OvenBoxConfig.BoxPurgeTime, _systemConfig.OvenBoxConfig.BoxPurgeInterval);

                }
                else
                {
                    _plc.StopBoxPurge();
                }
            }
        }


        private void seOvenOverPressureThreshold_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.OvenOverPressureThreshold = (int)this.seOvenOverPressureThreshold.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.Oven2OverPressureThreshold = (int)this.seOvenOverPressureThreshold.Value;
            }
        }
        private void seOvenPurgeTime_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.OvenPurgeTime = (int)this.seOvenPurgeTime.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.OvenPurgeTime = (int)this.seOvenPurgeTime.Value;
            }
        }

        private void seOvenPurgeInterval_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.OvenPurgeInterval = (int)this.seOvenPurgeInterval.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.Oven2PurgeInterval = (int)this.seOvenPurgeInterval.Value;
            }
        }

        private void seOvenPurgePressureUpperLimit_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit = (int)this.seOvenPurgePressureUpperLimit.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.Oven2PurgePressureUpperLimit = (int)this.seOvenPurgePressureUpperLimit.Value;
            }
        }

        private void seOvenPurgePressureLowerLimit_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit = (int)this.seOvenPurgePressureLowerLimit.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.Oven2PurgePressureLowerLimit = (int)this.seOvenPurgePressureLowerLimit.Value;
            }
        }

        private void seHeatTargetTemperature_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.HeatTargetTemperature = (int)this.seHeatTargetTemperature.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.HeatTargetTemperature2 = (int)this.seHeatTargetTemperature.Value;
            }
        }

        private void seHeatPreservationMinute_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.HeatPreservationMinute = (int)this.seHeatPreservationMinute.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.HeatPreservationMinute2 = (int)this.seHeatPreservationMinute.Value;
            }
        }

        private void seOverTemperatureThreshold_EditValueChanged(object sender, EventArgs e)
        {
            if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                _systemConfig.OvenBoxConfig.OverTemperatureThreshold = (int)this.seOverTemperatureThreshold.Value;
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                _systemConfig.OvenBoxConfig.OverTemperatureThreshold2 = (int)this.seOverTemperatureThreshold.Value;
            }
        }

        

        private void seExchangePurgeTimes_EditValueChanged(object sender, EventArgs e)
        {
            _systemConfig.OvenBoxConfig.BoxPurgeTime = (int)this.seExchangePurgeTimes.Value;
        }

        private void seExchangePurgeInterval_EditValueChanged(object sender, EventArgs e)
        {
            _systemConfig.OvenBoxConfig.BoxPurgeInterval = (int)this.seExchangePurgeInterval.Value;
        }

        private void seExchangePurgePressureUpperLimit_EditValueChanged(object sender, EventArgs e)
        {
            _systemConfig.OvenBoxConfig.BoxPurgePressureUpperLimit = (int)this.seExchangePurgePressureUpperLimit.Value;
        }

        private void seExchangePurgePressureLowerLimit_EditValueChanged(object sender, EventArgs e)
        {
            _systemConfig.OvenBoxConfig.BoxPurgePressureLowerLimit = (int)this.seExchangePurgePressureLowerLimit.Value;
        }

        private void OvenBoxControlPanel_Load(object sender, EventArgs e)
        {
            Initialize();
            RegisterIOChangedAct();
        }

        private void OvenBoxNumcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyOvenBox = OvenBoxNumcomboBox.SelectedIndex;

            if(OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven1)
            {
                //烘箱加热参数
                float BakeOvenTargettemp = _systemConfig.OvenBoxConfig.HeatTargetTemperature;
                seHeatTargetTemperature.Text = BakeOvenTargettemp.ToString();

                int BakeOvenHoldingTimeM = _systemConfig.OvenBoxConfig.HeatPreservationMinute;
                seHeatPreservationMinute.Text = BakeOvenHoldingTimeM.ToString();

                short BakeOvenPassedTimeM = 0;
                teHeatPreservationResidueMinute.Text = BakeOvenPassedTimeM.ToString();

                float BakeOvenAlarmtemp = _systemConfig.OvenBoxConfig.OverTemperatureThreshold;
                seOverTemperatureThreshold.Text = BakeOvenAlarmtemp.ToString();


                //烘箱抽充参数
                float BakeOvenAlarmPressure = _systemConfig.OvenBoxConfig.OvenOverPressureThreshold;
                seOvenOverPressureThreshold.Text = BakeOvenAlarmPressure.ToString();

                float BakeOvenPFUpPressure = _systemConfig.OvenBoxConfig.OvenPurgePressureUpperLimit;
                seOvenPurgePressureUpperLimit.Text = BakeOvenPFUpPressure.ToString();

                float BakeOvenPFDownPressure = _systemConfig.OvenBoxConfig.OvenPurgePressureLowerLimit;
                seOvenPurgePressureLowerLimit.Text = BakeOvenPFDownPressure.ToString();

                int BakeOvenPFnum = _systemConfig.OvenBoxConfig.OvenPurgeTime;
                seOvenPurgeTime.Text = BakeOvenPFnum.ToString();

                int BakeOvenPFCompletednum = 0;
                teOvenPurgeResidueTime.Text = BakeOvenPFCompletednum.ToString();

                int BakeOvenPFinterval = _systemConfig.OvenBoxConfig.OvenPurgeInterval;
                seOvenPurgeInterval.Text = BakeOvenPFinterval.ToString();
            }
            else if (OvenBoxNumcomboBox.SelectedIndex == (int)EnumOvenBoxNum.Oven2)
            {
                //烘箱加热参数
                float BakeOvenTargettemp = _systemConfig.OvenBoxConfig.HeatTargetTemperature2;
                seHeatTargetTemperature.Text = BakeOvenTargettemp.ToString();

                int BakeOvenHoldingTimeM = _systemConfig.OvenBoxConfig.HeatPreservationMinute2;
                seHeatPreservationMinute.Text = BakeOvenHoldingTimeM.ToString();

                short BakeOvenPassedTimeM = 0;
                teHeatPreservationResidueMinute.Text = BakeOvenPassedTimeM.ToString();

                float BakeOvenAlarmtemp = _systemConfig.OvenBoxConfig.OverTemperatureThreshold2;
                seOverTemperatureThreshold.Text = BakeOvenAlarmtemp.ToString();


                //烘箱抽充参数
                float BakeOvenAlarmPressure = _systemConfig.OvenBoxConfig.Oven2OverPressureThreshold;
                seOvenOverPressureThreshold.Text = BakeOvenAlarmPressure.ToString();

                float BakeOvenPFUpPressure = _systemConfig.OvenBoxConfig.Oven2PurgePressureUpperLimit;
                seOvenPurgePressureUpperLimit.Text = BakeOvenPFUpPressure.ToString();

                float BakeOvenPFDownPressure = _systemConfig.OvenBoxConfig.Oven2PurgePressureLowerLimit;
                seOvenPurgePressureLowerLimit.Text = BakeOvenPFDownPressure.ToString();

                int BakeOvenPFnum = _systemConfig.OvenBoxConfig.Oven2PurgeTime;
                seOvenPurgeTime.Text = BakeOvenPFnum.ToString();

                int BakeOvenPFCompletednum = 0;
                teOvenPurgeResidueTime.Text = BakeOvenPFCompletednum.ToString();

                int BakeOvenPFinterval = _systemConfig.OvenBoxConfig.Oven2PurgeInterval;
                seOvenPurgeInterval.Text = BakeOvenPFinterval.ToString();
            }

        }

        private void seExchangeOverPressureThreshold_EditValueChanged(object sender, EventArgs e)
        {
            _systemConfig.OvenBoxConfig.BoxOverPressureThreshold = (int)this.seExchangeOverPressureThreshold.Value;
        }
    }



    public class ExclusionHelper
    {
        public AutoResetEvent _ioChangedAct = new AutoResetEvent(true);
        public AutoResetEvent IOChangedActWaitEvent { get { return _ioChangedAct; } }
        private static readonly object _lockObj = new object();
        private static volatile ExclusionHelper _instance = null;
        public static ExclusionHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ExclusionHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        private ExclusionHelper()
        {
            //Initialize();
        }
    }

}
