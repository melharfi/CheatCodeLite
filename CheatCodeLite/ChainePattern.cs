using System;
using System.Collections.Generic;
using System.Text;

namespace CheatCodeLite
{
    public class ChainePattern
    {
        public readonly int[] Pattern;
        public string Alias { get; set; }
        public ChainePattern(int[] pattern, string alias)
        {
            Alias = alias ?? throw new ArgumentNullException(nameof(alias));
            Pattern = pattern;
        }

        public int Count()
        {
            return Pattern.Length;
        }
    }
}
