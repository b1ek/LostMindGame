using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class UIElement
    {
        private protected int _x;
        private protected int _y;
        public int lastX { get { return _x; } }
        public int lastY { get { return _y; } }
        private protected bool _intrctbl;
        private protected ConsoleColor _hbg;
        private protected ConsoleColor _hfg;
        private protected ConsoleColor _dbg;
        private protected ConsoleColor _dfg;
        private protected ConsoleColor _bg;
        private protected ConsoleColor _fg;
        private protected string _intxt;
        private protected bool currentlyHovered = false;
        bool displayed = false;
        public string innerText {
            get { return _intxt; }
            set { _intxt = value; }
        }
        public UIElement(bool interactable, ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor hoverBgColor, ConsoleColor hoverFgColor, ConsoleColor cmdBgDef, ConsoleColor cmdFgDef) {
            _intrctbl = interactable;
            _hbg = hoverBgColor;
            _hfg = hoverFgColor;
            _dbg = cmdBgDef;
            _dfg = cmdFgDef;
            _bg = bgColor;
            _fg = fgColor;
        }
        public void print(int x, int y) {
            print(x, y, true);
        }
        public void print(int x, int y, bool removeOld) {
            if (removeOld) {
                if (displayed) User.UserConsoleOutput.WriteXY(_x, _y, new string(' ', _intxt.Length), _dbg, _dfg);
                _x = x;
                _y = y;
            }
            User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _bg, _fg);
            displayed = true;
        }
        public void remove() {
            User.UserConsoleOutput.WriteXY(_x, _y, new string(' ', _intxt.Length), _dbg, _dfg);
            displayed = false;
        }

        private protected ConsoleColor _crrntbg; private protected ConsoleColor _crrntfg;
        public ConsoleColor bg {
            get { return _crrntbg; }
            set {
                User.UserConsoleOutput.WriteXY(_x, _y, _intxt, value, _crrntfg);
                _crrntbg = value;
            }
        } public ConsoleColor txt {
            get { return _crrntfg; }
            set {
                User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _crrntbg, value);
                _crrntfg = value;
            }
        }

        public virtual void hover(bool hovered) {
            if (_intrctbl) { currentlyHovered = hovered;
                if (hovered) {
                    User.UserConsoleOutput.WriteXY(_x, _y, _intxt);
                    bg = _hbg;
                    txt = _hfg;
                } else {
                    User.UserConsoleOutput.WriteXY(_x, _y, _intxt);
                    bg = _bg;
                    txt = _fg;
                }
            }else
            throw new Exception("Element not interactable");
        }

        public void click() {
            OnClick?.Invoke();
        }

        public delegate void OnClickEvent();
        public event OnClickEvent OnClick;


    }
}
