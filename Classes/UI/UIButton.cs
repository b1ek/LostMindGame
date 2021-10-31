using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class UIButton : UIElement
    {
        string _prefix = "> ";
        string _btnTxt = "";
        public string prefix {
            get { return _prefix; }
            set { _prefix = value; innerText = _prefix + _btnTxt; }
        }
        public string buttonText {
            get { return _btnTxt; }
            set { _btnTxt = value; innerText = _prefix + _btnTxt; }
        }
        public UIButton(string btnText) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White) {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = _prefix + btnText;
            OnButtonClick += eventHandler;
        }

        internal bool _beingPressed = false;

        public override void hover(bool hovered) {
            currentlyHovered = hovered;
            if (hovered) {
                User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _hbg, _hfg);
            } else {
                User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _bg, _fg);
            }
        }

        /**
         <summary>Calls when user selects the button ans hits Enter/E/Spacebar</summary>
         */
        public event EventHandler OnButtonClick;
        /**
         <summary>Calls when user stop selecting the button ans hit Enter/E/Spacebar</summary>
         */
        public event EventHandler OnButtonUnClick;
        public void processKeyEvent(ConsoleKey key) {
            if (UISysConfig.UIEnterKey.Contains(key)) {
                this.OnButtonClick?.Invoke(this, new EventArgs());
                _beingPressed = true;
            } else {
                this.OnButtonUnClick?.Invoke(this, new EventArgs());
                _beingPressed = false;
            }
        }

        internal void eventHandler(object s, EventArgs e) {
            if (_beingPressed) {
                
            }
        }

    }
}
