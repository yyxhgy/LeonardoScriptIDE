using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public interface IRequestEntityFactory
    {
        IReceiveEntity CreateRequestEntity(byte[] data);
    }
    public class RequestEntityFactory<TRequestEntity> : IRequestEntityFactory
        where TRequestEntity : IReceiveEntity, new()
    {
        public IReceiveEntity CreateRequestEntity(byte[] data)
        {
            var length = data.Length;
            if (length == 0)
                return new TRequestEntity();
            var entity = new TRequestEntity();
            entity.Deserialize(data);
            return entity;
        }
    }
    public static class RequestEntityFactoryManager
    {
        private static Dictionary<uint, IRequestEntityFactory> m_Factories = new Dictionary<uint, IRequestEntityFactory>();
        public static IEnumerable<uint> RegistedCommands
        {
            get
            {
                return m_Factories.Keys;
            }
        }
        public static void Register(IEnumerable<ICommand> commands, ICommandManager cmdManager)
        {
            var factoryTypeBase = typeof(RequestEntityFactory<>);
            foreach (var c in commands)
            {
                var cmdType = c.GetType().BaseType;

                while (cmdType != null)
                {
                    if (!cmdType.FullName.StartsWith("LeonardoScriptCore.Serial.CommandBase"))
                    {
                        cmdType = cmdType.BaseType;
                        continue;
                    }
                    var entityType = cmdType.GetGenericArguments().Last();
                    cmdManager.AddCommand(c);
                    m_Factories[c.ID] = (IRequestEntityFactory)Activator.CreateInstance(factoryTypeBase.MakeGenericType(entityType));
                    break;
                }
            }
        }
        public static void Register(ICommandManager cmdManager)
        {
            Register(AppDomain.CurrentDomain.GetAssemblies(), cmdManager);
        }
        public static void Register(Assembly[] assemblies, ICommandManager cmdManager)
        {
            foreach (var assembly in assemblies)
            {
                Register(assembly, cmdManager);
            }
        }
        public static void Register(Assembly assembly, ICommandManager cmdManager)
        {
            var factoryTypeBase = typeof(RequestEntityFactory<>);

            IEnumerable<Type> ts = assembly.GetTypes();//assembly.GetExportedTypes();

            foreach (var t in ts)
            {
                if (t.GetInterface(typeof(ICommand).Name) == null)
                {
                    continue;
                }
                var cmdType = t.BaseType;

                while (cmdType != null)
                {
                    if (!cmdType.FullName.StartsWith("LeonardoScriptCore.Serial.CommandBase"))
                    {
                        cmdType = cmdType.BaseType;
                        continue;
                    }
                    var cmdInst = Activator.CreateInstance(t) as ICommand;
                    var entityType = cmdType.GetGenericArguments().Last();
                    cmdManager.AddCommand(cmdInst);
                    m_Factories[cmdInst.ID] = (IRequestEntityFactory)Activator.CreateInstance(factoryTypeBase.MakeGenericType(entityType));

                    break;
                }
            }
        }

        public static IRequestEntityFactory GetEntityFactoryByKey(uint key)
        {
            IRequestEntityFactory t;
            if (m_Factories.TryGetValue(key, out t))
            {
                return t;
            }
            return null;
        }
    }
}
