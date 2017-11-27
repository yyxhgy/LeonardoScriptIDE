using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptLog
{
    public class LogManager
    {
        /// <summary>
        /// 初始化HY.Log,  Log配置文件需要写到 Web.config  OR  App.config
        /// </summary>
        public static void Init()
        {
            XmlConfigurator.Configure();
        }
        public static void Init(string configFileName)
        {
            XmlConfigurator.Configure(new FileInfo(configFileName));
        }
        /// <summary>
        /// 检索Logger名称返回日志处理接口
        /// </summary>
        /// <param name="name">Logger名称</param>
        /// <returns>日志接口</returns>
        public static ILog GetLogger(string name)
        {
            var log4Logger = log4net.LogManager.GetLogger(name);
            return new NormalLog(log4Logger);
        }
    }
}
