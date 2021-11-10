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
        string _hprefx = "= ";
        string _btnTxt = "";
        public string prefix {
            get { return _prefix; }
            set { _prefix = value; innerText = _prefix + _btnTxt; }
        }
        public string buttonText {
            get { return _btnTxt; }
            set { _btnTxt = value; innerText = _prefix + _btnTxt; }
        }
        public List<Action> lazyHandlers = new List<Action>();
        public UIButton(string btnText) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White) {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = _prefix + btnText;
        }
        public UIButton(string btnText, Action onclick) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White)
        {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = _prefix + btnText;
            lazyHandlers.Add(onclick);
            OnClick += procLazyHandlers;
        }

        void procLazyHandlers() {
            foreach( var method in lazyHandlers ) {
                method?.Invoke();
            }
        }

        internal bool _beingPressed = false;

        public override void hover(bool hovered) {
            currentlyHovered = hovered;
            if (hovered) {
                innerText = _hprefx + buttonText;
                User.UCO.WriteXY(_x, _y, _intxt, colors.hoverBackground, colors.hoverForeground);
            } else {
                innerText = _prefix + buttonText;
                User.UCO.WriteXY(_x, _y, _intxt, colors.background, colors.foreground);
            }
        }

    }
}
