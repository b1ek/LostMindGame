using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.User
{
    public class UserConsoleWriter
    {
        public int _sx; public int _sy;
        public int _x; public int _y;
        public UserConsoleWriter(int x, int y) {
            _x = x;
            _y = y;
            _sx = _x;
            _sy = _y;
        }

        public void write(string value) {

            foreach (var current in value) {
                UserConsoleOutput.WriteXY(_x, _y, current.ToString());
                if (current == '\n') {
                    _x = _sx;
                    _y++;
                }
                _x++;
            }
        }
        public void writeLine(string value) {

            foreach (var current in value) {
                UserConsoleOutput.WriteXY(_x, _y, current.ToString());
                if (current == '\n') {
                    _x = _sx;
                    _y++;
                }
                _x++;
            } _y++;
        }
        public async Task fancyWrite(string value) {
            foreach (var c in value) {
                write(c.ToString());
                await Task.Delay(Util.RandomGen.getInt(1, 4));
            }
        }
    }
}
