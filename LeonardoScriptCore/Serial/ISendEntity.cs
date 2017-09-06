using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public interface ISendEntity
    {
        ushort CmdID { get; }
        byte[] Serialize();
    }
}
