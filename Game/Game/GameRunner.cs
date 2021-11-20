using LostMind.Engine.Animation;
using LostMind.Engine.Config;
using LostMind.Engine.User;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LostMind.Game.Game {
    public class GameRunner {
        public void Run() {

            Console.CursorVisible = false;
            Console.ResetColor(); Console.Clear();
            UCO.TrySetSize(120, 30);
            UserConsoleNativeInterface.setFont("system", 1, 10, -1);
            UserConsoleNativeInterface.setTransparency(255);

            var cp = Console.GetCursorPosition();
            const int animMarginTop = 3;
            const int animMarginLeft = 20;
            string pathToLogo = Environment.CurrentDirectory + @"\Resources\Logo.txt";
            if (RegistryConfig.noStartupLogoAnim) {
                Animation anim = Animation.createSimple(File.ReadAllText(pathToLogo), animMarginLeft, animMarginTop, 1, ConsoleColor.DarkGreen);
                anim.run();
            } else {
                Animation anim = Animation.createSimple(File.ReadAllText(pathToLogo), animMarginLeft, animMarginTop, 32);
                anim.run();
            }
            bootload(16, true);
        }
        void bootload(int marginLeft, bool allowRegistryConfig) {
            const string presskeyTxt = "Press any key to launch the game...";
            if (!RegistryConfig.startGameWithoutBootloader && allowRegistryConfig) {
                UserConsoleWriter writer = new UserConsoleWriter(marginLeft, Console.CursorTop + 2);
                writer.FancyWrite("Booting up...", 1).Wait();
                Thread.Sleep(64);
                writer.FancyWrite("Operating system: " + Environment.OSVersion, 1).Wait();
                writer.FancyWrite("Using .NET runtime version " + Environment.Version, 1).Wait();

                var myProcess = Process.GetCurrentProcess();

                writer.WriteLine();
                writer._sx += 2; writer._x = writer._sx;
                var msg =
                $"Physical memory usage     : " + myProcess.WorkingSet64 + "\n" +
                $"Base priority             : " + myProcess.BasePriority + "\n" +
                $"Priority class            : " + myProcess.PriorityClass + "\n" +
                $"User processor time       : " + myProcess.UserProcessorTime + "\n" +
                $"Privileged processor time : " + myProcess.PrivilegedProcessorTime + "\n" +
                $"Total processor time      : " + myProcess.TotalProcessorTime + "\n" +
                $"Paged system memory size  : " + myProcess.PagedSystemMemorySize64 + "\n" +
                $"Paged memory size         : " + myProcess.PagedMemorySize64 + "\n"; writer.Write(msg);
                writer._sx -= 2; writer._x = writer._sx;
                Thread.Sleep(512);
                writer.FancyWrite("\n\n" + presskeyTxt).Wait();
            } else { UserNativeLib.printToXY(presskeyTxt, marginLeft, Console.CursorTop + 2); }
            UserKeyInput.awaitKeyPress();
        }
    }
}
