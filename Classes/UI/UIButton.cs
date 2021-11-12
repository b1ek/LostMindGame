using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class UIButton : UIElement
    {
        string _prefix = "[]";
        string _hprefix = "{}";
        string _btnTxt = "";
        public string prefix {
            get => prefix;
            set {
                prefix = value;
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
                prefix = _hprefix;
            } else {
                prefix = _prefix;
            }
        }
        public List<Action> lazyHandlers = new List<Action>();
        public UIButton(string btnText) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White) {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = prefix[0] + buttonText + prefix[1];
        }
        public UIButton(string btnText, Action onclick) : base(true, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.White)
        {
            btnText = btnText.First().ToString().ToUpper() + string.Join("", btnText.Skip(1));
            _btnTxt = btnText;
            innerText = prefix[0] + buttonText + prefix[1];
            lazyHandlers.Add(onclick);
            OnClick += procLazyHandlers;
        }
        public void SetMarginleft() {

        }

        void procLazyHandlers() {
            foreach( var method in lazyHandlers ) {
                method?.Invoke();
            }
        }

        internal bool _beingPressed = false;


    }
}
