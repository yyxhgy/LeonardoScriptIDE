using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public class DataStream : EventArgs
    {
        public Byte[] Data { get; set; }
        public DataStream(byte[] data, int length)
        {
            Data = new byte[length];
            data.CopyTo(Data, 0);
        }
    }
}
