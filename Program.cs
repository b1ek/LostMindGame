using System;
using System.Runtime.InteropServices;
using LostMind.Classes.User;
using System.Diagnostics;
using LostMind.Classes.GameController;
using LostMind.Classes.UI;
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

        static string locale = "en-US";
        /**<summary>Current localization of the program.</summary>*/
        public static Classes.Localization.Localization.Localizations localization = Classes.Localization.Localization.getLocale(locale);

        /**<summary>Game controller.</summary>*/
        public static GameController gameController = new GameController();

        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        internal const UInt32 SC_CLOSE = 0xF060;
        internal const UInt32 MF_ENABLED = 0x00000000;
        internal const UInt32 MF_GRAYED = 0x00000001;
        internal const UInt32 MF_DISABLED = 0x00000002;
        internal const uint MF_BYCOMMAND = 0x00000000;
        public static void EnableCloseButton(bool bEnabled)
        {
            IntPtr hSystemMenu = GetSystemMenu(GetStdHandle(-11), false);
            EnableMenuItem(hSystemMenu, SC_CLOSE, (uint)(MF_ENABLED | (bEnabled ? MF_ENABLED : MF_GRAYED)));
        }

        /**<summary>
         * Main entry point.
         * </summary>
         */
        static void Main(string[] args) {
            Console.CursorVisible = false;
            UserConsoleOutput.FlushConsole();
            UserConsoleOutput.TrySetSize(120, 30);
            UserConsoleNativeInterface.setFont("Courier New", 8);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            if (RegistryConfig.AllowBSODStyleException) AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            UserKeyInput.InstallHook();
            if (22f/7f != 3.142857f) {
                Console.BackgroundColor = ConsoleColor.DarkRed; Console.Clear();
                Console.WriteLine("WARNING: Your PC may be broken. The calculation result of 22 / 7 is "+ 22f/7f + ", but it should be 3.142857.");
                Console.WriteLine("Anyway, press ESC if you read that and want to proceed.");
                while(true) {
                    if (Console.KeyAvailable) {
                        if (Console.ReadKey().Key == ConsoleKey.Escape) break;
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black; Console.Clear();
            }

            var cp = Console.GetCursorPosition();
            const int animMarginTop = 3;
            const int animMarginLeft = 20;
            if (RegistryConfig.noStartupLogoAnim)
            {
                Animation anim = Animation.createSimple(File.ReadAllText(@"C:\Users\blek\source\repos\MyLifeGame\Resources\Logo.txt"), animMarginLeft, animMarginTop, 1, ConsoleColor.DarkGreen);
                anim.run();
            }
            else
            {
                Animation anim = Animation.createSimple(File.ReadAllText(@"C:\Users\blek\source\repos\MyLifeGame\Resources\Logo.txt"), animMarginLeft, animMarginTop, 32);
                anim.run();
            }
            #region Bootload
            if (!RegistryConfig.startGameWithoutBootloader)
            {
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
                writer.FancyWrite("\n\nPress any key to launch the game...").Wait();
            } else { UserConsoleOutput.WriteXY(animMarginLeft, Console.CursorTop + 2, "Press any key to launch the game..."); }
            #endregion

            UserKeyInput.awaitKeyPress();

            UserConsoleOutput.FlushConsole();
            gameController.startGame();
        }
        static bool _exit = false;
        /**<summary>Method that is called on program exit.</summary>*/
        static void OnProcessExit(object sender, EventArgs e)
        {
            if (_exit)
            {
                Console.ResetColor(); Console.Clear();
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.CursorVisible = true;
                UserKeyInput.stopThread();
                Process.GetCurrentProcess().Kill();
            }
        }

        static bool exceptionHandled = false;
        /**<summary>BSOD exception handler.</summary>*/
        static void UnhandledException(object s, UnhandledExceptionEventArgs e) {
            if (exceptionHandled) Environment.Exit(0x000000FE);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.TreatControlCAsInput = true;
            _ = Task.Run(() => { Beep(1024, int.MaxValue); });
            
            Console.WriteLine("A problem has been detected and the game was shut down to prevent damage to your computer.\n");
            Console.WriteLine(Regex.Replace(e.ExceptionObject.GetType().Name.ToUpper(), "(\\B[A-Z])", " $1") + "\n> " + ((Exception) e.ExceptionObject).Message);
            Console.WriteLine("\nIf this is the first time you've seen this stop error screen, restart your computer.\nIf these screen appears again, follow these steps:\n");
            
            Console.WriteLine("Check to make sure any new mods or updates to the game or runtime was properly installed.");
            Console.WriteLine("If this is a new installation, ask your geek firend about it and he might help,");
            Console.WriteLine("you may need some windows/runtime updates.\n");
            
            Console.WriteLine("If problems continue, disable or remove any newly installed hardware or software. Disable BIOS memory options such as caching or shadowing.\nIf you need to use safe mode to remove or disable components, restart your computer, save your game saves to save location, and re-install the game or .NET runtime.");
            
            Console.WriteLine("\nTechnical information:");

            Console.WriteLine("*** STOP: 0x000000FE (0x00000008, 0x00000000666, 0x00000009, 0x847075cc).");
            Console.WriteLine("***   gv3.sys - Address F86B5A89 base at F8685000. DateStamp "+ new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString("X"));
            Console.WriteLine("*** Press S to display StackTrace");
            while (true) if (Console.KeyAvailable) if (Console.ReadKey(true).Key == ConsoleKey.S) break;
            Console.WriteLine("*** StackTrace: ");
            Console.WriteLine(Environment.StackTrace);
            Console.WriteLine("\nPress any key to exit the program.");
            while (true) if (Console.KeyAvailable) break;
            Environment.Exit(0);
        }
    }
}
