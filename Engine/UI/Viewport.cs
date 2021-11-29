using LostMind.Engine.User;
using LostMind.Engine.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.UI {
    internal struct ViewportData {
        public List<UIElement> elements;
        public int selection;
        public bool breakLoop;
        public DrawOrder order;
        public int distance;
    }
    public enum DrawOrder {
        Horizontal,
        Vertical,
        Absolute
    }
    public class Viewport : IDisposable {
        bool _readOnly;
        #region Position/Dimensions
        int _x, _y, _width, _height;
        #region Accessors
        #region Position
        public int PositionX {
            get => _x;
            set {
                if (_readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionX VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                _x = value;
            }
        }
        public int PositionY {
            get => _y;
            set {
                if (_readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionY VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                _y = value;
            }
        }
        #endregion
        #region Width/height
        public int Width {
            get => _width;
            set {
                if (_readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionX VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                _width = value;
            }
        }
        public int Height {
            get => _height;
            set {
                if (_readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionX VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                _height = value;
            }
        }
        #endregion
        #endregion
        #endregion
        #region Data
        ViewportData data;
        public DrawOrder DrawOrder { get => data.order; set { data.order = value; } }
        public List<UIElement> Elements {
            get => data.elements;
            set {
                if (_readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET Elements VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                data.elements = value;
            }
        }
        public int Selection {
            get => data.selection;
            set {
                if (_readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET Selection VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                data.selection = value;
                Draw();
            }
        }
        #endregion

        /**<summary>
         * Create a new Viewport instance
         * </summary>
         * <param name="distance">Distance between elements in characters.</param>
         * <param name="paramReadonly">Set whether the fields are readonly or not<br/>Note: consider setting it to true in case to make it safer.</param>
         */
        public Viewport(int x, int y, int width, int height, bool paramReadonly = true, int distance = 1) {
            data.elements = new List<UIElement>();
            data.order = DrawOrder.Vertical;
            data.selection = 0;
            data.breakLoop = false;
            data.distance = distance;
            _readOnly = paramReadonly;
            UserKeyInput.KeyPress += ProcessKeyEvent;
        }
        #region Drawing
        public void Draw(bool removeOld = true) {
            Console.CursorVisible = false;
            (int x, int y) cursor = (_x, _y-1);
            if (data.elements.Count == 0) {
                Debug.Fail("Cannot draw viewport with zero elements!");
                return;
            }
            List<int> _widths = new List<int>();
            int[] widths = { };
            int maxwid = 0;
            if (DrawOrder == DrawOrder.Horizontal) {
                for (int i = 0; i < data.elements.Count; i++) {
                    _widths.Add(data.elements[i].innerText.Length);
                }
                widths = _widths.ToArray();
                maxwid = widths.Max();
            }
            for (int i = 0; i < data.elements.Count; i++) {
                switch (data.order) {
                    case DrawOrder.Vertical:
                        cursor.y++;
                        cursor.x = _x;
                        break;
                    case DrawOrder.Horizontal:
                        cursor.x = ((maxwid / 2) - (data.elements[i].innerText.Length / 2)) - (_width / 2);
                        cursor.y++;
                        break;
                    case DrawOrder.Absolute:
                        cursor.x = data.elements[i].PositionX;
                        cursor.y = data.elements[i].PositionY;
                        break;
                }
                data.elements[i].print(cursor.x, cursor.y, removeOld, _width - cursor.x);
            }
            Console.CursorLeft = data.elements[data.selection].PositionX + data.elements[data.selection].innerText.Length - 1;
            Console.CursorTop = data.elements[data.selection].PositionY;
            Console.CursorVisible = true;
        }
        public void DumpArea(ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.White, char chr = ' ') {
            UCO.ClearConsoleArea(_x, _y, _width, _height, bg, fg, chr);
        }
        #endregion
        #region Cursor
        public void MoveCursor(bool up) {
            if (data.elements.Count == 0) return;
            data.elements[data.selection].hover(false);

            if (up) {
                data.selection--;
                if (data.selection < 0) {
                    data.selection++;
                }
            } else {
                data.selection++;
                if (data.selection > data.elements.Count - 1) {
                    data.selection--;
                }
            }

            data.elements[data.selection].hover(true);
            Draw();
        }
        public void SetCursor(int pos, bool unsafe_ = false) {
            if (!unsafe_ && data.elements.Count == 0) return;
            if (unsafe_) {
                data.selection = pos;
                return;
            }
            if (pos > data.elements.Count-1) {
                throw new Exception("Position cannot be more than elements size");
            }
            data.selection = pos;
        }
        public void Click() {
            Debug.WriteLine("Clicked " + data.elements[data.selection].innerText);
            data.elements[data.selection].click();
        }
        #endregion
        #region Loops
        public void Mainloop() {
            Draw();
            if (data.elements.Count == 0) {
                throw new Exception("Cannot start mainloop without elements!");
            }
            UserKeyInput.CallEvent(new ConsoleKeyInfo((char)UISysConfig.UIMoveDownKey_txtIn, UISysConfig.UIMoveDownKey_txtIn, false, false, false));
            UserKeyInput.CallEvent(new ConsoleKeyInfo((char)UISysConfig.UIMoveUpKey_txtIn, UISysConfig.UIMoveUpKey_txtIn, false, false, false));
            while (!data.breakLoop) {
                if (!data.breakLoop) UserKeyInput.IterateLoop();
                else {
                    Debug.WriteLine("Terminating loop...");
                    break;
                }
            }
        }
        public void IterateLoop() => UserKeyInput.IterateLoop();
        public void ProcessKeyEvent(ConsoleKeyInfo key) {
            if (key.Key == UISysConfig.UIEnterKey_txtIn)
                Click();
            if (key.Key == UISysConfig.UIMoveUpKey_txtIn)
                MoveCursor(true);
            if (key.Key == UISysConfig.UIMoveDownKey_txtIn)
                MoveCursor(false);
            return;
        }
        public async Task _StopLoop() {
            data.breakLoop = true;
            await Task.Delay(150);
            data.breakLoop = false;
        }
        public void StopLoop() => _ = _StopLoop();
        #endregion
        #region Memory
        public void AddElement(UIElement element, bool select = false) {
            data.elements.Add(element);
            if (select) {
                data.selection = data.elements.Count;
                Draw();
            }
        }
        public void RemoveElement(UIElement element) {
            data.elements.Remove(element);
        }
        public void RemoveElement(int index) {
            data.elements.RemoveAt(index);
        }
        public void RemoveElement(string elementText) {
            for (int i = 0; i > data.elements.Count; i++) {
                if (data.elements[i].innerText == elementText) {
                    RemoveElement(i);
                    return;
                }
            }
        }

        public void Dispose() {
            StopLoop();
            DumpArea(ConsoleColor.Black, ConsoleColor.White, ' ');
            data.elements.Clear();
        }
        #endregion
    }
}