using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.Core {
    class Core {
        GameController.GameController controller;
        BackgroundWorker ControlsWorker;
        CancellationTokenSource sauce;
        public const string version  = "1__0.1.3 INDEV";
        public const string codename = "MATRIX_MIX";
        public void Run() {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Running LostEngine version " + version);
            Console.WriteLine("Codename: " + codename);
            Console.TreatControlCAsInput = true;
            var workerPack = User.UserKeyInput.CreateWorker();
            ControlsWorker = workerPack.worker;
            sauce = workerPack.cancellation;
            User.UserKeyInput.KeyPress += (key) => { Console.Write($"{(int)key.Key}, "); };
            d: goto d;
        }
    }
}
