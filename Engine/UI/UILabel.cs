using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.UI {
    public class UILabel : UIElement {
        string val;
        ConsoleColor _clr;
        public ConsoleColor Color { get => _clr; set {
                _clr = value;
                colors.foreground = value;
                colors.defaultForeground = value;
                colors.hoverForeground = value;
            }
        }
        public string text { get => val; set { val = value; } }
        public UILabel(string value, ConsoleColor textColor = ConsoleColor.White) : base(false) {
            val = value;
            innerText = val;
            Color = textColor;
        }
        // this is the most simpliest UI class in the project
        // это самый простой класс для интерфейса в проекте
    }
}
