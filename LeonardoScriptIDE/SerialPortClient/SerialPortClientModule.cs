using Autofac;
using LeonardoScriptCore.Serial;
using LeonardoScriptIDE.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptIDE.SerialPortClient
{
    [InitializeModule]
    public class SerialPortClientModule : Autofac.Module, ISerialPortClientModule
    {
        public void Initialize()
        {
            var coreModule = Module.GetModule<ICoreModule>();
            coreModule.OnStarting += (sender, e) =>
            {
                ConnectSubSystem();
            };
            coreModule.OnStopping += (sender, e) =>
            {
                Stop();
            };
        }

        public void Stop()
        {
            var client = Module.Container.Resolve<ISerialPortClient>();
            client.Stop();
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LeonardoScriptCore.Serial.SerialPortClient>().As<ISerialPortClient>().SingleInstance();
            base.Load(builder);
        }

        public void ConnectSubSystem()
        {
            //var client = Module.Container.Resolve<ISerialPortClient>();
            //client.Start("COM1", 9600);
            //client.Send(new SystemStart() { Time = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds });
        }
    }
}
