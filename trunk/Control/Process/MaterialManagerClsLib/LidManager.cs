using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.UtilityHelper;

namespace MaterialManagerClsLib
{

    public class LidManager
    {
        #region 单例
        private static volatile LidManager _instance;
        private static readonly  object _lockObj = new object();
        public static LidManager Instance
        {
            get
            {
                if(_instance==null)
                {
                    lock(_lockObj)
                    {
                        if(_instance==null)
                        {
                            _instance = new LidManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private LidManager()
        {

        }
        #endregion

        #region 事件
        /// <summary>
        /// E90 SubstProcState事件
        /// </summary>
        public Action<Lid, EnumSubstProcState, EnumSubstProcState> WaferSubstProcStateChangeEvt { get; set; }

        /// <summary>
        /// E90 SubstTransportState事件
        /// </summary>
        public Action<Lid, EnumSubstState, EnumSubstState> WaferSubstStateChangeEvt { get; set; }
        #endregion

        public void InternalBindE90StateEvent(Lid wafer)
        {
            wafer.SubstProcStateChangeEvt = OnWaferSubstProcStateChangeEvt;
            wafer.SubstStateChangeEvt = OnWaferSubstStateChangeEvt;
        }


        #region Wafer事件转发

        private void OnWaferSubstProcStateChangeEvt(Lid wafer, EnumSubstProcState oldState, EnumSubstProcState newState)
        {
            if (WaferSubstProcStateChangeEvt != null)
            {
                WaferSubstProcStateChangeEvt.InvokeAsync(wafer, oldState, newState);
            }

        }

        private void OnWaferSubstStateChangeEvt(Lid wafer, EnumSubstState oldState, EnumSubstState newState)
        {
            if (WaferSubstStateChangeEvt != null)
            {
                WaferSubstStateChangeEvt.InvokeAsync(wafer, oldState, newState);
            }
        }
        #endregion
    }
}
