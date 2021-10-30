using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.Localization
{
    public static class Localization
    {
        public struct Localizations
        {
            public string localeCode;
            public string gameTitle;
            public string gameSetup;
        }

        public static Localizations getLocale(string code)
        {
            var a = new Localizations();
            a.localeCode = code;
            string lf = File.ReadAllText(@".\Resources\Locale\" + code + ".locale");
            string[] _lf = lf.Replace("\r", "").Split("\n");
            a.gameTitle = _lf[0];
            a.gameSetup = _lf[1];

            return a;
        }
    }
}
