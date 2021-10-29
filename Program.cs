using System;
using System.Threading;
using System.Runtime.InteropServices;
using LostMind.Classes.User;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LostMind.Classes.Animation;
using System.Diagnostics;

namespace LostMind
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        private static string locale = "en-US";

        static void Main(string[] args) {

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Console.CursorVisible = false;
            var cp = Console.GetCursorPosition();
            Animation anim = Animation.createSimple(File.ReadAllText(@"C:\Users\blek\source\repos\MyLifeGame\Resources\Logo.txt"), cp.Left+16, cp.Top+1, 2);
            anim.run();
            #region Bootload
            UserConsoleWriter writer = new UserConsoleWriter(Console.CursorLeft, Console.CursorTop);
            writer.fancyWrite("Booting up...", 1).Wait();
            Thread.Sleep(64);
            writer.fancyWrite("Operating system: " + Environment.OSVersion, 1).Wait();
            writer.fancyWrite("Using .NET runtime version " + Environment.Version, 1).Wait();

            var myProcess = Process.GetCurrentProcess();

            writer.write("\n");
            writer._sx+=2; writer._x = writer._sx;
            var msg = 
            $"Physical memory usage     : " + myProcess.WorkingSet64 + "\n" +
            $"Base priority             : " + myProcess.BasePriority + "\n" +
            $"Priority class            : " + myProcess.PriorityClass + "\n" +
            $"User processor time       : " + myProcess.UserProcessorTime + "\n" +
            $"Privileged processor time : " + myProcess.PrivilegedProcessorTime + "\n" +
            $"Total processor time      : " + myProcess.TotalProcessorTime + "\n" +
            $"Paged system memory size  : " + myProcess.PagedSystemMemorySize64 + "\n" +
            $"Paged memory size         : " + myProcess.PagedMemorySize64 + "\n"; writer.write(msg);
            writer._sx-=2; writer._x = writer._sx;
            Thread.Sleep(512);
            writer.write("Trying to load russian console mode...");
            var ah = writer.fancyWrite("\n.............", 64, 256, "");
            ah.Wait();
            writer.fancyWrite(" Done").Wait();
            #endregion
            writer.write("\nWould you like to change the language to Russian? Press Y or N" + 
                         "\nХотели бы вы сменить язык на русский? Нажмите Y или N\n\n");
            var userRussian = false;
            while (true) {
                if (Classes.User.UserKeyInput.isKeyPressed(ConsoleKey.Y)) { 
                    userRussian = true;
                    break;
                }
                if (Classes.User.UserKeyInput.isKeyPressed(ConsoleKey.N)) {
                    userRussian = false;
                    break;
                }
            }
            if (userRussian) {
                writer.write("Язык изменен на русский.");
                locale = "ru-RU";
            }
            else {
                writer.write("OK! Still using English.\nNote: you still can change it in Settings menu.");
            }

        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.SetCursorPosition(0, Console.CursorTop+3);
        }
    }
}
