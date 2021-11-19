using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.UI {
    public class UILabel : UIElement {
        string val;
        public string text { get => val; set { val = value; } }
        public UILabel(string value) : base(false) {
            val = value;
            innerText = val;
        }
        // this is the most simpliest UI class in the project
        // это самый простой класс для интерфейса в проекте
    }
}
