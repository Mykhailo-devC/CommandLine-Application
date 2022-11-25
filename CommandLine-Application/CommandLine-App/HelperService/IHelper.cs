using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_App.HelperService
{
    public interface IHelper
    {
        public void StandartHelp();
        public void HelpChooseCommand(string param);
        public void HelpChooseParameter(List<string> param);
    }
}
