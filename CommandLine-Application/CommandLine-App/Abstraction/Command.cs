using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class Command
    {
        public abstract string Name { get; set; }
        public abstract string ArgumentDescription { get; set; }
        public abstract bool Execute(params string[] param);
        public abstract override string ToString();
        public abstract void PrintBaseToString();

        public void PrintArgumentTip()
        {
            Console.Write(ArgumentDescription);
        }


    }
}
