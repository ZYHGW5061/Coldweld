using ConfigurationClsLib;
using HardwareManagerClsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace BondTerminal
{
    static class Program
    {
        /// <summary>
        /// 系统全局日志记录器
        /// </summary>
        public static IBaseLogger _systemGlobalLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger(GlobalParamSetting.SYSTEM_DEBUG_LOGGER_ID); }
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process instance = GetOnlyProcess();
            if (instance != null)
            {
                //显示正常状态
                CommonProcess.ShowWindowAsync(instance.MainWindowHandle, 3);
                //显示在前面
                CommonProcess.SetForegroundWindow(instance.MainWindowHandle);
                return;
            }
            Application.ThreadException += Application_ThreadException; //未捕获线程异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; //未捕获异常捕获
            InitLogSystem();
            var systemConfig = SystemConfiguration.Instance;
            var hardware = HardwareConfiguration.Instance;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Stopwatch timeCounter = new Stopwatch();
            timeCounter.Reset();
            timeCounter.Start();
            _systemGlobalLogger.AddInfoContent("===============Program start===================");
            _systemGlobalLogger.AddInfoContent($"Program startPath:{Application.ExecutablePath}");
            Application.Run(new MainForm());
            HardwareManager.Instance.DisconnectHardwares();
            _systemGlobalLogger.AddInfoContent($"==============Program exits normally, runs for {(timeCounter.ElapsedMilliseconds / 1000 / 60 / 60d).ToString("0.00")} Hours.================");
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
                LoggerManager.GetHandler().AddTxtLogger("KeyActionAppender", "KeyActionLogger");
            }
            catch (Exception ex)
            {

            }
        }
        //多线程异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _systemGlobalLogger.AddErrorContent("Thread Exception catched while System is running.", e.Exception);
        }

        //UI线程异常
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _systemGlobalLogger.AddErrorContent("UI Exception catched while System is running.", (Exception)e.ExceptionObject);
        }
        /// <summary>
        /// 获取当前程序的活动进程，没有活动进程返回NULL
        /// </summary>
        /// <returns></returns>
        public static Process GetOnlyProcess()
        {
            //获取当期活动进程
            Process currentProcess = Process.GetCurrentProcess();
            //获取当期活动进程相关联的所有进程资源
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程
                if (process.Id != currentProcess.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (Assembly.GetExecutingAssembly().Location.Replace(@"/", @"\") == currentProcess.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }
    }
}
