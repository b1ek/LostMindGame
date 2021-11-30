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


    #region Other stuff
    internal struct ViewportData {
        public List<UIElement> elements;
        public int selection;
        public bool breakLoop;
        public DrawOrder order;
    }
    public struct ViewportSettings {
        public int inlineSpace;
        public int x;
        public int y;
        public int width;
        public int height;
        bool ridOnly;
        bool createdThruConstructor;
        public bool readOnly {
            get {
                if (createdThruConstructor) return ridOnly;
                else return true;
            }
        }
        public ViewportSettings(int inlSpace, int positionX, int positionY, int width_, int height_, bool readOnly_) {
            inlineSpace = inlSpace;
            x = positionX;
            y = positionY;
            width = width_;
            height = height_;
            ridOnly = readOnly_;
            createdThruConstructor = true;
        }
    }
    public struct ViewportMargins {
        public int marginLeft;
        public int marginRight;
        public int marginTop;
        public int marginBottom;
    }
    public enum DrawOrder {
        /**<summary></summary>*/
        /**<summary>Draw in the vertical layout, with centering(makes the best look!)</summary>*/
        VerticalCenter,
        /**<summary>Draw in the vertical layout, without centering each element</summary>*/
        VerticalNoCent,
        /**<summary>Draw in inline order, just like this: <br/><code>[ELEM1] [ELEM2] [ELEM3]</code></summary>*/
        Inline,
        InlineNoBreak,
        /**<summary>Draw in horizontal layout, just like this:<br/><code>[ELEM1]<br/>[ELEM2]<br/>[ELEM3]</code></summary>*/
        Horizontal,
        /**<summary>Be careful! Make sure to set every singe element coordinates by using ELEMENT.PositionX/Y = ...</summary>*/
        Absolute
    }
    #endregion
    
    
    public class Viewport : IDisposable {
        // Position/Dimensions
        public int PositionX {
            get => settings.x;
            set {
                if (settings.readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionX VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                settings.x = value;
            }
        }
        public int PositionY {
            get => settings.y;
            set {
                if (settings.readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionY VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                settings.y = value;
            }
        }
        public int Width {
            get => settings.width;
            set {
                if (settings.readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionX VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                settings.width = value;
            }
        }
        public int Height {
            get => settings.width;
            set {
                if (settings.readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET PositionX VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                settings.width = value;
            }
        }
        ViewportMargins margins;
        public ViewportMargins Margins { get => margins; set { margins = value; Draw(); } }
                
        
        // Data
        ViewportSettings settings;
        ViewportData data;
        /**<summary>Be careful with that. Create new settings instance only through a parameter constructor.</summary>*/
        public ViewportSettings Settings { get => settings; set { settings = value; } }
        public DrawOrder DrawOrder { get => data.order; set { data.order = value; } }
        public List<UIElement> Elements {
            get => data.elements;
            set {
                if (settings.readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET Elements VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                data.elements = value;
            }
        }
        public int Selection {
            get => data.selection;
            set {
                if (settings.readOnly) {
                    Debug.Fail("WARNING: TRYING TO SET Selection VIEWPORT FIELD WHILE ITS READONLY");
                    return;
                }
                data.selection = value;
                Draw();
            }
        }
        
        
        // Constructor
        /**<summary>
         * Create a new Viewport instance
         * </summary>
         * <param name="distance">Distance between elements in characters.</param>
         * <param name="paramReadonly">Set whether the fields are readonly or not<br/>Note: consider setting it to true in case to make it safer.</param>
         */
        public Viewport(int x, int y, int width, int height, bool paramReadonly = true, int distance = 1, ViewportMargins viewMargins = new ViewportMargins()) {
            data.elements = new List<UIElement>();
            data.order = DrawOrder.Horizontal;
            data.selection = 0;
            data.breakLoop = false;
            margins = viewMargins;
            settings = new ViewportSettings(1, x, y, width, height, paramReadonly);
            UserKeyInput.KeyPress += ProcessKeyEvent;
        }
        public Viewport(ViewportSettings viewSettings) {
            settings = new ViewportSettings(
                viewSettings.inlineSpace,
                viewSettings.x,
                viewSettings.y,
                viewSettings.width,
                viewSettings.height,
                viewSettings.readOnly
                );
        }


        // Drawing
        public void Draw(bool removeOld = true) {
            Console.CursorVisible = false;
            (int x, int y) cursor = (PositionX, PositionY-1);
            if (data.elements.Count == 0) {
                Debug.Fail("Cannot draw viewport with zero elements!");
                return;
            }

            // this variables are used only if it is vertical layout
            List<int> _widths = new List<int>();
            int maxwid = 0;
            if (DrawOrder == DrawOrder.VerticalCenter | DrawOrder == DrawOrder.VerticalNoCent) {
                for (int i = 0; i < data.elements.Count; i++) {
                    _widths.Add(data.elements[i].innerText.Length);
                }
                maxwid = _widths.ToArray().Max();
            }
            for (int i = 0; i < data.elements.Count; i++) {
                switch (data.order) {
                    case DrawOrder.Horizontal: // just move the cursor down
                        cursor.y++;
                        cursor.x = PositionX; 
                        break;
                    case DrawOrder.VerticalCenter: // move cursor down & center x position
                        cursor.x = PositionX + ((maxwid / 2) - (data.elements[i].innerText.Length / 2) - (PositionX - Width / 2));
                        cursor.y++;
                        break;
                    case DrawOrder.VerticalNoCent: // move cursor down & center x position
                        cursor.x = PositionX + (maxwid / 2) - (Width / 2);
                        cursor.y++;
                        break;
                    case DrawOrder.Absolute: // just follow every element position
                        cursor.x = data.elements[i].PositionX;
                        cursor.y = data.elements[i].PositionY;
                        break;
                    case DrawOrder.Inline:

                        break;
                }
                data.elements[i].print(cursor.x, cursor.y, removeOld, Width - cursor.x - 1);
            }
            if (data.elements[data.selection].PositionX + data.elements[data.selection].innerText.Length - 1 < Console.BufferWidth)
            Console.CursorLeft = data.elements[data.selection].PositionX + data.elements[data.selection].innerText.Length - 1;
            if (data.elements[data.selection].PositionY < Console.BufferHeight)
            Console.CursorTop = data.elements[data.selection].PositionY;
            if (Console.CursorLeft+1 < Console.BufferWidth)
            UCO.WriteXY(Console.CursorLeft + 1, Console.CursorTop, " ", doTry: true);
            
            Console.CursorVisible = true;
        }
        public void DumpArea(ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.White, char chr = ' ') {
            UCO.ClearConsoleArea(PositionX, PositionY, Width, Height, bg, fg, chr);
        }
        
        
        // Cursor
        public void MoveCursor(bool up) {
            while (true) {
                if (data.elements.Count == 0) return;
                data.elements[data.selection].hover(false);

                if (up)
                {
                    data.selection--;
                    if (data.selection < 0)
                    {
                        data.selection++;
                    }
                }
                else
                {
                    data.selection++;
                    if (data.selection > data.elements.Count - 1)
                    {
                        data.selection--;
                    }
                }
                if (data.elements[data.selection].Interactable) break;
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
            data.elements[data.selection].click();
        }


        // Loops
        public async Task MainloopAsync() {
            await Task.Run(Mainloop);
        }
        public void Mainloop() {
            Draw();
            if (data.elements.Count == 0) {
                throw new Exception("Cannot start mainloop without elements!");
            }
            UserKeyInput.CallEvent(new ConsoleKeyInfo((char)UISysConfig.UIMoveDownKey_txtIn, UISysConfig.UIMoveDownKey_txtIn, false, false, false)); Thread.Sleep(2);
            UserKeyInput.CallEvent(new ConsoleKeyInfo((char)UISysConfig.UIMoveUpKey_txtIn, UISysConfig.UIMoveUpKey_txtIn, false, false, false)); Thread.Sleep(2);
            while (!data.breakLoop) {
                if (!data.breakLoop) UserKeyInput.IterateLoop();
                else {
                    Debug.WriteLine("Terminating loop...");
                    UCO.WriteXY(Console.CursorLeft + 1, Console.CursorTop, " ");
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
        

        // Memory
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
    }
}