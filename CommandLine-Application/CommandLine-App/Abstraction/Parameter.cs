using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class Parameter
    {
        public abstract string Name { get; set; }
        public abstract List<string> NamePool { get; set; }
        public abstract short ArgumentsCount { get; set; }
        public abstract string ArgumentDescription { get; set; }
        public abstract override string ToString();

        protected StringBuilder PrintShortCuts (StringBuilder str)
        {
            str.Append($" (Shortcuts : ");
            for (int i = 1; i < NamePool.Count; i++)
            {
                str.Append($"{NamePool[i]}, ");
            }
            str.Remove(str.Length - 2, 2);
            str.Append(").");

            return str;
        }
    }
}
