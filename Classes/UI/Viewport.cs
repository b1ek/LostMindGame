using LostMind.Classes.User;
using LostMind.Classes.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.UI {
    public class TooMuchElementsException : Exception { }
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

        public bool breakLoop = false;

        /**<summary>Max elements limiter</summary>*/
        public const int maxElements = 128;
        #region margin
        int _marginLeft;
        int _marginTop;

        /**<summary>Left margin of elements, css-like</summary>*/
        public int marginLeft {
            get { return _marginLeft; }
            set {
                _marginLeft = value;
            }
        }

        /**<summary>Top margin of elements, css-like</summary>*/
        public int marginTop {
            get { return _marginTop; }
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
                UCO.WriteXY(x, y + i, new string(' ', width), ConsoleColor.DarkRed, ConsoleColor.White);
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
                UITextInput ti = (UITextInput)_elements[_selection];
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
                elem.print(_marginLeft + _x, _marginTop + _y + i);
                i++;
            }
        }

        bool closed = false;
        /**<summary>Close the viewport.</summary>*/
        public void close() {
            foreach (var elem in _elements) elem.remove();
            closed = true;
        }

        public void breakMainLoop() {
            _ = Task.Run(() => {
                breakLoop = true;
                Task.Delay(32);
                breakLoop = false;
            });
        }

        public void mainloop() {
            breakLoop = false;
            while (!closed) {
                if (breakLoop) break;
                if (Console.KeyAvailable)
                    UserKeyInput.CallEvent(Console.ReadKey(true));
            }
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
            if (_elements.Count == 1 && _elements[0].Interactable)
                _elements[0].hover(true);
        }

        public void RemoveElement(int index) {
            _elements[index].remove();
            _elements.RemoveAt(index);
        }

        public bool TryRemoveElement(int index) {
            try {
                RemoveElement(index);
            } catch (Exception e) {
                return false;
                Debug.WriteLine("Got an exception: " + e.GetType().Name + "\nMessage: " + e.Message + "\nStackTrace: \n" + e.StackTrace);
            }
            return true;
        }
        public void RemoveAllElements() {
            foreach (var item in _elements)
                item.remove();
            _elements.RemoveRange(0, _elements.Count);
        }
        #endregion
        #region Cursor
        public void clickSelection() {
            if (_elements[selection].Interactable) _elements[_selection].click();
        }
        /**<summary>Moves cursor up from the current position.\nBenchmark results: 1 ms (~10000 ticks) at 3.2 GHz clock CPU.</summary>*/
        public void moveCursorUp() {
            if (_selection - 1 < _elements.Count) {

                if (_selection == 0) { } else {
                    int i = selection;
                    while (true) {
                        i--;
                        if (i < 0) break;
                        if (_elements[i].Interactable) {
                            _elements[_selection].hover(false); _selection = i; _elements[_selection].hover(true);
                            break;
                        }
                    }
                }
            }
        }
        /**<summary>Moves cursor down from the current position.\nBenchmark results: 1 ms (~10000 ticks) at 3.2 GHz clock CPU.</summary>*/
        public void moveCursorDown() {
            if (_selection + 1 < _elements.Count) {
                if (_selection == _elements.Count) { } else {
                    int i = selection;
                    while (true) {
                        i++;
                        if (i > _elements.Count - 1) break;
                        if (_elements[i].Interactable) {
                            _elements[_selection].hover(false); _selection = i; _elements[_selection].hover(true);
                            break;
                        }
                    }
                }
            }
        }
        #endregion
        public void Paint(ConsoleColor background, ConsoleColor foreground) {
            for (int i = 0; i > _width; i++) {
                UCO.WriteXY(_x, _y + i, new string(' ', _width), background, foreground);
            }
        }
    }
}