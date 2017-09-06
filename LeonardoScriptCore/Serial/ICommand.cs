using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public interface ICommand
    {
        ushort ID { get; }
        void Execute(PackageInfo info);
    }
    public abstract class CommandBase<TEntity> : ICommand
        where TEntity : class, IReceiveEntity
    {
        public abstract ushort ID { get; }
        protected abstract void Execute(TEntity entity);
        public void Execute(PackageInfo info)
        {
            Execute(info.Entity as TEntity);
        }
    }
}
