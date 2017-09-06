using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptIDE.Modules
{
    public interface ICoreModule : IBaseModule
    {
        void Initialize();
        void SetUp();
        void Start();
        void Stop();

        event EventHandler OnStarting;

        event EventHandler OnStarted;
        event EventHandler OnStopping;
    }
}
