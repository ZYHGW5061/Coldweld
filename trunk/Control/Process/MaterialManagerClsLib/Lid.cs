using System;
using System.Drawing;
using System.Collections.Generic;
using GlobalDataDefineClsLib;

namespace MaterialManagerClsLib
{
    public enum EnumWaferAlignState
    {
        UnAligned = 0,
        Alignning = 1,
        Aligned = 2,
        AlignFailed = 3
    }

    public enum EnumWaferMeasureState
    {
        Undetect = 0,
        Detecting = 1,
        Detected = 2,
        DetectFailed = 3
    }

    public enum EnumSubstType
    {
        Wafer,
        FlatPanel,
        CD,
        Mask
    }

    public enum EnumSubstState
    {
        NoState,
        AtSource,
        AtWork,
        AtDestination,
        Extinction
    }

    public enum EnumSubstUsage
    {
        Product,
        Test,
        Filler,
        Cleaning
    }

    public enum EnumWaferType
    {
        Normal,
        Error,
        UnknowOrigin,
        Empty,
        Unknow
    }

    public class Substrate
    {
        #region "E90属性"

        public string LotID { get; set; }

        public string ObjID { get; set; }

        public string ObjType { get { return "Substrate"; } }

        public string SubstDestination { get; set; }

        /// <summary>
        /// Wafer location ID, timeIn, timeOut
        /// </summary>
        public List<SubstHistory> SubstHistoryList { get; set; }

        /// <summary>
        /// Substrate location ID
        /// </summary>
        public string SubstLocID { get; set; }
        
        public EnumSubstProcState SubstProcState { get; set; }

        public EnumSubstLocState SubstSource { get; set; }

        /// <summary>
        /// Substrate transport state
        /// </summary>
        public EnumSubstState SubstState { get; set; }

        public EnumSubstType SubstType { get; set; }

        public EnumSubstUsage SubstUsage { get; set; }

        #endregion

        public Substrate(string id, string lotID = "")
        {
            this.ObjID = id;
            this.LotID = lotID;
            this.SubstHistoryList = new List<SubstHistory>();
        }

    }

    public class SubstHistory
    {
        /// <summary>
        /// Substrate location ID
        /// </summary>
        public string SubstLocID { get; set; }

        /// <summary>
        /// Arrival time of substrate on the substrate location
        /// </summary>
        public string TimeIn { get; set; }

        /// <summary>
        /// departure time of substrate on the substrate location
        /// </summary>
        public string TimeOut { get; set; }
    }


    public class Lid
    {
        #region "E90属性"

        public string LotID { get; set; }

        public string WaferID { get; set; }

        public string ObjType { get { return "Substrate"; } }

        public string SubstDestination { get; set; }

        /// <summary>
        /// Wafer location ID, timeIn, timeOut
        /// </summary>
        public List<SubstHistory> SubstHistoryList { get; set; }

        /// <summary>
        /// Substrate location ID
        /// </summary>
        public string SubstLocID { get; set; }

        private EnumSubstProcState _substProcState = EnumSubstProcState.NoState;

        public EnumSubstProcState _previousSubstProcState = EnumSubstProcState.NoState;

        /// <summary>
        /// Substrate Processing state
        /// </summary>
        public EnumSubstProcState SubstProcState
        {
            get { return _substProcState; }
            private set
            {
                if (_substProcState != value)
                {
                    _previousSubstProcState = _substProcState;
                    _substProcState = value;
                    if (SubstProcStateChangeEvt != null)
                    {
                        SubstProcStateChangeEvt.Invoke(this, _previousSubstProcState, value);
                    }                    
                }
            }
        }


        public EnumSubstLocState SubstSource { get; set; }


        private EnumSubstState _substState = EnumSubstState.NoState;
        private EnumSubstState _previousSubstState = EnumSubstState.NoState;
        /// <summary>
        /// Substrate transport state
        /// </summary>
        public EnumSubstState SubstState
        {
            get { return _substState; }
            private set
            {
                if (_substState != value)
                {
                    _previousSubstState = _substState;
                    _substState = value;
                    if (SubstProcStateChangeEvt != null)
                    {
                        SubstStateChangeEvt.Invoke(this, _previousSubstState, value);
                    }
                }
            }
        }

        public EnumSubstType SubstType { get; set; }

        public EnumSubstUsage SubstUsage { get; set; }

        #endregion

        #region "事件"
        public Action<Lid, EnumSubstProcState, EnumSubstProcState> SubstProcStateChangeEvt { get; set; }

        public Action<Lid, EnumSubstState, EnumSubstState> SubstStateChangeEvt { get; set; }
        #endregion

        /// <summary>
        /// 内部进行索引的ID
        /// </summary>
        public string WaferInternalID { get; private set; }

        public EnumWaferType WaferType { get; set; }

        public EnumWaferSize WaferSize { get; set; }

        /// <summary>
        /// Wafer在执行工艺前所在的原始loadport
        /// </summary>
        public EnumEFEMStationID OriginalLoadport { get; set; }

        /// <summary>
        /// 记录历史记录的唯一ID，用于和各检测端的记录关联（内部使用，需要外部赋值）
        /// </summary>
        public string InspectionID { get; set; }

        #region "状态"
        public bool SelectState { get; set; }

        public EnumWaferAlignState AlignState { get; set; }

        public EnumWaferMeasureState MeasureState { get; set; }

        /// <summary>
        /// 流程中的组合状态
        /// </summary>
        public EnumWaferMissionState WaferMissionState
        {
            get { return this.GetWaferMissionState(); }
        }

        /// <summary>
        /// 组合状态对应的颜色
        /// </summary>
        public Color WaferMissionColor
        {
            get { return GlobalParameterSetting.WaferMissionStateColorDic[WaferMissionState]; }
        }
        #endregion

        #region 位置
        /// <summary>
        /// 原始位置，来自哪里
        /// </summary>
        public WaferPosition OriginalPosition;
        public WaferPosition UltimatePosition;
        public WaferPosition Position;
        public WaferPosition StepSourcePosition;
        public WaferPosition StepDestinationPosition;

        public List<WaferPosition> PositionHistory { get; private set; }

        public int OriginalIndex
        {
            get { return OriginalPosition.WaferNumber; }
        }

        public int UltimateIndex
        {
            get { return UltimatePosition.WaferNumber; }
        }

        #endregion

        public Lid()
        {
            this.WaferID = "";
            this.LotID = "";
            this.WaferSize = EnumWaferSize.INVALID;
            this.OriginalLoadport = EnumEFEMStationID.Loadport1;
        }

        public Lid(string id, string lotID = "")
        {
            this.WaferID = id;
            this.LotID = lotID;
            this.SubstHistoryList = new List<SubstHistory>();
        }

        private EnumWaferMissionState GetWaferMissionState()
        {
            EnumWaferMissionState temp;
            if (this.WaferType == EnumWaferType.Unknow)
            {
                temp = EnumWaferMissionState.Unknow;
            }
            else if (this.WaferType == EnumWaferType.Empty)
            {
                temp = EnumWaferMissionState.NoWafer;
            }
            else if (this.WaferType == EnumWaferType.Error)
            {
                temp = EnumWaferMissionState.ErrorWafer;
            }
            else if (this.WaferType == EnumWaferType.UnknowOrigin)
            {
                temp = EnumWaferMissionState.UnknowOriginWafer;
            }

            else if (this.SelectState == false)
            {
                temp = EnumWaferMissionState.Unselect;
            }

            //else if (this.MeasureState == EnumWaferMeasureState.Detected &&
            //         this.Position.Equals(this.UltimatePosition))
            //{
            //    temp = EnumWaferMissionState.CompletePass;
            //}
            else if (this.MeasureState == EnumWaferMeasureState.Detected)
            {
                temp = EnumWaferMissionState.Measured;
            }
            else if (this.MeasureState == EnumWaferMeasureState.Detecting)
            {
                temp = EnumWaferMissionState.Measuring;
            }
            else if (this.MeasureState == EnumWaferMeasureState.DetectFailed)
            {
                temp = EnumWaferMissionState.MeasureFailed;
            }
            else if (this.AlignState == EnumWaferAlignState.Aligned)
            {
                temp = EnumWaferMissionState.Aligned;
            }
            else if (this.AlignState == EnumWaferAlignState.Alignning)
            {
                temp = EnumWaferMissionState.Aligning;
            }
            else if (this.AlignState == EnumWaferAlignState.AlignFailed)
            {
                temp = EnumWaferMissionState.AlignFailed;
            }
            else if (this.SelectState)
            {
                temp = EnumWaferMissionState.Selected;
            }
            else
            {
                throw new Exception("WaferMission状态超出预期");
            }
            return temp;
        }

        private EnumSubstProcState GetSubstProcState()
        {
            switch (WaferMissionState)
            {
                case EnumWaferMissionState.Unknow:
                case EnumWaferMissionState.NoWafer:
                case EnumWaferMissionState.ErrorWafer:
                case EnumWaferMissionState.UnknowOriginWafer:              
                    return EnumSubstProcState.Rejected;

                case EnumWaferMissionState.Unselect:
                    return EnumSubstProcState.Skipped;

                case EnumWaferMissionState.Selected:
                    return EnumSubstProcState.NeedsProcessing;

                case EnumWaferMissionState.PreAlign:
                case EnumWaferMissionState.Aligning:
                case EnumWaferMissionState.Aligned:
                case EnumWaferMissionState.Measuring:
                    return EnumSubstProcState.InProcess;

                case EnumWaferMissionState.Measured:
                case EnumWaferMissionState.CompletePass:
                case EnumWaferMissionState.CompleteNotPass:
                    return EnumSubstProcState.Processed;
                case EnumWaferMissionState.AlignFailed:
                case EnumWaferMissionState.MeasureFailed:
                    return EnumSubstProcState.Aborted;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Lid(EnumEFEMStationID loadportID, int waferNumber, string waferID, EnumWaferType waferType, EnumWaferSize waferSize)
        {
            switch (waferType)
            {
                case EnumWaferType.Empty://后续考虑生成一个静态的Wafer
                //throw new Exception("Can't create empty wafer.");

                case EnumWaferType.Unknow://后续考虑生成一个静态的Wafer
                //throw new Exception("Can't create unknow wafer.");
                case EnumWaferType.Normal:
                    OriginalPosition.StationID = loadportID;
                    OriginalPosition.WaferNumber = waferNumber;
                    UltimatePosition = OriginalPosition;
                    Position.StationID = loadportID;
                    Position.WaferNumber = waferNumber;
                    StepSourcePosition.StationID = loadportID;
                    StepSourcePosition.WaferNumber = waferNumber;
                    StepDestinationPosition.StationID = loadportID;
                    StepDestinationPosition.WaferNumber = waferNumber;
                    break;

                case EnumWaferType.Error:
                    OriginalPosition.StationID = loadportID;
                    OriginalPosition.WaferNumber = waferNumber;
                    Position.StationID = loadportID;
                    Position.WaferNumber = waferNumber;
                    UltimatePosition.StationID = EnumEFEMStationID.None;
                    StepSourcePosition.StationID = EnumEFEMStationID.None;
                    StepDestinationPosition.StationID = EnumEFEMStationID.None;
                    break;

                case EnumWaferType.UnknowOrigin:
                    OriginalPosition.StationID = EnumEFEMStationID.None;
                    Position.StationID = loadportID;
                    Position.WaferNumber = waferNumber;
                    UltimatePosition.StationID = EnumEFEMStationID.None;
                    StepSourcePosition.StationID = EnumEFEMStationID.None;
                    StepDestinationPosition.StationID = EnumEFEMStationID.None;
                    break;

                default:
                    throw new Exception(string.Format("Can't genarate {0} wafer", waferType));
            }

            LotID = "";
            WaferInternalID = waferID;
            WaferID = WaferInternalID;
            WaferType = waferType;
            WaferSize = waferSize;
            SelectState = false;
            AlignState = EnumWaferAlignState.UnAligned;
            MeasureState = EnumWaferMeasureState.Undetect;
            PositionHistory = new List<WaferPosition>();
            PositionHistory.Add(new WaferPosition() { StationID = loadportID, WaferNumber = waferNumber });
        }

        public static string CreateWaferID(EnumEFEMStationID originalLoadPortID, int originalWaferNumber)
        {
            return string.Format("{0}.{1}", originalLoadPortID, originalWaferNumber);
        }

        public void ChangeWaferType(EnumWaferType waferType)
        {
            WaferType = waferType;
            switch(WaferType)
            {
                case EnumWaferType.Error:
                case EnumWaferType.Unknow:
                case EnumWaferType.UnknowOrigin:
                case EnumWaferType.Empty:
                    SubstProcState = EnumSubstProcState.Rejected;
                    break;

            }
            
        }

        public void ChangeSelectedState(bool isSelected)
        {
            SelectState = isSelected;
            //if (SubstProcState == EnumSubstProcState.NoState ||
            //    SubstProcState == EnumSubstProcState.NeedsProcessing ||
            //    SubstProcState == EnumSubstProcState.Skipped)
            {
                if (isSelected)
                {
                    SubstProcState = EnumSubstProcState.NeedsProcessing;
                }
                else
                {
                    SubstProcState = EnumSubstProcState.Skipped;
                }
            }
        }

        public void ChangeAlignedState(EnumWaferAlignState state)
        {
            AlignState = state;

            switch (AlignState)
            {
                case EnumWaferAlignState.Alignning:
                case EnumWaferAlignState.Aligned:
                     SubstProcState = EnumSubstProcState.InProcess;
                    break;
                case EnumWaferAlignState.AlignFailed:
                    SubstProcState = EnumSubstProcState.Aborted;
                    break;
                case EnumWaferAlignState.UnAligned:
                    SubstProcState = EnumSubstProcState.NeedsProcessing;
                    break;

            }
        }

        public void ChangeMeasureState(EnumWaferMeasureState state)
        {
            MeasureState = state;
            switch (state)
            {
                case EnumWaferMeasureState.Undetect:
                    SubstProcState = EnumSubstProcState.NeedsProcessing;
                    break;
                case EnumWaferMeasureState.Detecting:
                    SubstProcState = EnumSubstProcState.InProcess;
                    break;
                case EnumWaferMeasureState.Detected:
                    SubstProcState = EnumSubstProcState.Processed;
                    break;
                case EnumWaferMeasureState.DetectFailed:
                    SubstProcState = EnumSubstProcState.Stopped;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(state.ToString(), state, null);
            }
        }

        public void ChangeSubstrateTransportState(EnumSubstState substrateTransportState)
        {
            SubstState = substrateTransportState;           
        }
      

        public void ResetWaferState()
        {
            ChangeAlignedState(EnumWaferAlignState.UnAligned);
            ChangeMeasureState(EnumWaferMeasureState.Undetect);
            ChangeSelectedState(false);
        }
    }


    public class SubstrateLocation
    {
        /// <summary>
        /// Text equal to the Substrate Location ID
        /// </summary>
        public string ObjID { get; set; }

        /// <summary>
        /// Text = "SubstLoc"
        /// </summary>
        public string ObjType { get; private set; }

        /// <summary>
        /// Substrate ID related to this location
        /// </summary>
        public string SubstID { get; set; }

        /// <summary>
        /// Substrate location state
        /// </summary>
        public EnumSubstLocState SubstLocState { get; set; }

        public SubstrateLocation(string id)
        {
            this.ObjID = id;
            this.ObjType = "SubstLoc";
            this.SubstLocState = EnumSubstLocState.NoState;
        }

        private bool ChangeSubstLocState(EnumSubstLocState state)
        {
            if (state == SubstLocState || state == EnumSubstLocState.NoState)
            {
                return false;
            }
            this.SubstLocState = state;
            return true;
        }

    }

    public enum EnumSubstLocState
    {
        Unoccupied = 0,
        Occupied = 1,
        NoState = 2
    }


    public struct WaferPosition
    {
        public EnumEFEMStationID StationID;
        public int WaferNumber;

        public override string ToString()
        {
            return string.Format("{0},{1}", StationID, WaferNumber);
        }

        public override bool Equals(object obj)
        {
            WaferPosition objPosition = (WaferPosition)obj;
            if (StationID == objPosition.StationID && WaferNumber == objPosition.WaferNumber)
            {
                return true;
            }
            return false;
        }

        public bool Equals(EnumEFEMStationID componentID, int waferNumber)
        {
            if (StationID == componentID && WaferNumber == waferNumber)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return StationID.GetHashCode() * 10000 + WaferNumber;
        }
    }
   
}
