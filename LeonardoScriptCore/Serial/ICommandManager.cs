using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public interface ICommandManager
    {
        bool AddCommand(ICommand command);
        ICommand RetrieveCommand(ushort cmdId);
        ICommand RemoveCommand(ushort cmdId);
        IEnumerable<ICommand> GetAllCommand();
    }
}
