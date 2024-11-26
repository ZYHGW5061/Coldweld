using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.LoggerManagerClsLib;
using WestDragon.Framework.UserOperationLogClsLib;

namespace GlobalToolClsLib
{
    public class LogRecorder
    {

        /// <summary>
        /// 用于记录当前系统最近发生的1000条重要动作或流程的操作记录
        /// </summary>
        public static List<LogContent> LastSystemLogList = new List<LogContent>(1001);

        /// <summary>
        /// ModuleLog发生时的回调
        /// </summary>
        public static Action<EnumLogContentType, string, string> ModuleLogAct { get; set; }

        /// <summary>
        /// ModuleLog发生时的回调
        /// </summary>
        public static Action<EnumLogContentType, string> GlobalLogAct { get; set; }

        /// <summary>
        /// UserOperationLog发生时的回调
        /// </summary>
        public static Action<EnumLogContentType, string> UserOperationLogAct { get; set; }

        /// <summary>
        /// RemotingLogView发生时的回调
        /// </summary>
        public static Action<EnumLogContentType, string> RemotingLogAct { get; set; }
        
        /// <summary>
        /// 系统系统调试日志
        /// </summary>
        private static IBaseLogger _systemLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger("SystemGlobalLogger"); }
        }

        /// <summary>
        /// 系统远程交互事件日志
        /// </summary>
        private static IBaseLogger _keyActionLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger("KeyActionLogger"); }
        }

        /// <summary>
        /// 系统远程交互事件日志
        /// </summary>
        private static IBaseLogger _remoteLogger
        {
            get { return LoggerManager.GetHandler().GetFileLogger("RemotingLogger"); }
        }

        /// <summary>
        /// 用户操作日志
        /// </summary>
        private static UserOperationLogger _userOperationLogger
        {
            get { return UserOperationLogger.GetHandler(); }
        }
        private static IBaseLogger _data1Logger
        {
            get { return LoggerManager.GetHandler().GetFileLogger("HardwareData1Logger"); }
        }
        private static IBaseLogger _data2Logger
        {
            get { return LoggerManager.GetHandler().GetFileLogger("HardwareData2Logger"); }
        }
        private static IBaseLogger _data3Logger
        {
            get { return LoggerManager.GetHandler().GetFileLogger("HardwareData3Logger"); }
        }
        /// <summary>
        /// 记录全局日志
        /// </summary>
        /// <param name="sender">窗体控件名称</param>
        /// <param name="caption">日志标题</param>
        public static void RecordLog(EnumLogContentType logType, string log, Exception ex = null)
        {
            if (_systemLogger == null) return;

            switch(logType)
            {
                case EnumLogContentType.Debug:
                    _systemLogger.AddDebugContent(log);
                    break;

                case EnumLogContentType.Error:
                    _systemLogger.AddErrorContent(log, ex);
                    break;

                case EnumLogContentType.Info:
                    _systemLogger.AddInfoContent(log);
                    break;
                case EnumLogContentType.Warn:
                    _systemLogger.AddWarnContent(log, ex);
                    break;
            }

            if (LastSystemLogList.Count > 1000)
            {
                LastSystemLogList.RemoveAt(LastSystemLogList.Count - 1);
            }

            LastSystemLogList.Insert(0, new LogContent() { Object = "System", Message = log, Time = DateTime.Now, Level = logType.ToString() });

            if (GlobalLogAct != null)
            {
                GlobalLogAct(logType, log + ex ?? "");
            }
        }

         /// <summary>
        /// 记录Module日志
        /// </summary>
        /// <param name="sender">窗体控件名称</param>
        /// <param name="caption">日志标题</param>
        public static void RecordModuleLog(EnumLogContentType logType, string Module, string log, Exception ex = null)
        {
            var logcontent = string.Format("{0}, {1}", log, ex == null ? "" : ex.ToString());
            switch (logType)
            {
                case EnumLogContentType.Debug:
                    _keyActionLogger.AddDebugContent(logcontent);
                    break;

                case EnumLogContentType.Error:
                    _keyActionLogger.AddErrorContent(logcontent, ex);
                    break;

                case EnumLogContentType.Info:
                    _keyActionLogger.AddInfoContent(logcontent);
                    break;
                case EnumLogContentType.Warn:
                    _keyActionLogger.AddWarnContent(logcontent, ex);
                    break;
            }     

            if(ModuleLogAct!=null)
            {
                ModuleLogAct(logType, Module, log);
            }
        }
        public static void RecordData1Log(EnumLogContentType logType, string log, Exception ex = null)
        {
            if (_data1Logger == null) return;
            var logcontent = string.Format("{0}, {1}", log, ex == null ? "" : ex.ToString());
            switch (logType)
            {
                case EnumLogContentType.Debug:
                    _data1Logger.AddDebugContent(logcontent);
                    break;

                case EnumLogContentType.Error:
                    _data1Logger.AddErrorContent(logcontent, ex);
                    break;

                case EnumLogContentType.Info:
                    _data1Logger.AddInfoContent(logcontent);
                    break;
                case EnumLogContentType.Warn:
                    _data1Logger.AddWarnContent(logcontent, ex);
                    break;
            }

            //if (ModuleLogAct != null)
            //{
            //    ModuleLogAct(logType, Module, log);
            //}
        }

        public static void RecordData2Log(EnumLogContentType logType, string log, Exception ex = null)
        {
            if (_data2Logger == null) return;
            var logcontent = string.Format("{0}, {1}", log, ex == null ? "" : ex.ToString());
            switch (logType)
            {
                case EnumLogContentType.Debug:
                    _data2Logger.AddDebugContent(logcontent);
                    break;

                case EnumLogContentType.Error:
                    _data2Logger.AddErrorContent(logcontent, ex);
                    break;

                case EnumLogContentType.Info:
                    _data2Logger.AddInfoContent(logcontent);
                    break;
                case EnumLogContentType.Warn:
                    _data2Logger.AddWarnContent(logcontent, ex);
                    break;
            }

            //if (ModuleLogAct != null)
            //{
            //    ModuleLogAct(logType, Module, log);
            //}
        }

        public static void RecordData3Log(EnumLogContentType logType, string log, Exception ex = null)
        {
            if (_data3Logger == null) return;
            var logcontent = string.Format("{0}, {1}", log, ex == null ? "" : ex.ToString());
            switch (logType)
            {
                case EnumLogContentType.Debug:
                    _data3Logger.AddDebugContent(logcontent);
                    break;

                case EnumLogContentType.Error:
                    _data3Logger.AddErrorContent(logcontent, ex);
                    break;

                case EnumLogContentType.Info:
                    _data3Logger.AddInfoContent(logcontent);
                    break;
                case EnumLogContentType.Warn:
                    _data3Logger.AddWarnContent(logcontent, ex);
                    break;
            }

            //if (ModuleLogAct != null)
            //{
            //    ModuleLogAct(logType, Module, log);
            //}
        }

        /// <summary>
        /// 记录用户的行为
        /// </summary>
        /// <param name="log"></param>
        public static void RecordUserOperationLog(string log, string operatorUser = null)
        {
            _systemLogger.AddDebugContent($"UserOperation：{log}");
            //_userOperationLogger.AddRecord(log, operatorUser);

            if (UserOperationLogAct!=null)
            {
                UserOperationLogAct(EnumLogContentType.Info, log);
            }
        }

        /// <summary>
        /// RecordRemoteLog日志
        /// </summary>
        /// <param name="sender">窗体控件名称</param>
        /// <param name="caption">日志标题</param>
        public static void RecordRemoteLog(EnumLogContentType logType, string log, Exception ex = null)
        {
            if (_remoteLogger == null) return;

            switch (logType)
            {
                case EnumLogContentType.Debug:
                    _remoteLogger.AddDebugContent(log);
                    break;

                case EnumLogContentType.Error:
                    _remoteLogger.AddErrorContent(log, ex);
                    break;

                case EnumLogContentType.Info:
                    _remoteLogger.AddInfoContent(log);
                    break;
                case EnumLogContentType.Warn:
                    _remoteLogger.AddWarnContent(log, ex);
                    break;
            }

            if (RemotingLogAct != null)
            {
                RemotingLogAct(logType, log + ex ?? "");
            }
        }
    }
}
