using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.UI
{
    public class UITextInput : UIElement
    {
        string _text;
        int _cursor = 0;
        public string text
        {
            get => _text;
            set { _text = value; }
        }
        int _width;
        public UITextInput(int width) : base(true) {
            colors.defaultForeground = ConsoleColor.DarkGreen;
            _width = width;
            _text = new string(' ', _width);

            innerText = "[" + new string(' ', width) + "]";
        }
        public void addChar(char c) {
            if (c == 8) {
                if (_cursor - 1 < 0) return;
                var _a = _text.ToCharArray();
                _a[_cursor-1] = ' ';
                _text = new string(_a);
                _cursor -= 2;
                print(_x, _y);
                return;
            } if (_cursor+1 > _text.Length) return;
            if (c == 46 | c == '\0') {
                var _a = _text.ToCharArray();
                _a[_cursor+1] = ' '; _cursor--;
                _text = new string(_a);
                print(_x, _y);
                return;
            }

            var a = _text.ToCharArray();
            a[_cursor] = c;
            _text = new string(a);
            print(_x, _y);
        }
        public void moveCursor(int moveCharsToRight) {
            if (_cursor + moveCharsToRight > _text.Length | _cursor + moveCharsToRight < 0) return;
            _cursor += moveCharsToRight;
        }
        public override void print(int x, int y, bool displayCursor = true, int maxLen = -1) {
            try {
                int curr = _cursor;
                string text = "[" + new string(' ', _text.Length) + "]";
                if (maxLen > 0) text = Utils.SplitByCount(text, maxLen).First();
                User.UCO.WriteXY(x, y, text, ConsoleColor.Black, ConsoleColor.White, true);
                User.UCO.WriteXY(x + 1, y, _text, bg, txt);
                if (displayCursor)  User.UCO.WriteXY(x + curr+2, y, _text[curr+1].ToString(), ConsoleColor.DarkBlue, txt);
            } catch (Exception) { }
            _x = x;
            _y = y;
        }

        public override void hover(bool hovered) {
            base.hover(hovered);
            print(_x, _y, hovered);
        }
    }
}
