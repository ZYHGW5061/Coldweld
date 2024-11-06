using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalToolClsLib
{
    /// <summary>
    /// 日志类--用于日志的显示
    /// </summary>
    public class LogContent
    {
        public string Object { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public string Level { get; set; }
    }
}
