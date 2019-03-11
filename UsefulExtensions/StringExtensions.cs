using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UsefulExtensions
{
    public static class StringExtensions
    {
        public static int Occurrence(this String instr, string search)
        {
            return Regex.Matches(instr, search).Count;
        }
    }
}
