using LostMind.Classes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class Viewport {
        int _x = 0;
        int _y = 0;
        int selection = 0;
        List<UIElement> _elements = new List<UIElement>();
        public List<UIElement> Elements { get { return _elements; } }
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

        public Viewport(int x, int y, int width, int height) {
            _x = x;
            _y = y;

            for (int i = 0; i > height; i++) {
                UserConsoleOutput.WriteXY(x, y + i, new string(' ', width), ConsoleColor.DarkRed, ConsoleColor.White);
            }
            UserKeyInput.KeyPress += OnKeyPress;
        }

        public void OnKeyPress(ConsoleKeyInfo key) {
            ConsoleKey k = key.Key;
            if (k == ConsoleKey.W) moveCursorUp();
            if (k == ConsoleKey.S) moveCursorDown();
            if (k == ConsoleKey.Spacebar) clickSelection();
        }

        public void drawElements() {
            int i = 0;
            foreach (var elem in _elements) {
                elem.print(_marginLeft+_x, _marginTop+_y+i);
                i++;
            }
        }
        public void addElement(UIElement element) {
            _elements.Add(element);
            element.print(_marginLeft + _x, _marginTop + _y + Elements.Count);
            if (_elements.Count == 1) {
                _elements[0].hover(true);
            }
        }

        public void clickSelection() {
            _elements[selection].click();
        }

        public void moveCursorUp()
        {
            if (selection - 1 < _elements.Count)
            {
                if (selection == 0) { }
                else { _elements[selection].hover(false); selection--; _elements[selection].hover(true); }
            }
        }
        public void moveCursorDown() {
            if (selection + 1 < _elements.Count)
            {
                if (selection == _elements.Count) { }
                else
                {
                    _elements[selection].hover(false);
                    selection++;
                    _elements[selection].hover(true);
                }
            }
        }
        
    }
}
