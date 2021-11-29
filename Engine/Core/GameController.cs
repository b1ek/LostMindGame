using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LostMind.Engine.Native;

namespace LostMind.Engine.Core {
    public class GameController {
        public static GameController CreateNew() {
            return new GameController();
        }

        long hiResTimestamp;
        int delta = 0;
        public int DeltaTime => delta;
        BackgroundWorker worker = new BackgroundWorker();

        private GameController() {
            worker.DoWork += doWork;
            SafeNativeMethods.QueryPerformanceCounter(out hiResTimestamp);
            worker.RunWorkerAsync();
        }
        void doWork(object sender, DoWorkEventArgs e) {
            long now = 0;
            SafeNativeMethods.QueryPerformanceCounter(out now);
            delta = (int) (now - hiResTimestamp);
            hiResTimestamp = now;
        }

    }
}
