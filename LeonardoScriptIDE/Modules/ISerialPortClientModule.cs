﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptIDE.Modules
{
    public interface ISerialPortClientModule : IBaseModule
    {
        void Initialize();
        void Stop();
    }
}
