using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LostMind.Engine.Util {
    public static class Util {
        public static string camelCaseSplit(string value) => Regex.Replace(value, "(\\B[A-Z])", " $1");
    }
}
