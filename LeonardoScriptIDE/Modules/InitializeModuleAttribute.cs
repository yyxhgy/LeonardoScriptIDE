using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptIDE.Modules
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InitializeModuleAttribute : Attribute
    {
        public string Method { get; set; } = "Initialize";
        public int Order { get; set; } = 1000;
    }
}
