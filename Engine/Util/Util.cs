using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LostMind.Engine.Util {
    public static class Util {
        public static string CamelCaseSplit(string value) => Regex.Replace(value, "(\\B[A-Z])", " $1");
        public static IEnumerable<string> SplitByCount(string str, int chunkSize) =>
            Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));

    }
}
