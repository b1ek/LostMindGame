using LostMind.Engine.Animation;
using LostMind.Engine.Config;
using LostMind.Engine.User;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LostMind.Game.Game {
    public class GameRunner {
        RegistryConfig conf = new RegistryConfig(@"SOFTWARE\b1ek\LostMind");

        public void Run() {
            Console.CursorVisible = false;
            Console.ResetColor(); Console.Clear();
            UCO.TrySetSize(120, 30);
            UserConsoleNativeInterface.setFont("system", 1, 10, -1);
            AppDomain.CurrentDomain.UnhandledException += BSOD;
            throw new Exception();
        }

        private void BSOD(object sender, UnhandledExceptionEventArgs e) {
            UserConsoleNativeInterface.setFont("Lucida Console", 3, 9, 1);
            var exc = ((Exception)e.ExceptionObject);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            

        }
    }
}
