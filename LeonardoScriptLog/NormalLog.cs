using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace LeonardoScriptLog
{
    public class NormalLog : ILog
    {
        private static ConcurrentDictionary<string, log4net.ILog> _ListILog
            = new ConcurrentDictionary<string, log4net.ILog>();
        private log4net.ILog _Ilog;
        #region 构造函数
        public NormalLog(log4net.ILog log)
        {
            string loggerName = log.Logger.Name;
            if (!_ListILog.Keys.Contains(loggerName))
            {
                _ListILog.TryAdd(loggerName, log);
            }
            else
            {
                _ListILog.TryUpdate(loggerName, log, log);
            }
            _ListILog.TryGetValue(loggerName, out _Ilog);
        }
        public NormalLog(string loggername)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(loggername);
            string loggerName = log.Logger.Name;
            if (!_ListILog.Keys.Contains(loggerName))
            {
                _ListILog.TryAdd(loggerName, log);
            }
            else
            {
                _ListILog.TryUpdate(loggerName, log, log);
            }
            _ListILog.TryGetValue(loggerName, out _Ilog);
        }
        #endregion

        public void Debug(string message, Exception exception = null)
        {
            LogMessage messageEntity = new LogMessage()
            {
                Message = message
            };
            Debug(message, exception);
        }

        public void Debug(object messageEntity, Exception exception = null)
        {
            if (_Ilog.IsDebugEnabled)
            {
                if (exception != null)
                {
                    _Ilog.Debug(messageEntity, exception);
                }
                else
                {
                    _Ilog.Debug(messageEntity);
                }
            }
        }

        public void Error(string message, Exception exception = null)
        {
            LogMessage messageEntity = new LogMessage()
            {
                Message = message
            };
            Error(messageEntity, exception);
        }

        public void Error(object messageEntity, Exception exception = null)
        {
            if (_Ilog.IsErrorEnabled)
            {
                if (exception != null)
                {
                    _Ilog.Error(messageEntity, exception);
                }
                else
                {
                    _Ilog.Error(messageEntity);
                }
            }
        }

        public void Fatal(string message, Exception exception = null)
        {
            LogMessage messageEntity = new LogMessage()
            {
                Message = message
            };
            Fatal(messageEntity, exception);
        }

        public void Fatal(object messageEntity, Exception exception = null)
        {
            if (_Ilog.IsFatalEnabled)
            {
                if (exception != null)
                {
                    _Ilog.Fatal(messageEntity, exception);
                }
                else
                {
                    _Ilog.Fatal(messageEntity);
                }
            }
        }

        public void Info(string message, Exception exception = null)
        {
            LogMessage messageEntity = new LogMessage()
            {
                Message = message
            };
            Info(message, exception);
        }

        public void Info(object messageEntity, Exception exception = null)
        {
            if (_Ilog.IsInfoEnabled)
            {
                if (exception!=null)
                {
                    _Ilog.Info(messageEntity, exception);
                }
                else
                {
                    _Ilog.Info(messageEntity);
                }
            }
        }

        public void Warn(string message, Exception exception = null)
        {
            LogMessage messageEntity = new LogMessage()
            {
                 Message=message
            };
            Warn(messageEntity, exception);
        }

        public void Warn(object messageEntity, Exception exception = null)
        {
            if (_Ilog.IsWarnEnabled)
            {
                if (exception!=null)
                {
                    _Ilog.Warn(messageEntity, exception);
                }
                else
                {
                    _Ilog.Warn(messageEntity);
                }
            }
        }
    }
}
