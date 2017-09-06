using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public class CommandManager : ICommandManager
    {
        private CommandManager() { }
        private ConcurrentDictionary<ushort, ICommand> _Commands = new ConcurrentDictionary<ushort, ICommand>();
        public bool AddCommand(ICommand command)
        {
            return _Commands.TryAdd(command.ID, command);
        }
        public ICommand RetrieveCommand(ushort cmdId)
        {
            ICommand command;
            if (_Commands.TryGetValue(cmdId, out command))
            {
                return command;
            }
            return null;
        }
        public ICommand RemoveCommand(ushort cmdId)
        {
            ICommand cmd;
            if (_Commands.TryRemove(cmdId, out cmd))
            {
                return cmd;
            }
            return null;
        }
        public IEnumerable<ICommand> GetAllCommand()
        {
            return _Commands.Values;
        }

        private static ICommandManager _Instance;
        public static ICommandManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CommandManager();
                return _Instance;
            }
        }
    }
}
