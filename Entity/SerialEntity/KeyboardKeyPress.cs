using LeonardoScriptCore.Serial;
using MiscUtil.Conversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.SerialEntity
{
    public class KeyboardKeyPress : ISendEntity
    {
        public Byte Key { get; set; }
        public ushort CmdID => 0x0101;

        public byte[] Serialize()
        {
            var data = new byte[1];
            var converter = EndianBitConverter.Big;
            int offset = 0;
            converter.CopyBytes(Key, data, offset);
            return data;
        }
    }
}
