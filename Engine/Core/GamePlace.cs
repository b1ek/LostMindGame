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

        private event EventHandler onDispose;

        public virtual void Start() { }
        public virtual void Update() { }
        public void Dispose() {
            onDispose?.Invoke(this, new EventArgs());
            //view.breakMainLoop();
            //view.close();
        }
    }
}
