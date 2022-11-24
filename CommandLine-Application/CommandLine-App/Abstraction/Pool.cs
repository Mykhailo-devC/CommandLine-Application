using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class PoolAbs<T>
    {
        public abstract Dictionary<string, T> Pool { get; }// need DI
    }
}
