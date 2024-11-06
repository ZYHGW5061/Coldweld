using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerControllerManagerClsLib
{
    public enum PowerAdd
    {
        /// <summary>
        /// 第1段温度
        /// </summary>
        T1 = 0,
        /// <summary>
        /// 第2段温度
        /// </summary>
        T2,
        /// <summary>
        /// 第3段温度
        /// </summary>
        T3,
        /// <summary>
        /// 第4段温度
        /// </summary>
        T4,
        /// <summary>
        /// 第5段温度
        /// </summary>
        T5,
        /// <summary>
        /// 预压时间
        /// </summary>
        t1,
        /// <summary>
        /// 第1段加热时间
        /// </summary>
        t2,
        /// <summary>
        /// 第2段加热时间
        /// </summary>
        t3,
        /// <summary>
        /// 第3段加热时间
        /// </summary>
        t4,
        /// <summary>
        /// 第4段加热时间
        /// </summary>
        t5,
        /// <summary>
        /// 保持时间
        /// </summary>
        t6,

        //("H1")]
        H1,
        //("H2")]
        H2,
        //("H3")]
        H3,
        //("H4")]
        H4,

        //("L1")]
        L1,
        //("L2")]
        L2,
        //("L3")]
        L3,
        //("L4")]
        L4,



        //("G1")]
        G1,
        //("G2")]
        G2,
        //("G3")]
        G3,
        //("G4")]
        G4,



        //("t0")]
        t0,

        //("T0")]
        T0,



        //("MH")]
        MH,
        //("ML")]
        ML,

        //("HD")]
        HD,

        //("CNTLLimit")]
        CNTLLimit,

        //("MP")]
        MP,

        //("TC")]
        TC,

        //("IOUT")]
        IOUT,

        //("OPMD")]
        OPMD,

        //("DM")]
        DM,

        /// <summary>
        /// 参数组
        /// </summary>
        GP,

        //("DN")]
        DN,

        //("ERRC")]
        ERRC,


        //("CNTH")]
        CNTH,
        //("CNTL")]
        CNTL,

        //("TM1")]
        TM1,
        //("TM2")]
        TM2,
        //("TM3")]
        TM3,
        //("TM4")]
        TM4,
        //("TMC")]
        TMC,
        //("DataTime")]
        DataTime,
        //("tcys")]
        tcys,
        //("IM1")]
        IM1,
        //("IM2")]
        IM2,
        //("IM3")]
        IM3,
        //("IM4")]
        IM4,
        //("UM1")]
        UM1,
        //("UM2")]
        UM2,
        //("UM3")]
        UM3,
        //("UM4")]
        UM4,

        //("TData1")]
        TData1,
    }


    public interface IPowerController
    {
        /// <summary>
        /// 链接状态
        /// </summary>
        bool IsConnect { get; }
        /// <summary>
        /// 建立连接
        /// </summary>
        void Connect();
        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();
        /// <summary>
        /// 写入参数
        /// </summary>
        /// <param name="Add"></param>
        /// <param name="value"></param>
        void Write(PowerAdd Add, int value);
        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="Add"></param>
        /// <returns></returns>
        int Read(PowerAdd Add);

        /// <summary>
        /// 读取所有参数
        /// </summary>
        /// <returns></returns>
        GlobalDataDefineClsLib.PowerParam ReadAll();

        /// <summary>
        /// 写入所有参数
        /// </summary>
        /// <param name="param"></param>
        void WriteAll(GlobalDataDefineClsLib.PowerParam param);
    }
}
