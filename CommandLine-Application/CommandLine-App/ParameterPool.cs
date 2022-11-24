using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.Parameters;
using CommandLine_App.Parametrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App
{
    public class ParameterPool
    {
        public static readonly Dictionary<string, Parameter> Pool = new Dictionary<string, Parameter>()
            {
                { "all", new AllParam() },
                { "memory", new MemoryParam() },
                { "name", new NameParam() },
                { "pid", new PidParam() },
            };

        public static bool ContainsParam(string param)
        {
            foreach(var p in Pool.Values)
            {
                if (p.NamePool.Contains(param))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
