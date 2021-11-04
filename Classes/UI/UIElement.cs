﻿using System;
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

        private protected bool _intrctbl; // interactable

        private protected ConsoleColor _hbg; // hover background
        private protected ConsoleColor _hfg; //       foreground
        private protected ConsoleColor _dbg; // default background
        private protected ConsoleColor _dfg; //         foreground
        private protected ConsoleColor _bg; // background
        private protected ConsoleColor _fg; // foreground

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
        public UIElement(bool interactable) {
            _intrctbl = interactable;
            _hbg = ConsoleColor.Gray;
            _hfg = ConsoleColor.White;
            _dbg = ConsoleColor.Black;
            _dfg = ConsoleColor.White;
            _bg = ConsoleColor.Black;
            _fg = ConsoleColor.White;
        }
        public virtual void print(int x, int y) {
            print(x, y, true);
        }
        public virtual void print(int x, int y, bool removeOld) {
            if (removeOld) {
                if (displayed) User.UserConsoleOutput.WriteXY(_x, _y, new string(' ', _intxt.Length), _dbg, _dfg);
                _x = x;
                _y = y;
            }
            User.UserConsoleOutput.WriteXY(_x, _y, _intxt, _bg, _fg);
            displayed = true;
        }
        public virtual void remove() {
            if (displayed) User.UserConsoleOutput.WriteXY(_x, _y, new string(' ', _intxt.Length), _dbg, _dfg);
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
                    bg = _hbg;
                    txt = _hfg;
                } else {
                    bg = _bg;
                    txt = _fg;
                }
            }else
            throw new Exception("Element not interactable");
        }

        public virtual void click() {
            OnClick?.Invoke();
        }

        public delegate void OnClickEvent();
        public event OnClickEvent OnClick;


    }
}
