using System;
using System.Runtime.InteropServices;
using LostMind.Classes.User;
using System.Diagnostics;
using LostMind.Classes.GameController;
using LostMind.Classes.UI;
using LostMind.Classes.Util;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LostMind.Classes.Animation;
using LostMind.Classes.Config;
using System.IO;
using System.Threading;

namespace LostMind
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        /**<summary>Game controller.</summary>*/
        public static GameController gameController = new GameController(new(), new());

        /**<summary>
         * Main entry point.
         * </summary>
         */
        static void Main(string[] args) {
            MachineTest.runDefaultNotBrokenTest();

            // DONT TOUCH
            Process.GetCurrentProcess().EnableRaisingEvents = true;
            Process.GetCurrentProcess().Exited += (s, e) => { OnProcessExit(s, e); };
            if (RegistryConfig.AllowBSODStyleException) AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);


            UserKeyInput.InstallHook();

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
            }
            else {
                Animation anim = Animation.createSimple(File.ReadAllText(pathToLogo), animMarginLeft, animMarginTop, 32);
                anim.run();
            }
            #region Bootload
            if (!RegistryConfig.startGameWithoutBootloader) {
                UserConsoleWriter writer = new UserConsoleWriter(animMarginLeft, Console.CursorTop+2);
                writer.FancyWrite("Booting up...", 1).Wait();
                Thread.Sleep(64);
                writer.FancyWrite("Operating system: " + Environment.OSVersion, 1).Wait();
                writer.FancyWrite("Using .NET runtime version " + Environment.Version, 1).Wait();

                var myProcess = Process.GetCurrentProcess();

                writer.Write("\n");
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
                writer.FancyWrite("\n\nPress ESC to launch the game...").Wait();
            } else { UCO.WriteXY(animMarginLeft, Console.CursorTop + 2, "Press ESC to launch the game..."); }
            #endregion

            UserKeyInput.awaitKeyPress();

            UCO.FlushConsole();
        }

        /**<summary>Method that is called on program exit.</summary>*/
        public static void OnProcessExit(object sender, EventArgs e) {
            Console.ResetColor(); Console.Clear();
            Console.SetCursorPosition(0, Console.CursorTop + 16);
            Console.CursorVisible = true;
            UserKeyInput.stopThread();
            Process.GetCurrentProcess().Kill();
        }

        public static void DoSafeExit() {
            Console.ResetColor(); Console.Clear();
            Console.SetCursorPosition(0, Console.CursorTop + 16);
            Console.CursorVisible = true;
            UserKeyInput.stopThread();
            Process.GetCurrentProcess().Kill();
        }

        static bool exceptionHandled = false;
        /**<summary>BSOD-style exception handler.</summary>*/
        public static void UnhandledException(object s, UnhandledExceptionEventArgs e) {
            if (exceptionHandled) Environment.Exit(0x000000FE);
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
            var beepTask = Task.Run(() => { Beep(0, int.MaxValue); });

            Console.WriteLine("A problem has been detected and the game was shut down to prevent damage to your computer.\n");
            Console.WriteLine(Util.camelCaseSplit(e.ExceptionObject.GetType().Name).ToUpper().Replace(' ', '_') + "\n> " + ((Exception)e.ExceptionObject).Message);
            Console.WriteLine("\nIf this is the first time you've seen this stop error screen, restart your computer.\nIf these screen appears again, follow these steps:\n");

            Console.WriteLine("Check to make sure any new mods or updates to the game or runtime was properly installed.");
            Console.WriteLine("If this is a new installation, ask your geek firend about it and he might help,");
            Console.WriteLine("you may need some windows/runtime updates.\n");
            Console.WriteLine("\nIf you are  developer who changed something and it stopped working,");
            Console.WriteLine("undo your changes.");

            Console.WriteLine("If problems continue, disable or remove any newly installed hardware or software. Disable BIOS memory options such as caching or shadowing.\nIf you need to use safe mode to remove or disable components, restart your computer, save your game saves to save location, and re-install the game or .NET runtime.");

            Console.WriteLine("\nTechnical information:");

            Console.WriteLine("*** STOP: 0x000000FE (0x00000008, 0x00000000666, 0x00000009, 0x847075cc).");
            Console.WriteLine("***   gv3.sys - Address F86B5A89 base at F8685000. DateStamp "+ new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString("X"));
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
