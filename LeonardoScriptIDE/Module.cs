using Autofac;
using LeonardoScriptIDE.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptIDE
{
    public class Module
    {
        public static IContainer Container
        {
            get;
            private set;
        }
        public static void Initial()
        {
            
            LeonardoScriptCore.Serial.RequestEntityFactoryManager.Register(AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asb => asb.GetName().Name == "LeonardoScriptIDE"), LeonardoScriptCore.Serial.CommandManager.Instance);
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyModules(assembly);

            var baseType = typeof(IBaseModule);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies).Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract).AsImplementedInterfaces().SingleInstance();

            Container = builder.Build();
        }
        //public static void ModuleInitial()
        //{
        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //    var assembly = assemblies.Where(assemble => assemble.FullName.StartsWith("com.jyjet.vendingmachine,")).First();
        //    IEnumerable<Type> ts = assembly.GetTypes();
        //    List<Tuple<Type, InitializeModuleAttribute>> list = new List<Tuple<Type, InitializeModuleAttribute>>();
        //    foreach (var t in ts)
        //    {
        //        var ats = t.GetCustomAttributes(typeof(InitializeModuleAttribute), false);
        //        if (ats == null || ats.Count() == 0)
        //        {
        //            continue;
        //        }
        //        InitializeModuleAttribute at = ats[0] as InitializeModuleAttribute;
        //        list.Add(new Tuple<Type, InitializeModuleAttribute>(t, at));
        //    }
        //    list.Sort((t1, t2) =>
        //    {
        //        if (t1.Item2.Order > t2.Item2.Order)
        //            return 1;
        //        else if (t1.Item2.Order < t2.Item2.Order)
        //            return -1;
        //        else
        //            return 0;
        //    });
        //    foreach (var t in list)
        //    {
        //        var m = t.Item1.GetMethod(t.Item2.Method);
        //        if (m == null)
        //            continue;
        //        var inf = t.Item1.GetInterfaces().FirstOrDefault(tt => tt.Name != "IBaseModule" && tt.Name != "IModule");
        //        if (inf == null)
        //            continue;
        //        var inst = Container.Resolve(inf);
        //        m.Invoke(inst, null);
        //    }
        //}
        private static object _Locker = new object();
        public static T GetModule<T>()
        {
            T instance;
            lock (_Locker)
            {
                instance = Container.Resolve<T>();
            }
            return instance;
        }
    }
}
