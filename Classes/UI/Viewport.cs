using LostMind.Classes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class TooMuchElementsException : Exception {}
    public class Viewport {
        int _x = 0;
        int _y = 0;
        int _selection = 0;

        /**<summary>Start X point of Viewport</summary>*/
        public int rectX { get => _x; }

        /**<summary>Start Y point of Viewport</summary>*/
        public int rectY { get => _y; }

        /**<summary>Returns current selection</summary>*/
        public int selection { get => _selection; }

        List<UIElement> _elements = new List<UIElement>();
        public List<UIElement> Elements { get => _elements; }

        /**<summary>Max elements limiter</summary>*/
        public const int maxElements = 128;

        int _marginLeft;
        int _marginTop;

        /**<summary>Left margin of elements, css-like</summary>*/
        public int marginLeft { get { return _marginLeft; }
            set {
                _marginLeft = value;
            }
        }

        /**<summary>Top margin of elements, css-like</summary>*/
        public int marginTop { get { return _marginTop; }
            set {
                _marginTop = value;
            }
        }

        /**
         * <summary>
         * Crete new viewport
         * </summary>
         * <param name="x">The left x point</param>
         * <param name="y">The left y point</param>
         * <param name="width">Width of rectangle</param>
         * <param name="height">Height of rectangle</param>
         */
        public Viewport(int x, int y, int width, int height) {
            _x = x;
            _y = y;

            for (int i = 0; i > height; i++) {
                UserConsoleOutput.WriteXY(x, y + i, new string(' ', width), ConsoleColor.DarkRed, ConsoleColor.White);
            }
            UserKeyInput.KeyPress += OnKeyPress;
        }

        /**<summary>Key press event handler.</summary>*/
        public void OnKeyPress(ConsoleKeyInfo key) {
            ConsoleKey k = key.Key;
            foreach (var _key in UISysConfig.UIMoveUpKey)
                if (_key == k) moveCursorUp();
            foreach (var _key in UISysConfig.UIMoveDownKey)
                if (_key == k) moveCursorDown();
            foreach (var _key in UISysConfig.UIEnterKey)
                if (_key == k) clickSelection();
        }

        /**<summary>Draw viewport elements.</summary>*/
        public void DrawElements() {
            int i = 0;
            foreach (var elem in _elements) {
                elem.print(_marginLeft+_x, _marginTop+_y+i);
                i++;
            }
        }

        /**<summary>Add element to viewport.</summary>*/
        public void AddElement(UIElement element) {
            if (_elements.Count < maxElements) _elements.Add(element);
            else throw new TooMuchElementsException();
            element.print(_marginLeft + _x, _marginTop + _y + Elements.Count);
            if (_elements.Count == 1) {
                _elements[0].hover(true);
            }
        }

        #region Cursor
        public void clickSelection() => _elements[_selection].click();
        public void moveCursorUp() {
            if (_selection - 1 < _elements.Count) {
                if (_selection == 0) { }
                else { _elements[_selection].hover(false); _selection--; _elements[_selection].hover(true); }
                Console.SetCursorPosition(_elements[_selection].lastX, _elements[_selection].lastY);
            }
        }
        public void moveCursorDown() {
            if (_selection + 1 < _elements.Count) {
                if (_selection == _elements.Count) { }
                else { _elements[_selection].hover(false); _selection++; _elements[_selection].hover(true); }
                Console.SetCursorPosition(_elements[_selection].lastX, _elements[_selection].lastY);
            }
        }
        #endregion
    }
}
