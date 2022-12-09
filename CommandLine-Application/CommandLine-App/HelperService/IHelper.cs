using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.HelperService
{
    public interface IHelper
    {
        public HelpCommand HelpCommand { get; }
        public void StandartHelp();
        public void HelpChooseCommand(string param);
        public void HelpChooseParameter(string param);
    }
}
