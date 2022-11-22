using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Abstraction
{
    public abstract class Parameter
    {
        public abstract string Name { get; set; }
        public abstract List<string> NamePool { get; set; }

        public abstract override string ToString();

        public virtual string ArgView()
        {
            return $"[your_{Name}_argument]";
        }

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
