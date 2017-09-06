using LeonardoScriptIDE.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptIDE
{
    [InitializeModule]
    public class CoreModule : Autofac.Module, ICoreModule
    {
        public void Initialize()
        {

        }

        public void SetUp()
        {

        }

        public void Start()
        {
            OnStarting?.Invoke(this, new EventArgs());
            OnStarted?.Invoke(this, new EventArgs());
        }

        public void Stop()
        {
            OnStopping?.Invoke(this, new EventArgs());
        }
        #region 事件
        public event EventHandler OnStarting;
        public event EventHandler OnStarted;
        public event EventHandler OnStopping;
        #endregion

    }
}
