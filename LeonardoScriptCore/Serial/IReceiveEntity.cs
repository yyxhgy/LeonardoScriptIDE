using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public interface IReceiveEntity
    {
        void Deserialize(byte[] data);
    }
}
