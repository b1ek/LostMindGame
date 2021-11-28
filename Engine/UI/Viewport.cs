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

        public void Draw() {
            Console.CursorVisible = false;
            (int x, int y) cursor = (_x, _y);
            if (data.elements.Count == 0) {
                Debug.Fail("Cannot draw viewport with zero elements!");
                return;
            }
            for (int i = 0; i < data.elements.Count; i++) {
                switch (data.order) {
                    case DrawOrder.Vertical:
                        cursor.y++;
                        cursor.x = _x;
                        break;
                    case DrawOrder.Horizontal:
                        if (cursor.x + data.elements[i].innerText.Length > _width) {
                            cursor.y++;
                            cursor.x = _x + data.elements[i].innerText.Length;
                        }
                        cursor.x += data.elements[i].innerText.Length;
                        cursor.x += data.distance;
                        break;
                    case DrawOrder.Absolute:
                        cursor.x = data.elements[i].PositionX;
                        cursor.y = data.elements[i].PositionY;
                        break;
                }
                data.elements[i].print(cursor.x, cursor.y, true, cursor.x - _width);
            }
            Console.CursorLeft = data.elements[data.selection].PositionX + data.elements[data.selection].innerText.Length - 1;
            Console.CursorTop = data.elements[data.selection].PositionY;
            Console.CursorVisible = true;
        }
        public void Dispose() {
            for (int i = 0; i > data.elements.Count; i++) {
                data.elements[i].remove();
            }
            data.elements.Clear();
        }
        public void AddElement(UIElement element, bool select = false) {
            data.elements.Add(element);
            if (select) {
                data.selection = data.elements.Count;
                Draw();
            }
        }
        public void IterateLoop() {
            UserKeyInput.IterateLoop();
        }
        public void ProcessKeyEvent(ConsoleKeyInfo key) {
            if (key.Key == UISysConfig.UIEnterKey_txtIn)
                Click();
            if (key.Key == UISysConfig.UIMoveUpKey_txtIn)
                MoveCursor(true);
            if (key.Key == UISysConfig.UIMoveDownKey_txtIn)
                MoveCursor(false);
            return;
        }
        public void MoveCursor(bool up) {
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
        public void Mainloop() {
            Draw();
            if (data.elements.Count == 0) {
                throw new Exception("Cannot start mainloop without elements!");
            }
            while (!data.breakLoop) {
                UserKeyInput.IterateLoop();
            }
        }
        public void DumpArea() {
            UCO.ClearConsoleArea(_x, _y, _width, _height);
        }
    }
}