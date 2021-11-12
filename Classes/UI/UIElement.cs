using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI {
    public class UIElement {
        #region Position
        private protected int _x; // x position
        private protected int _y; // y position
        public int lastX { get { return _x; } }
        public int lastY { get { return _y; } }
        #endregion

        /**<summary>Determinies if this form using user text(chars) input.</summary>**/
        private protected bool useTxtInput = false;

        #region Colors
        public struct ElementColors {
            public ConsoleColor hoverBackground;
            public ConsoleColor hoverForeground;
            public ConsoleColor defaultBackground;
            public ConsoleColor defaultForeground;
            public ConsoleColor background;
            public ConsoleColor foreground;
        }
        private protected ElementColors colors;
        #endregion
        #region Accessors
        public bool Interactable => _intrctbl;
        public bool isDisplayed => displayed;
        public bool usesTextInput => useTxtInput;
        public string innerText {
            get { return _intxt; }
            set { _intxt = value; }
        }
        #endregion
        #region Constructors + overload
        public UIElement(bool interactable, ConsoleColor bgColor, ConsoleColor fgColor, ConsoleColor hoverBgColor, ConsoleColor hoverFgColor, ConsoleColor cmdBgDef, ConsoleColor cmdFgDef) {
            _intrctbl = interactable;
            colors.background        = bgColor;
            colors.foreground        = fgColor;
            colors.hoverBackground   = hoverBgColor;
            colors.hoverForeground   = hoverFgColor;
            colors.defaultBackground = cmdBgDef;
            colors.defaultForeground = cmdFgDef;
        }
        public UIElement(bool interactable) {
            _intrctbl = interactable;
            colors.background        = ConsoleColor.Black;
            colors.foreground        = ConsoleColor.White;
            colors.hoverBackground   = ConsoleColor.Gray;
            colors.hoverForeground   = ConsoleColor.White;
            colors.defaultBackground = ConsoleColor.Black;
            colors.defaultForeground = ConsoleColor.White;
        }
        #endregion
        #region Painting
        /**<summary>Element displayed or not (print, remove)</summary>*/
        bool displayed = false; /**<summary>Element interactable or not</summary>*/
        private protected bool _intrctbl; /**<summary>Inner text</summary>*/
        private protected string _intxt; /**<summary>Element hovered or not</summary>*/
        private protected bool currentlyHovered = false; /**<summary>Print element to coordinates</summary>*/


        public virtual void print(int x, int y) {
            print(x, y, true);
        }
        public virtual void print(int x, int y, bool removeOld) {
            if (removeOld) {
                if (displayed) User.UCO.WriteXY(_x, _y, new string(' ', _intxt.Length), colors.defaultBackground, colors.defaultForeground);
                _x = x;
                _y = y;
            }
            User.UCO.WriteXY(_x, _y, _intxt, colors.background, colors.foreground);
            displayed = true;
        }
        public virtual void remove() {
            if (displayed) User.UCO.WriteXY(_x, _y, new string(' ', _intxt.Length), colors.defaultBackground, colors.defaultForeground);
            displayed = false;
        }
        public virtual void hover(bool hovered) {
            if (_intrctbl) { currentlyHovered = hovered;
                if (hovered) {
                    bg = colors.hoverBackground;
                    txt = colors.hoverForeground;
                } else {
                    bg = colors.defaultBackground;
                    txt = colors.defaultForeground;
                }
            }
        }

        private protected ConsoleColor _crrntbg; private protected ConsoleColor _crrntfg;
        public ConsoleColor bg {
            get { return _crrntbg; }
            set {
                User.UCO.WriteXY(_x, _y, _intxt, value, _crrntfg);
                _crrntbg = value;
            }
        } public ConsoleColor txt {
            get { return _crrntfg; }
            set {
                User.UCO.WriteXY(_x, _y, _intxt, _crrntbg, value);
                _crrntfg = value;
            }
        }
        #endregion

        public virtual void click() {
            OnClick?.Invoke();
        }
        public delegate void OnClickEvent();
        public event OnClickEvent OnClick;


    }
}
