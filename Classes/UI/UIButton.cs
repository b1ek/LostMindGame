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

        public event EventHandler OnButtonClick;
    }
}
