using System;
using System.Collections.Generic;
using System.Text;

namespace CheatCodeLite
{
    public class PatternEventHandler : EventArgs
    {
        public ChainePattern ChainePattern { get; set; }
        public PatternEventHandler(ChainePattern chainePattern)
        {
            ChainePattern = chainePattern;
        }
    }
}
