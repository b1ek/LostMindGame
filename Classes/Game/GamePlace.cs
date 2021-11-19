using LostMind.Classes.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.Game {
    public struct GamePlace {
        public Viewport Viewport;
        public string Name;
        public string NameId;
        public int Id;
        public ConsoleColor Background;
        public ConsoleColor Foreground;
        public GamePlace(Viewport viewport = null, string name = "", string nameID = "", int id = -1, ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.White) {
            Viewport = viewport;
            Name = name;
            NameId = nameID;
            Id = id;
            Background = bg;
            Foreground = fg;
        }
        public void Display() {
            Viewport.FillColor(Background, Foreground);
            Viewport.DrawElements();
        }
        public void Update() {
            Viewport.Update();
        }
        public bool IsEmpty => Viewport == null;
        public static GamePlace Empty => new GamePlace(null);
    }
}
