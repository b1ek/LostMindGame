using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class UIElement
    {
        internal int _x;
        internal int _y;
        internal bool _intrctbl;
        internal ConsoleColor _hbg;
        internal ConsoleColor _hfg;
        internal ConsoleColor _dbg;
        internal ConsoleColor _dfg;
        internal ConsoleColor _bg;
        internal ConsoleColor _fg;
        internal string _intxt;
        public string innerText {
            get { return _intxt; }
            set {
                _intxt = value;
            }
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
            User.UserConsoleOutput.WriteXY(_x, _y, new string(' ', _intxt.Length), _dbg, _dfg); _x = x; _y = y;
            User.UserConsoleOutput.WriteXY(x, y, _intxt, _bg, _fg);
        }
        public void print(int x, int y, bool removeOld)
        {
            if (removeOld) { User.UserConsoleOutput.WriteXY(_x, _y, new string(' ', _intxt.Length), _dbg, _dfg); _x = x; _y = y; }
            User.UserConsoleOutput.WriteXY(x, y, _intxt, _bg, _fg);
        }
        public void hover(bool hovered) {
            if (_intrctbl) {
                if (hovered) {
                    User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _hbg, _hfg);
                } else {
                    User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _bg, _fg);
                }
            }
            throw new Exception("Element not interactable");
        }

    }
}
