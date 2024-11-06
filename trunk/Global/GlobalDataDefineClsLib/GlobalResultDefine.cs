using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalDataDefineClsLib
{
    /// <summary>
    /// 返回码 与 提示 定义
    /// 
    /// 10000 操作成功
    /// 
    /// ###################### 操作错误代码 1xxxx ######################
    /// 
    /// ## 轴操作相关错误代码 11xxx ##
    /// 11001 轴初始化错误
    /// 11002 轴获取移动位置错误
    /// 
    /// ## 相机相关错误代码 12xxx ##
    /// 12001 相机初始化错误
    /// 
    /// ###################### 提醒警告代码 2xxxx ######################
    /// 
    /// ## 操作提示 21xxx ##
    /// 21001 请放置吸嘴
    /// 
    /// </summary>
    public class GWResult
    {
        public int ResultCode { get; set; }
        public string ResultMsg { get; set; }

        public GWResult(int code, string msg)
        {
            ResultCode = code;
            ResultMsg = msg;
        }
    }

    /// <summary>
    /// 返回码常量定义
    /// </summary>
    public class GlobalGWResultDefine
    {
        public static readonly GWResult RET_SUCCESS = new GWResult(10000, "操作成功");
    }

    /*public GWResult testMethod()
    {
        return GlobalGWResultDefine.RET_SUCCESS;
    }*/
}
