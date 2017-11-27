using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptLog
{
    public interface ILog
    {
        void Debug(string message, Exception exception = null);
        void Debug(object messageEntity, Exception exception = null);
        void Info(string message, Exception exception = null);
        void Info(object messageEntity, Exception exception = null);
        void Warn(string message, Exception exception = null);
        void Warn(object messageEntity, Exception exception = null);
        void Error(string message, Exception exception = null);
        void Error(object messageEntity, Exception exception = null);
        void Fatal(string message, Exception exception = null);
        void Fatal(object messageEntity, Exception exception = null);
    }
}
