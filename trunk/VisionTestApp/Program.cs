using ConfigurationClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WestDragon.Framework.LoggerManagerClsLib;

namespace VisionTestApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitLogSystem();
            var systemConfig = SystemConfiguration.Instance;
            var hardware = HardwareConfiguration.Instance;

            Application.Run(new Form1());


           

        }

        /// <summary>
        /// 初始化日志系统
        /// </summary>
        private static void InitLogSystem()
        {
            try
            {
                //全局日志记录
                LoggerManager.GetHandler().AddTxtLogger("SystemGlobalAppender", "SystemGlobalLogger");
                LoggerManager.GetHandler().AddTxtLogger("HardwarePLCAppender", "HardwarePLCLogger");
                LoggerManager.GetHandler().AddTxtLogger("KeyActionAppender", "KeyActionLogger");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
