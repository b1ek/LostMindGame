using LostMind.Engine.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Localization
{
    public class Locale
    {
        static readonly string localeFileDir = Environment.CurrentDirectory + @"\Resources\Locale\";
        static string currentLocale = "en-US";
        const string localeExtension = ".locale";
        public string this[int i] {
            get => File.ReadAllTextAsync(localeFileDir + currentLocale + localeExtension).Result.Replace('\r', char.MinValue).Split("\n")[i];
        }


        public string gameTitle {
            get => this[0];
        }
        public string mainMenu {
            get => this[1];
        }
    }
}
