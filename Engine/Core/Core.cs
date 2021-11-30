using LostMind.Engine.User;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.Core {
    public class Core {
        BackgroundWorker ControlsWorker;
        CancellationTokenSource sauce;
        LostMind.Game.Game.GameRunner gameRunner;
        public const string version  = "1__0.1.3 INDEV";
        public const string codename = "MATRIX_MIX";


        public void Run() {
            Console.ResetColor();
            Console.Clear();
            UserConsoleNativeInterface.setFont("Lucida Console", 100);
            Console.TreatControlCAsInput = true;
            Registry.CurrentUser.CreateSubKey("CONSOLE").SetValue("VirtualTerminalLevel", 1, RegistryValueKind.DWord);

            Console.WriteLine(
                "Running LostEngine version \x1b[36m" + version +
                " " + codename +
                "\x1b[0m\n\nForged in the depths of hell by b!ek" +
                "\nMy email: \x1b[36mcreeperywime@gmail.com\x1b[0m");
            var workerPack = User.UserKeyInput.CreateWorker();
            ControlsWorker = workerPack.worker;
            sauce = workerPack.cancellation;
            double c = 2;
            int cy = User.UserNativeLib.ConsoleCursor_Y + 2;
            Console.CursorVisible = false;
            while (true) {
                if (c < 0) break;
                Thread.Sleep(50);
                c = c - 0.1d;
                UserNativeLib.printToXY(new string(' ', Console.BufferWidth), 0, cy);
                UserNativeLib.printToXY($"This console will be gone in \x1b[36m{(double)(int)(c * 10) / 10}\x1b[0m seconds...", 0, cy);
            }
            Console.ResetColor();
            Console.Clear();
            //controller = new Game.GameController();
            gameRunner = new Game.Game.GameRunner();
            gameRunner.Run();

        }

        /**<summary>BSOD-style exception handler.</summary>*/
        public static void UnhandledException(object s, UnhandledExceptionEventArgs e) {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            UserConsoleNativeInterface.setFont("Consolas", 2, 15, -1);
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);
            Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("\x1b[48;2;1;0;171m");
            Console.Clear();

            Console.TreatControlCAsInput = true;
            var beepTask = Task.Run(() => { Console.Beep(0, int.MaxValue); });

            Console.WriteLine("A problem has been detected and the game was shut down to prevent damage to your computer.\n");
            Console.WriteLine(LostMind.Engine.Utils.CamelCaseSplit(e.ExceptionObject.GetType().Name).ToUpper().Replace(' ', '_') + "\n> " + ((Exception)e.ExceptionObject).Message);
            Console.WriteLine("\nIf this is the first time you've seen this stop error screen, restart your computer.\nIf these screen appears again, follow these steps:\n");

            Console.WriteLine("Check to make sure any new mods or updates to the game or runtime was properly installed.");
            Console.WriteLine("If this is a new installation, ask your geek firend about it and he might help,");
            Console.WriteLine("you may need some windows/runtime updates.\n");
            Console.WriteLine("\nIf you are  developer who changed something and it stopped working,");
            Console.WriteLine("undo your changes.");

            Console.WriteLine("If problems continue, disable or remove any newly installed hardware or software. Disable BIOS memory options such as caching or shadowing.\nIf you need to use safe mode to remove or disable components, restart your computer, save your game saves to save location, and re-install the game or .NET runtime.");

            Console.WriteLine("\nTechnical information:");

            Console.WriteLine("*** STOP: 0x000000FE (0x00000008, 0x00000000666, 0x00000009, 0x847075cc).");
            Console.WriteLine("***   gv3.sys - Address F86B5A89 base at F8685000. DateStamp " + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString("X"));
            Console.WriteLine("*** Press S to display StackTrace");
            while (true) if (Console.KeyAvailable) if (Console.ReadKey(true).Key == ConsoleKey.S) break;
            Console.WriteLine("*** StackTrace: ");
            Console.WriteLine(((Exception)e.ExceptionObject).StackTrace);
            Debug.WriteLine(((Exception)e.ExceptionObject).Message + " StackTrace: \n" + ((Exception)e.ExceptionObject).StackTrace);
            Console.WriteLine("\nPress any key to exit the program.");
            while (true) if (Console.KeyAvailable) break;

            for (byte i = 255; i > 1; i--)
                UserConsoleNativeInterface.setTransparency(i);

            Environment.Exit(0);
        }
    }
}
