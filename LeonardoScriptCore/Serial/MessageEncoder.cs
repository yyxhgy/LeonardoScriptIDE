using MiscUtil.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    internal class MessageEncoder
    {
        public byte[] EncodeMessage(ISendEntity entity)
        {
            var entitydata = entity.Serialize();
            var data = new byte[entitydata.Length + 7];
            var converter = EndianBitConverter.Big;
            ushort bodylen = (ushort)(entitydata.Length + 2);
            int offset = 0;
            ushort flag = 0x5AA5;
            byte check = 0;
            converter.CopyBytes(flag, data, 0);
            offset += 2;
            converter.CopyBytes(entity.CmdID, data, offset);
            offset += 2;
            Buffer.BlockCopy(entitydata, 0, data, offset, (int)entitydata.Length);
            for (int i = 4; i < data.Length - 1; ++i)
            {
                check += data[i];
            }
            data[data.Length - 1] = check;
            return data;
        }
    }
}
