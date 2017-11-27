using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LeonardoScriptCore.Logging
{
    public class Log4NetLogFactory : LogFactoryBase
    {
        public Log4NetLogFactory()
            :base("log4net.config")
        {

        }
        public Log4NetLogFactory(string log4netconfig)
            :base(log4netconfig)
        {
            if (!IsSharedConfig)
            {
                XmlConfigurator.Configure(new FileInfo(ConfigFile));
            }
            else
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFile);
                var docElement = xmlDoc.DocumentElement;
                var perfLogNode = docElement.SelectSingleNode("logger[@name='Performance']");
                if (perfLogNode != null)
                    docElement.RemoveChild(perfLogNode);
                XmlConfigurator.Configure(docElement);
            }
        }
        public override ILog GetLog(string name)
        {
            return new Log4NetLog(LogManager.GetLogger(name));
        }
    }
}
