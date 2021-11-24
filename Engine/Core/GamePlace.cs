using LostMind.Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Core {
    class GamePlace : IDisposable {
        public string Id;
        Viewport view;
        public GamePlace(string id) {
            Id = id;
        }

        public void Dispose() {
            view.breakMainLoop();
            view.close();
        }
    }
}
