using LostMind.Classes.User;
using LostMind.Classes.Util;
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
        int _width = 0;
        int _height = 0;
        int _selection = 0;

        /**<summary>Rect height of Viewport rectangle</summary>*/
        public int rectHeight { get => _height; }

        /**<summary>Rect width of Viewport rectangle</summary>*/
        public int rectWidth { get => _width; }

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
        #region margin
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
        #endregion

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
        #region Key event handler
        /**<summary>Key press event handler.</summary>*/
        public void OnKeyPress(ConsoleKeyInfo key) {
            if (_elements == null) return;
            if (_elements.Count == 0) return;
            ConsoleKey k = key.Key;
            if (_elements[_selection].GetType() == typeof(UITextInput)) {
                UITextInput ti = (UITextInput) _elements[_selection];
                ti.addChar(key.KeyChar);
                ti.moveCursor(1);
            }
            foreach (var _key in UISysConfig.UIMoveUpKey)
                if (_key == k) moveCursorUp();
            foreach (var _key in UISysConfig.UIMoveDownKey)
                if (_key == k) moveCursorDown();
            foreach (var _key in UISysConfig.UIEnterKey)
                if (_key == k) clickSelection();
        }
        #endregion
        #region Elements
        /**<summary>Draw viewport elements.</summary>*/
        public void DrawElements() {
            int i = 0;
            foreach (var elem in _elements) {
                elem.remove();
                elem.print(_marginLeft+_x, _marginTop+_y+i);
                i++;
            }
        }

        bool closed = false;
        /**<summary>Close the viewport.</summary>*/
        public void close() {
            foreach (var elem in _elements) elem.remove();
            closed = true;
        }
        
        public void mainloop() {
            while (!closed)
                if (Console.KeyAvailable)
                    UserKeyInput.CallEvent(Console.ReadKey(true));
        }

        public Thread WrapLoopInThread() => new Thread(mainloop);
        public async Task WrapLoopAsync() => await Task.Run(mainloop);
        public LoopThread WrapLoopThread() {
            return new LoopThread(() => {
                if (Console.KeyAvailable)
                    UserKeyInput.CallEvent(Console.ReadKey(true));
            });
        }

        /**<summary>Add element to viewport.</summary>*/
        public void AddElement(UIElement element) {
            if (_elements.Count < maxElements) _elements.Add(element);
            else throw new TooMuchElementsException();
            DrawElements();
            if (_elements.Count == 1) 
                _elements[0].hover(true);
        }
        #endregion
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
        public void Paint(ConsoleColor background, ConsoleColor foreground) {
            for (int i = 0; i > _width; i++)
            {
                UserConsoleOutput.WriteXY(_x, _y + i, new string(' ', _width), background, foreground);
            }
        }
    }
}
