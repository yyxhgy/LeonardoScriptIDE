using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public class ReceiveFilter
    {
        public PackageInfo ResolvePackage(byte[] data)
        {
            ushort flag = data.ReadUInt16(0);
            if (flag != 0x5AA5)
                return null;
            int offset = 2;
            var len = data.ReadUInt16(offset);
            offset += 2;
            var cmdid = data.ReadUInt16(offset);
            offset += 2;
            var factory = RequestEntityFactoryManager.GetEntityFactoryByKey(cmdid);
            if (factory == null)
            {
                return new PackageInfo(cmdid, null);
            }
            byte check = data[4 + len + 1 - 1];
            byte cresult = 0;
            for (int i = 4; i < 4 + len; ++i)
            {
                cresult += data[i];
            }
            if (check != cresult)
            {
                return new PackageInfo(cmdid, null);
            }
            IReceiveEntity entity = null;
            try
            {
                var buffer = new byte[len - 1];
                Buffer.BlockCopy(data, offset, buffer, 0, len - 1);
                entity = factory.CreateRequestEntity(buffer);
            }
            catch (Exception e)
            {
                return null;
            }
            PackageInfo package = new PackageInfo(cmdid, entity);
            return package;
        }
    }
}
