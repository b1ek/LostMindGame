using LostMind.Engine.Config;
using LostMind.Engine.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Game.Localization {
    public enum LocaleType {
        enUS,
        ruRU
    }
    public static class Locale {
        static string localeFile = "# Please, put the original file back here.\n" +
                                   "# Your localization file may broken your game.\n" +
                                   "# Your file text so its not deleted:\n" +
                                   "%ORIGFILETXT%";

        public static string RawFile => localeFile;
        static string CurrentFilePath = FileLocation + CurrentFileName;
        static string CurrentFileName = "en-US.locale";
        public static readonly string FileLocation = @"\" + Environment.CurrentDirectory + @"\Resources\Locale";
        public static LocaleType CurrentLocale = LocaleType.enUS;

        public const int DemandedFileLengthMin = 16;

        public static async Task Initalize() {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Debug.WriteLine("Initalizing localization mechanizm...");
            CurrentFilePath = FileLocation + CurrentFileName;
            string tmp = File.ReadAllTextAsync(CurrentFilePath).Result;
            string chckSum = EasyCrypt.GetMD5(tmp).StringValue.Replace('-', '\0');

            Debug.WriteLine("Got locale file with MD5 checksum " + chckSum);

            /*if (tmp.Length != 16 && RegistryConfig.localeSafe) {
                stopwatch.Stop();
                Debug.WriteLine("Error! File length is not as expected!");
                Debug.WriteLine($"Lines got: {tmp.Length}, expected: {DemandedFileLengthMin}");
                Debug.WriteLine("Note: if you want to disable this error, set Registry key localeSafe to true");
                Debug.WriteLine("Localization mechanizm initalization took " + stopwatch.ElapsedMilliseconds + " ms");
                localeFile = localeFile.Replace("%ORIGFILETEXT%", tmp);
                await File.WriteAllTextAsync(CurrentFilePath, localeFile, Encoding.UTF8);
                throw new Exception("Localization file is corrupt!\nSee " + CurrentFilePath + " for details.");
                #pragma warning disable CS0162 // fuck this warning
                Environment.Exit(-1);
            }
            Debug.WriteLine("Localization mechanizm initalization took " + stopwatch.ElapsedMilliseconds + " ms");*/
        }
    }
}
