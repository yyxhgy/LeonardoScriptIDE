using LeonardoScriptLog.PatternConverter;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptLog
{
    public class CustomLayout : PatternLayout
    {
        public CustomLayout()
        {
            #region 内部自定义
            AddConverter("ClientPort", typeof(ClientPortPatternConverter));
            #endregion
        }
    }
}
