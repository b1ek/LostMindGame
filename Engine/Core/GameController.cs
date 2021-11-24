using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostMind.Engine.Native;

namespace LostMind.Engine.Core {
    class GameController {

        long hiResTimestamp;
        int delta = 0;
        BackgroundWorker worker;
        
        public GameController() {
            worker.DoWork += doWork;
            SafeNativeMethods.QueryPerformanceCounter(out hiResTimestamp);
        }

        void doWork(object sender, DoWorkEventArgs e) {
            long now = 0;
            SafeNativeMethods.QueryPerformanceCounter(out now);
            delta = (int) (now - hiResTimestamp);

        }
    }
}
