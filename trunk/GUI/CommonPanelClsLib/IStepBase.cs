using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPanelClsLib
{
    /// <summary>
    /// 定义ProcessRecipe的内容接口
    /// </summary>
    public interface IStepBase
    {
        /// <summary>
        /// 上一步的页面Id
        /// </summary>
        Type PrePageView
        {
            get;
            set;
        }
        /// <summary>
        /// 上一步页面描述
        /// </summary>
        string PrePageViewDescription
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页面描述
        /// </summary>
        string NextPageViewDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 下一步的页面Id
        /// </summary>
        Type NextPageView
        {
            get;
            set;
        }

        ///// <summary>
        ///// 当前步骤所属的root step
        ///// </summary>
        //EnumRecipeRootStep CurRecipeStepOwner { get; set; }

        /// <summary>
        /// 通知单步Recipe定义完成
        /// </summary>
        Action NotifySingleStepDefineFinished
        {
            get;
            set;
        }

        ///// <summary>
        ///// 加载Recipe内容
        ///// </summary>
        ///// <param name="recipe"></param>
        //void LoadEditedRecipe(BondRecipe recipe);

        /// <summary>
        /// 确认定义Recipe是否完成
        /// </summary>
        /// <param name="finished"></param>
        void VertifyAndNotifySingleStepDefineFinished(out bool finished, out EnumRecipeStep CurrentStep);

        /// <summary>
        /// 是否允许切换到前一页面
        /// </summary>
        /// <param name="enableGoto"></param>
        void GotoProviousStepPage(out bool enableGoto);
    }
}
