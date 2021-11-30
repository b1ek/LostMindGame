using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.Core {
    public class GameController {
        public static GameController CreateNew() {
            return new GameController();
        }

        long hiResTimestamp;
        int delta = 0;
        public int DeltaTime => delta;
        BackgroundWorker worker = new BackgroundWorker();

        private GameController(GamePlace startPlace) {
            worker.DoWork += doWork;
            worker.RunWorkerAsync();
            places.Add(startPlace);
        }
        void doWork(object sender, DoWorkEventArgs e) {
            
        }

        public List<GamePlace> Places { get => places; set { places = value; } }
        List<GamePlace> places = new List<GamePlace>();
        int currentPlaceIndex = 0;
    }
}
