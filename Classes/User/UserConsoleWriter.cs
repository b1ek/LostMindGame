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

            string[] splitedValue = value.Split("\n");
            foreach (string current in splitedValue) {
                UserConsoleOutput.WriteXY(_x, _y, current);
                _x = _sx;
                _y++;
            }

        }
        public void writeLine(string value) {
            UserConsoleOutput.WriteXY(_x, _y, value);
            _x = _sx;
            _y++;
        }

    }
}
