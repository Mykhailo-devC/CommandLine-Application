using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.Pools
{
    public class CommandDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<ParameterType, string> Parameters { get; set; }
        public CommandDescription(string name, string description, Dictionary<ParameterType, string> parameters)
        {
            Name = name;
            Description = description;
            Parameters = parameters;
        }

        public string this[ParameterType index]
        {
            get
            {
                if(index == ParameterType.Undefined)
                {
                    Console.WriteLine(ToString());
                    return null;
                }

                return Parameters[index];
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(Description);

            foreach(var param in Parameters.Values)
            {
                builder.AppendLine(param);
            }

            return builder.ToString();
        }
    }
}
