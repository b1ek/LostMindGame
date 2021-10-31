using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class UIButton : UIElement
    {
        public UIButton(string btnText, ConsoleColor bgClr, ConsoleColor txtClr, ConsoleColor hoveredBgClr, ConsoleColor hoveredTxtClr) : base(true, bgClr, txtClr, hoveredBgClr, hoveredTxtClr, bgClr, txtClr) {
            innerText = btnText;
        }

        public override void hover(bool hovered) {
            currentlyHovered = hovered;
            if (hovered) {
                User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _hbg, _hfg);
            } else {
                User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _bg, _fg);
            }
        }

        public event EventHandler OnButtonClick;
        public void processKeyEvent(ConsoleKey key) {

        }
    }
}
