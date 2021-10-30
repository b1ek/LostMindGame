using System;
using System.Threading;
using System.Runtime.InteropServices;
using LostMind.Classes.User;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LostMind.Classes.Animation;
using System.Diagnostics;
using LostMind.Classes.GameController;

namespace LostMind
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        static string locale = "en-US";
        public static Classes.Localization.Localization.Localizations localization = Classes.Localization.Localization.getLocale(locale);
        public static GameController gameController = new GameController();

        static void Main(string[] args) {
            #region a
            /*
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
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
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
            writer.fancyWrite("\n\nPress any key to launch the game...").Wait();

            UserKeyInput.awaitKeyPress();
            */
            #endregion
            gameController.startGame();
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.SetCursorPosition(0, Console.CursorTop+3);
            Console.CursorVisible = true;
        }

        static void UnhandledException(object s, UnhandledExceptionEventArgs e) {
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.TreatControlCAsInput = true;
            //new Thread(() => { while (true) {Beep(Classes.Util.RandomGen.getInt(1023, 1024), 1); } }).Start();
            
            Console.WriteLine("A problem has been detected and the game was shut down to prevent damage to your computer.\n");
            Console.WriteLine(e.ExceptionObject.GetType().Name.ToUpper());
            Console.WriteLine("\nIf this is the first time you've seen this stop error screen, restart your computer.\nIf these screen appears again, follow these steps:\n");
            
            Console.WriteLine("Check to make sure any new mods or updates to the game or runtime was properly installed.");
            Console.WriteLine("If this is a new installation, ask your geek firend about it and he might help,");
            Console.WriteLine("you may need some windows/runtime updates.\n");
            
            Console.WriteLine("If problems continue, disable or remove any newly installed hardware or software. Disable BIOS memory options such as caching or shadowing.\nIf you need to use safe mode to remove or disable components, restart your computer, save your game saves to save location, and re-install the game or .NET runtime.");
            
            Console.WriteLine("\nTechnical information:");

            Console.WriteLine("*** STOP: 0x000000FE (0x00000008, 0x00000000666, 0x00000009, 0x847075cc).");
            Console.WriteLine("***   gv3.sys - Address F86B5A89 base at F8685000. DateStamp "+ new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString("X"));
            Console.WriteLine("*** StackTrace: ");
            Console.WriteLine(Environment.StackTrace);
            Console.WriteLine("\nPress ESC to exit the program.");
            while (true) {
                if (Console.KeyAvailable) if (Console.ReadKey().Key == ConsoleKey.Escape)
                        Environment.Exit(0x000000FE);
            }

        }
    }
}
