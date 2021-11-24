using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            User.UserConsoleNativeInterface.setFont("Lucida Console", 100);
            Console.TreatControlCAsInput = true;
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
                User.UserNativeLib.printToXY(new string(' ', Console.BufferWidth), 0, cy);
                User.UserNativeLib.printToXY($"This console will be gone in \x1b[36m{(double)(int)(c * 10) / 10}\x1b[0m seconds...", 0, cy);
            }
            Console.ResetColor();
            Console.Clear();
            //controller = new Game.GameController();
            gameRunner = new LostMind.Game.Game.GameRunner();
            gameRunner.Run();

        }
    }
}
