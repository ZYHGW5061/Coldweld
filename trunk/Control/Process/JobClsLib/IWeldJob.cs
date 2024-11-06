using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using RecipeClsLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobClsLib
{
    /// <summary>
    /// ProcessJob Interface.
    /// </summary>
    public interface IWeldJob
    {
        /// <summary>
        /// 原始数据存储路径
        /// </summary>
        string RawDataSavingPath { get; set; }

        /// <summary>
        /// 运行job任务.
        /// </summary>
        /// <param name="recipe"></param>
        void RunProcessJob(BondRecipe recipe, int index = 0, bool isFirstWafer = false);

        /// <summary>
        /// 停止任务
        /// </summary>
        void Pause();

        /// <summary>
        /// 继续任务
        /// </summary>
        void Resume();

        /// <summary>
        /// 终止任务
        /// </summary>
        void Abort();

        /// <summary>
        /// 获取分析结果
        /// </summary>
        //MeasureResult GetFinalResult();
    }
}
