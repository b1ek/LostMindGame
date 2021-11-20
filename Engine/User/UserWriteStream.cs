using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.User {
    class UserWriteStream: IDisposable {
        string _s = string.Empty;
        bool disposed = false;
        int _x = 0;
        int _y = 0;
        int _height = 0;
        int _width = 0;

        public int X => _x;
        public int Y => _y;
        public int Height => _height;
        public int Width => _width;
        public ConsoleColor StdBackground = ConsoleColor.Black;
        public ConsoleColor StdTextColor = ConsoleColor.White;
        public string Written => _s;



        public void Write(string s) {
            if (disposed) return;
            string[] spl = s.Replace('\r', '\0').Split('\n');
            foreach(var str in spl) {

                if (str.Length > _width) {
                    var sppl = Util.Util.SplitByCount(str, Width);

                    foreach (var _str in sppl) {
                        UserNativeLib.printToXY(_str, _x, _y);
                        _y += 1;
                    }
                    _x = 0; _y -= 1;
                }
                UserNativeLib.printToXY(str, _x, _y);
            }
            _s = _s + s;
        }



        public void Flush() {

        }
        public void Dispose() {

        }
    }
}
