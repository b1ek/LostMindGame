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
        int selection = 0;
        List<UIElement> _elements = new List<UIElement>();
        public List<UIElement> Elements { get { return _elements; } }
        public const int maxElements = 256;
        int _marginLeft;
        int _marginTop;
        public int marginLeft { get { return _marginLeft; }
            set {
                _marginLeft = value;
            }
        }
        public int marginTop { get { return _marginTop; }
            set {
                _marginTop = value;
            }
        }
        Thread _loop;
        public Task loop {
            get { return loop; }
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
        public void clickSelection() => _elements[selection].click();
        public void moveCursorUp() {
            if (selection - 1 < _elements.Count) {
                if (selection == 0) { }
                else { _elements[selection].hover(false); selection--; _elements[selection].hover(true); }
                Console.SetCursorPosition(_elements[selection].lastX, _elements[selection].lastY);
            }
        }
        public void moveCursorDown() {
            if (selection + 1 < _elements.Count) {
                if (selection == _elements.Count) { }
                else { _elements[selection].hover(false); selection++; _elements[selection].hover(true); }
                Console.SetCursorPosition(_elements[selection].lastX, _elements[selection].lastY);
            }
        }
        #endregion
    }
}
