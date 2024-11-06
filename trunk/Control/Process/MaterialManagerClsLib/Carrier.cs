using GlobalDataDefineClsLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MaterialManagerClsLib
{
    public enum EnumCarrierIDStatus
    {
        NoState,
        /// <summary>
        /// 
        /// </summary>
        IDNotRead,
        /// <summary>
        /// 
        /// </summary>
        WaitingForHost,
        /// <summary>
        /// 
        /// </summary>
        IDVerificationOK,
        /// <summary>
        /// 
        /// </summary>
        IDVerificationFailed
    }

    public enum EnumCarrierAccessingStatus
    {
        None,
        /// <summary>
        /// Carrier没有正在使用
        /// </summary>
        NotAccessed,
        /// <summary>
        /// Carrier Door打开后
        /// </summary>
        InAccess,
        /// <summary>
        /// Carrier Door正常关闭
        /// </summary>
        CarrierComplete,
        /// <summary>
        /// Carrier 开门过程中出现了问题
        /// </summary>
        CarrierStopped
    }

    public enum EnumSlotMapStatus
    {
        None,
        /// <summary>
        /// 
        /// </summary>
        SlotMapNotRead,
        /// <summary>
        /// 
        /// </summary>
        WaitingForHost,
        /// <summary>
        /// 
        /// </summary>
        SlotMapVerificationOK,
        /// <summary>
        /// 
        /// </summary>
        SlotMapVerificationFailed
    }

    /// <summary>
    /// The reason for transition 14,SLOT MAP NOT READ to WAITING FOR HOST.
    /// </summary>
    public enum EnumReason
    {
        /// <summary>
        /// 需要验证
        /// </summary>
        VerificationNeeded = 0,
        /// <summary>
        /// 机台端验证
        /// </summary>
        VerificationByEquipmentUnsuccessful = 1,
        /// <summary>
        /// 设备端读取失败
        /// </summary>
        ReadFail = 2,
        /// <summary>
        /// 机台端和Host端不匹配
        /// </summary>
        ImproperSubstratePosition = 3
    }

    public struct CarrierContent
    {
        public string LotID { get; set; }
        public string WaferID { get; set; }
    }

    public enum EnumCarrierType
    {
        None,
        BindServiceCreate,
        CarrierNotificationServiceCreate,
        CarrierIDReadCreate,
        ProceedWithCarrierCreate
    }

    public enum EnumMappingResultType
    {
        Undefined = 0,
        Empty = 1,
        NotEmpty = 2,
        CorrectlyOccupied = 3,
        DoubleSlotted = 4,
        CrossSlotted = 5
    }
    public abstract class SEMIObject
    {
        public abstract string ObjectType { get; }

        public abstract string ObjectID { get; }
    }

    public class SEMIObjectTypes
    {
        public const string CarrierObj = "CarrierObj";





    }
    public class Carrier : SEMIObject
    {
        #region "E87属性"
        public override string ObjectType
        {
            get { return SEMIObjectTypes.CarrierObj; }
        }

        public override string ObjectID
        {
            get { return CarrierID; }
        }

        public string CarrierID { get; set; }

        public EnumCarrierIDStatus CarrierIDStatus { get; private set; }

        public byte Capacity { get; set; }

        public EnumCarrierAccessingStatus CarrierAccessingStatus { get; private set; }

        public List<CarrierContent> ContentMap { get; private set; }

        public string LocationID { get; set; }

        public List<EnumMappingResultType> SlotMap { get; set; }

        public EnumSlotMapStatus SlotMapStatus { get; private set; }

        /// <summary>
        /// 标记载具是几槽的型号
        /// </summary>
        public byte SubstrateCount { get; set; }

        /// <summary>
        ///     用途，例如"TEST"或"PRODUCT"
        /// </summary>
        public string Usage { get; set; }

        public EnumReason Reason { get; set; }
        #endregion

        #region Carrier属性
        public EnumCarrierType CarrierType { get; set; }

        public List<Lid> WaferList { get; set; }

        public Dictionary<int, Lid> WaferDic { get; set; }

        public List<SubstrateLocation> SubstrateLocList { get; set; }

        /// <summary>
        /// 新增--本地维护一个LOTID来记录当前Carrier的ID，2020,01,06
        /// </summary>
        public string LotID { get; set; }

        /// <summary>
        /// 新增--载具支持的晶圆尺寸，2020.03.26
        /// </summary>
        public EnumWaferSize WaferSize { get; set; }

        /// <summary>
        /// 新增--载具里的晶圆信息，2020.4.11，例如：1,2,3,4,5,6,7,8,9,
        /// </summary>
        public string SlotSummary 
        {
            get { return string.Join(",", WaferDic.Keys); }
        }
        #endregion

        #region "事件"
        public Action<Carrier, EnumCarrierIDStatus, EnumCarrierIDStatus> CarrierIDStatusChanged { get; set; }

        public Action<Carrier, EnumSlotMapStatus, EnumSlotMapStatus> CarrierSlotMapStatusChanged { get; set; }

        public Action<Carrier, EnumCarrierAccessingStatus, EnumCarrierAccessingStatus> CarrierAccessingStatusChanged { get; set; }
        #endregion

        #region "状态机"

        private ACarrierIDState _currentCarrierIDState
        {
            get { return _carrierIDStateDic[CarrierIDStatus]; }
        }

        private ASlotMapState _currentSlotMapState
        {
            get { return _slotMapStateDic[SlotMapStatus]; }
        }

        private AAccessingState _currentAccessingState
        {
            get { return _carrierAccessingStateDic[CarrierAccessingStatus]; }
        }

        private readonly ConcurrentDictionary<EnumCarrierIDStatus, ACarrierIDState> _carrierIDStateDic = new ConcurrentDictionary<EnumCarrierIDStatus, ACarrierIDState>();

        private readonly ConcurrentDictionary<EnumSlotMapStatus, ASlotMapState> _slotMapStateDic = new ConcurrentDictionary<EnumSlotMapStatus, ASlotMapState>();

        private readonly ConcurrentDictionary<EnumCarrierAccessingStatus, AAccessingState> _carrierAccessingStateDic = new ConcurrentDictionary<EnumCarrierAccessingStatus, AAccessingState>();

        #endregion

        public Carrier(int substrateCount = 25)
        {
            ContentMap = new List<CarrierContent>();
            SlotMap = new List<EnumMappingResultType>();
            SubstrateLocList = new List<SubstrateLocation>();
            SubstrateCount = (byte)substrateCount;
            for (int i = 0; i < SubstrateCount; i++)
            {
                SlotMap.Add(EnumMappingResultType.Undefined);
                SubstrateLocList.Add(new SubstrateLocation(string.Format("{0}", i+1)));
            }
            CarrierID = "UNKNOWN";

            _carrierIDStateDic.TryAdd(EnumCarrierIDStatus.NoState, new IDNoState(this));
            _carrierIDStateDic.TryAdd(EnumCarrierIDStatus.IDNotRead, new IDNotRead(this));
            _carrierIDStateDic.TryAdd(EnumCarrierIDStatus.WaitingForHost, new WaitingForHost(this));
            _carrierIDStateDic.TryAdd(EnumCarrierIDStatus.IDVerificationOK, new IDVerificationOK(this));
            _carrierIDStateDic.TryAdd(EnumCarrierIDStatus.IDVerificationFailed, new IDVerificationFailed(this));
            CarrierIDStatus = EnumCarrierIDStatus.NoState;

            _slotMapStateDic.TryAdd(EnumSlotMapStatus.SlotMapNotRead, new SlotMapNotRead(this));
            _slotMapStateDic.TryAdd(EnumSlotMapStatus.WaitingForHost, new SlotMapWaitingForHost(this));
            _slotMapStateDic.TryAdd(EnumSlotMapStatus.SlotMapVerificationOK, new SlotMapVerificationOK(this));
            _slotMapStateDic.TryAdd(EnumSlotMapStatus.SlotMapVerificationFailed, new SlotMapVerificationFail(this));
            //12转换
            SlotMapStatus = EnumSlotMapStatus.SlotMapNotRead;

            _carrierAccessingStateDic.TryAdd(EnumCarrierAccessingStatus.NotAccessed, new NotAccessed(this));
            _carrierAccessingStateDic.TryAdd(EnumCarrierAccessingStatus.InAccess, new InAccess(this));
            _carrierAccessingStateDic.TryAdd(EnumCarrierAccessingStatus.CarrierComplete, new CarrierComplete(this));
            _carrierAccessingStateDic.TryAdd(EnumCarrierAccessingStatus.CarrierStopped, new CarrierStopped(this));
            CarrierAccessingStatus = EnumCarrierAccessingStatus.NotAccessed;

            Usage = "";
            Capacity = 25;
            WaferList = new List<Lid>();
            WaferDic = new Dictionary<int, Lid>();
        }

        public void ResetMapStatus()
        {
            SlotMapStatus = EnumSlotMapStatus.SlotMapNotRead;
            if (ContentMap != null)
            {
                ContentMap.Clear();
            }

            if (SlotMap != null)
            {
                SlotMap.Clear();
            }
        }

        public void SetCarrierID(string carrierID)
        {
            CarrierID = carrierID;
        }

        public void SetCarrierIDStatus(EnumCarrierIDStatus status)
        {
            var oldState = CarrierIDStatus;
            CarrierIDStatus = status;
            if (CarrierIDStatusChanged != null)
            {
                CarrierIDStatusChanged(this, oldState, CarrierIDStatus);
            }
        }

        public void SetSlotMapStatue(EnumSlotMapStatus status)
        {
            var oldState = SlotMapStatus;
            SlotMapStatus = status;
            if (CarrierSlotMapStatusChanged != null)
            {
                CarrierSlotMapStatusChanged(this, oldState, SlotMapStatus);
            }
        }

        public void SetAccessingStatus(EnumCarrierAccessingStatus status)
        {
            var oldState = CarrierAccessingStatus;
            CarrierAccessingStatus = status;
            if (CarrierAccessingStatusChanged != null)
            {
                CarrierAccessingStatusChanged(this, oldState, CarrierAccessingStatus);
            }
        }

        /// <summary>
        /// 更新Wafer类型
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="waferType"></param>
        public void ChangeWaferType(int slotId, EnumWaferType waferType)
        {
            if (WaferDic.ContainsKey(slotId))
            {
                WaferDic[slotId].ChangeWaferType(waferType);
            }
        }

        /// <summary>
        /// 更新Wafer 选择类型
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="waferType"></param>
        public void ChangeWaferSelectState(int slotId, bool selected)
        {
            if (WaferDic.ContainsKey(slotId))
            {
                WaferDic[slotId].ChangeSelectedState(selected);
            }
        }

        /// <summary>
        /// 更新Wafer传输状态
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="substState"></param>
        public void ChangeSubstrateTransportState(int slotId, EnumSubstState substState)
        {
            if (WaferDic.ContainsKey(slotId))
            {
                WaferDic[slotId].ChangeSubstrateTransportState(substState);
            }
        }

        /// <summary>
        /// 更新Wafer PreAlign状态
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="substState"></param>
        public void ChangeAlignedState(int slotId, EnumWaferAlignState alignState)
        {
            if (WaferDic.ContainsKey(slotId))
            {
                WaferDic[slotId].ChangeAlignedState(alignState);
            }
        }

        /// <summary>
        /// 更新Wafer MeasureState
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="substState"></param>
        public void ChangeMeasureState(int slotId, EnumWaferMeasureState waferMeasureState)
        {
            if (WaferDic.ContainsKey(slotId))
            {
                WaferDic[slotId].ChangeMeasureState(waferMeasureState);
            }
        }

        /// <summary>
        /// 更新Wafer的历史记录外键ID，用于关联各检测端的检测记录
        /// </summary>
        /// <param name="slotId"></param>
        /// <param name="substState"></param>
        public void ChangeWaferInternalInspectionID(int slotId, string inspectionID)
        {
            if (WaferDic.ContainsKey(slotId))
            {
                WaferDic[slotId].InspectionID = inspectionID;
            }
        }

        #region "改变CarrierID状态的方法"

        /// <summary>
        /// 2转换
        /// </summary>
        public void ReceiveBinService()
        {
            _currentCarrierIDState.ReceiveBinService();
        }

        /// <summary>
        /// 2转换
        /// </summary>
        public void ReceiveCarrierNotificationService()
        {
            _currentCarrierIDState.ReceiveCarrierNotificationService();
        }

        /// <summary>
        /// 3转换
        /// </summary>
        public void ReadIDSuccessfullyAtFirstTime()
        {
            _currentCarrierIDState.ReadIDSuccessfullyAtFirstTime();
        }

        /// <summary>
        /// 4转换
        /// </summary>
        public void ProceedWithCarrierServiceCreateCarrier()
        {
            _currentCarrierIDState.ProceedWithCarrierServiceCreateCarrier();
        }

        /// <summary>
        /// 5转换
        /// </summary>
        public void CancelCarrierServiceCreateCarrier()
        {
            _currentCarrierIDState.CancelCarrierServiceCreateCarrier();
        }

        public void IDReadOkAndEqpVerifiedOK(string carrierID)
        {
            _currentCarrierIDState.IDReadOkAndEqpVerifiedOK(carrierID);
        }

        public void IDReadFailAndReceiveProceedWithCarrierService(string carrierID)
        {
            _currentCarrierIDState.IDReadFailAndReceiveProceedWithCarrierService(carrierID);
        }

        public void IDReadFailed()
        {
            _currentCarrierIDState.IDReadFailed();
        }

        public void BypassReadyIDAndCanNotReadID()
        {
            _currentCarrierIDState.BypassReadyIDAndCanNotReadID();
        }

        public void DontBypassReadyIDAndCanNotReadID()
        {
            _currentCarrierIDState.DontBypassReadyIDAndCanNotReadID();
        }

        public void ReceivedProcessWithCarrierVerifyID()
        {
            _currentCarrierIDState.ReceivedProcessWithCarrier();
        }

        public void ReceivedCancelCarrier()
        {
            _currentCarrierIDState.ReceivedCancelCarrier();
        }

        #endregion

        #region "改变SlopMap状态的方法"
        /// <summary>
        /// 13转换，目前不会发生
        /// </summary>
        public void SlotMapReadAndVerifiedOK()
        {
            _currentSlotMapState.SlotMapReadAndVerifiedOK();
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public void SlotMapReadOKAndWaitingForHostVerification()
        {
            _currentSlotMapState.SlotMapReadOKAndWaitingForHostVerification();
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public void SlotMapReadOKButEquipmentVerificationFail()
        {
            _currentSlotMapState.EquipmentVerificationFail();
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public void SlotMapCanNotRead()
        {
            _currentSlotMapState.SlotMapCanNotRead();
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public void SlotMapReadFail()
        {
            _currentSlotMapState.SlotMapReadFail();
        }

        /// <summary>
        /// 15转换
        /// </summary>
        public void ReceivedProceedWithCarrierVerifySlotMap()
        {
            _currentSlotMapState.ReceivedProceedWithCarrierService();
        }

        /// <summary>
        /// 16转换
        /// </summary>
        public void ReceivedCancelCarrierServiceCancelSlotMap()
        {
            _currentSlotMapState.ReceivedCancelCarrierService();
        }

        #endregion

        #region "改变Accessing状态的方法"
        public void StartAccessingCarrier()
        {
            _currentAccessingState.StartAccessingCarrier();
        }

        public void FinishAccessingCarrier()
        {
            _currentAccessingState.FinishAccessingCarrier();
        }

        public void FinishAccessingAbnormally()
        {
            _currentAccessingState.FinishAccessingAbnormally();
        }
        #endregion
    }




    #region "Carrier ID 状态机"

    internal abstract class ACarrierIDState
    {
        public abstract EnumCarrierIDStatus CarrierIdStatus { get; }

        protected Carrier _carrierEntity;

        protected ACarrierIDState(Carrier currentCarrier)
        {
            _carrierEntity = currentCarrier;
        }

        /// <summary>
        /// 2转换
        /// </summary>
        public virtual void ReceiveBinService()
        {
        }

        /// <summary>
        /// 2转换
        /// </summary>
        public virtual void ReceiveCarrierNotificationService()
        {
        }

        /// <summary>
        /// 3转换
        /// </summary>
        public virtual void ReadIDSuccessfullyAtFirstTime()
        {
        }

        /// <summary>
        /// 4转换
        /// </summary>
        public virtual void ProceedWithCarrierServiceCreateCarrier()
        {
        }

        /// <summary>
        /// 5转换
        /// </summary>
        public virtual void CancelCarrierServiceCreateCarrier()
        {
        }


        public virtual void IDReadOkAndEqpVerifiedOK(string carrierID)
        {
        }

        public virtual void IDReadFailAndReceiveProceedWithCarrierService(string carrierID)
        {
        }

        public virtual void IDReadFailed()
        {
        }

        public virtual void BypassReadyIDAndCanNotReadID()
        {
        }

        public virtual void DontBypassReadyIDAndCanNotReadID()
        {
        }

        public virtual void ReceivedProcessWithCarrier()
        {
        }

        public virtual void ReceivedCancelCarrier()
        {
        }
    }

    internal class IDNoState : ACarrierIDState
    {
        public override EnumCarrierIDStatus CarrierIdStatus
        {
            get { return EnumCarrierIDStatus.NoState; }
        }
        public IDNoState(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 2转换
        /// </summary>
        public override void ReceiveBinService()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDNotRead);
        }

        /// <summary>
        /// 2转换
        /// </summary>
        public override void ReceiveCarrierNotificationService()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDNotRead);
        }

        /// <summary>
        /// 3转换
        /// </summary>
        public override void ReadIDSuccessfullyAtFirstTime()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.WaitingForHost);
        }

        /// <summary>
        /// 4转换
        /// </summary>
        public override void ProceedWithCarrierServiceCreateCarrier()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationOK);
        }

        /// <summary>
        /// 5转换
        /// </summary>
        public override void CancelCarrierServiceCreateCarrier()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationFailed);
        }
    }

    internal class IDNotRead : ACarrierIDState
    {
        public override EnumCarrierIDStatus CarrierIdStatus
        {
            get { return EnumCarrierIDStatus.IDNotRead; }
        }

        public IDNotRead(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 6
        /// </summary>
        /// <param name="carrierID"></param>
        public override void IDReadOkAndEqpVerifiedOK(string carrierID)
        {
            _carrierEntity.CarrierID = carrierID;
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationOK);
        }

        /// <summary>
        /// 6
        /// </summary>
        /// <param name="carrierID"></param>
        public override void IDReadFailAndReceiveProceedWithCarrierService(string carrierID)
        {
            _carrierEntity.CarrierID = carrierID;
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationOK);
        }

        /// <summary>
        /// 7
        /// </summary>
        public override void IDReadFailed()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.WaitingForHost);
        }

        /// <summary>
        /// 11
        /// </summary>
        public override void BypassReadyIDAndCanNotReadID()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationOK);
        }

        /// <summary>
        /// 10
        /// </summary>
        public override void DontBypassReadyIDAndCanNotReadID()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.WaitingForHost);
        }       
    }

    internal class WaitingForHost : ACarrierIDState
    {
        public override EnumCarrierIDStatus CarrierIdStatus
        {
            get { return EnumCarrierIDStatus.WaitingForHost; }
        }

        public WaitingForHost(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 8
        /// </summary>
        public override void ReceivedProcessWithCarrier()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationOK);
        }

        public override void ReceivedCancelCarrier()
        {
            _carrierEntity.SetCarrierIDStatus(EnumCarrierIDStatus.IDVerificationFailed);
        }
    }

    internal class IDVerificationOK : ACarrierIDState
    {
        public override EnumCarrierIDStatus CarrierIdStatus
        {
            get { return EnumCarrierIDStatus.IDVerificationOK; }
        }

        public IDVerificationOK(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }       
    }

    internal class IDVerificationFailed : ACarrierIDState
    {
        public override EnumCarrierIDStatus CarrierIdStatus
        {
            get { return EnumCarrierIDStatus.IDVerificationFailed; }
        }

        public IDVerificationFailed(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }
    }

    #endregion


    #region "Carrier Slot Map 状态机"

    internal abstract class ASlotMapState
    {
        public abstract EnumSlotMapStatus CarrierSlotMapStatus { get; }

        protected Carrier _carrierEntity;

        protected ASlotMapState(Carrier currentCarrier)
        {
            _carrierEntity = currentCarrier;
        }

        public virtual void SlotMapReadAndVerifiedOK()
        {
        }

        public virtual void SlotMapReadOKAndWaitingForHostVerification()
        {
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public virtual void EquipmentVerificationFail()
        {
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public virtual void SlotMapCanNotRead()
        {
        }

        /// <summary>
        /// 14转换
        /// </summary>
        public virtual void SlotMapReadFail()
        {
        }

        public virtual void ReceivedProceedWithCarrierService()
        {
        }

        public virtual void ReceivedCancelCarrierService()
        {
        }
    }

    internal class SlotMapNotRead : ASlotMapState
    {

        public override EnumSlotMapStatus CarrierSlotMapStatus
        {
            get { return EnumSlotMapStatus.SlotMapNotRead; }
        }


        public SlotMapNotRead(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 13
        /// </summary>
        public override void SlotMapReadAndVerifiedOK()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.SlotMapVerificationOK);
        }

        /// <summary>
        /// 14
        /// </summary>
        public override void SlotMapReadOKAndWaitingForHostVerification()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.WaitingForHost);
        }

        /// <summary>
        /// 14
        /// </summary>
        public override void EquipmentVerificationFail()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.WaitingForHost);
        }

        /// <summary>
        /// 14
        /// </summary>
        public override void SlotMapCanNotRead()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.WaitingForHost);
        }

        /// <summary>
        /// 14
        /// </summary>
        public override void SlotMapReadFail()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.WaitingForHost);
        }

    }


    internal class SlotMapWaitingForHost : ASlotMapState
    {

        public override EnumSlotMapStatus CarrierSlotMapStatus
        {
            get { return EnumSlotMapStatus.WaitingForHost; }
        }


        public SlotMapWaitingForHost(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 15
        /// </summary>
        public override void ReceivedProceedWithCarrierService()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.SlotMapVerificationOK);
        }

        /// <summary>
        /// 16
        /// </summary>
        public override void ReceivedCancelCarrierService()
        {
            _carrierEntity.SetSlotMapStatue(EnumSlotMapStatus.SlotMapVerificationFailed);
        }
    }

    internal class SlotMapVerificationOK : ASlotMapState
    {

        public override EnumSlotMapStatus CarrierSlotMapStatus
        {
            get { return EnumSlotMapStatus.SlotMapVerificationOK; }
        }

        public SlotMapVerificationOK(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }       

    }

    internal class SlotMapVerificationFail : ASlotMapState
    {

        public override EnumSlotMapStatus CarrierSlotMapStatus
        {
            get { return EnumSlotMapStatus.SlotMapVerificationFailed; }
        }


        public SlotMapVerificationFail(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

    }

    #endregion

    #region "Carrier Accessing 状态机"

    internal abstract class AAccessingState
    {
        public abstract EnumCarrierAccessingStatus AccessingStatus { get; }

        protected Carrier _carrierEntity;

        protected AAccessingState(Carrier currentCarrier)
        {
            _carrierEntity = currentCarrier;
        }

        public virtual void StartAccessingCarrier()
        {           
        }

        public virtual void FinishAccessingCarrier()
        {

        }

        public virtual void FinishAccessingAbnormally()
        {

        }

    }

    internal class NotAccessed : AAccessingState
    {

        public override EnumCarrierAccessingStatus AccessingStatus
        {
            get { return EnumCarrierAccessingStatus.NotAccessed; }
        }


        public NotAccessed(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 18
        /// </summary>
        public override void StartAccessingCarrier()
        {
            _carrierEntity.SetAccessingStatus(EnumCarrierAccessingStatus.InAccess);
        }

    }

    internal class InAccess : AAccessingState
    {

        public override EnumCarrierAccessingStatus AccessingStatus
        {
            get { return EnumCarrierAccessingStatus.InAccess; }
        }


        public InAccess(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        /// <summary>
        /// 19
        /// </summary>
        public override void FinishAccessingCarrier()
        {
            _carrierEntity.SetAccessingStatus(EnumCarrierAccessingStatus.CarrierComplete);
        }

        public override void FinishAccessingAbnormally()
        {
            _carrierEntity.SetAccessingStatus(EnumCarrierAccessingStatus.CarrierStopped);
        }
    }

    internal class CarrierComplete : AAccessingState
    {

        public override EnumCarrierAccessingStatus AccessingStatus
        {
            get { return EnumCarrierAccessingStatus.CarrierComplete; }
        }


        public CarrierComplete(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        public override void StartAccessingCarrier()
        {
            _carrierEntity.SetAccessingStatus(EnumCarrierAccessingStatus.InAccess);
        }

    }

    internal class CarrierStopped : AAccessingState
    {

        public override EnumCarrierAccessingStatus AccessingStatus
        {
            get { return EnumCarrierAccessingStatus.CarrierStopped; }
        }


        public CarrierStopped(Carrier currentCarrier)
            : base(currentCarrier)
        {
        }

        public override void StartAccessingCarrier()
        {
            _carrierEntity.SetAccessingStatus(EnumCarrierAccessingStatus.InAccess);
        }
    }



    #endregion

}