using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public class PackageInfo
    {
        public ushort Key { get; private set; }
        public IReceiveEntity Entity { get; private set; }
        public PackageInfo(ushort key,IReceiveEntity entity)
        {
            Key = key;
            Entity = entity;
        }
    }
}
