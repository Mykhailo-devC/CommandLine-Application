using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public interface IPool<T>
    {
        public Dictionary<string, T> Pool { get; set; }
    }
}
