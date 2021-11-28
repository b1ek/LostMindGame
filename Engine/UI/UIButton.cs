using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.UI
{
    public class UIButton : UIElement
    {
        string _prefix = "[]";
        string _hprefix = "[]";
        string _btnTxt = "";
        public string prefix {
            get => _prefix;
            set {
                _prefix = value;
                innerText = value[0] + buttonText + value[1];
            }
        }
        public string buttonText {
            get => _btnTxt;
            set {
                _btnTxt = value;
                innerText = prefix[0] + value + prefix[1];
            }
        }
        public override void hover(bool hovered) {
            currentlyHovered = hovered;
            if (currentlyHovered) {
                _crrntbg = colors.hoverBackground;
                _crrntfg = colors.hoverForeground;
                prefix = _hprefix;
            } else {
                _crrntbg = colors.defaultBackground;
                _crrntfg = colors.defaultForeground;
                prefix = _prefix;
            }
        }
        public List<Action> lazyHandlers = new List<Action>();
        public UIButton(string btnText                ) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.White) {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = prefix[0] + buttonText + prefix[1];
        }
        public UIButton(string btnText, Action onclick) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Black, ConsoleColor.White)
        {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = prefix[0] + buttonText + prefix[1];
            lazyHandlers.Add(onclick);
            OnClick += procLazyHandlers;
        }
        void procLazyHandlers() {
            foreach( var method in lazyHandlers ) {
                method?.Invoke();
            }
        }
        public override void print(int x, int y, bool removeOld = true, int maxLen = -1) {
            if (removeOld) {
                User.UCO.WriteXY(PositionX, PositionY, new string(' ', innerText.Length));
            }
            innerText = prefix[0] + buttonText + prefix[1];
            var _prn = innerText;
            if (maxLen > 0) {
                var intxt = Utils.SplitByCount(_btnTxt, maxLen).First();
                _prn = prefix[0] + intxt + prefix[1];
            }
            User.UCO.WriteXY(x, y, _prn, _crrntbg, _crrntfg, false);
            PositionX = x;
            PositionY = y;
        }
        internal bool _beingPressed = false;
    }
}
