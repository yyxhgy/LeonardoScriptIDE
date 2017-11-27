using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Logging
{
    public class Log4NetLog : ILog
    {
        private log4net.ILog _Log;
        public Log4NetLog(log4net.ILog log)
        {
            _Log = log ?? throw new ArgumentNullException("log");
        }
        public bool IsDebugEnabled { get { return _Log.IsDebugEnabled; } }

        public bool IsErrorEnabled { get { return _Log.IsErrorEnabled; } }

        public bool IsFatalEnabled { get { return _Log.IsFatalEnabled; } }

        public bool IsInfoEnabled { get { return _Log.IsInfoEnabled; } }

        public bool IsWarnEnabled { get { return _Log.IsWarnEnabled; } }

        public void Debug(object message)
        {
            _Log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _Log.Debug(message, exception);
        }

        public void DebugFormat(string format, object arg0)
        {
            _Log.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _Log.DebugFormat(format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            _Log.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            _Log.DebugFormat(format, arg0, arg1, arg2);
        }

        public void Error(object message)
        {
            _Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _Log.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            _Log.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _Log.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            _Log.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            _Log.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void Fatal(object message)
        {
            _Log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _Log.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            _Log.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _Log.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            _Log.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            _Log.FatalFormat(format, arg0, arg1, arg2);
        }

        public void Info(object message)
        {
            _Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _Log.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            _Log.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _Log.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            _Log.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            _Log.InfoFormat(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            _Log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _Log.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            _Log.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _Log.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _Log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            _Log.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            _Log.WarnFormat(format, arg0, arg1, arg2);
        }
    }
}
