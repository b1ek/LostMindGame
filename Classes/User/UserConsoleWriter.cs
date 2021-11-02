using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.User {
    public class UserConsoleWriter {
        public int _sx; public int _sy;
        public int _x; public int _y;
        public UserConsoleWriter(int x, int y) {
            _x = x;
            _y = y;
            _sx = _x;
            _sy = _y;
        } public void write(string value) {
            Console.SetCursorPosition(_x, _y);
            var valueSpl = value.Split('\n');
            int lx = 0;
            foreach (var val in valueSpl) {
                UserConsoleOutput.WriteXY(_x, _y, val);
                _y++;
                lx = _x;
                _x = _sx;
            } _x = lx+1; _y--;
            Console.SetCursorPosition(_x, _y);
        } public void writeLine(string value) {
            foreach (var current in value)
            {
                if (current == '\n')
                {
                    _x = _sx;
                    _y++;
                }
                else
                {
                    Console.SetCursorPosition(_x, _y);
                    Console.Write(current);
                    _x++;
                }
            } _y++; Console.SetCursorPosition(_x, _y);
        }
        
        public async Task fancyWrite(string value) {
            foreach (var c in value) {
                write(c.ToString());
                await Task.Delay(Util.RandomGen.getInt(1, 4));
            } _y++; _x = _sx; Console.SetCursorPosition(_x, _y);
        }
        
        public async Task fancyWrite(string value, int delay)
        {
            foreach (var c in value) {
                write(c.ToString());
                await Task.Delay(delay);
            } _y++; _x = _sx; Console.SetCursorPosition(_x, _y);
        }
        
        public async Task fancyWrite(string value, int floor, int roof)
        {
            foreach (var c in value) {
                write(c.ToString());
                await Task.Delay(Util.RandomGen.getInt(floor, roof));
            } _y++; _x = _sx; Console.SetCursorPosition(_x, _y);
        }
        
        public async Task fancyWrite(string value, string end)
        {
            foreach (var c in value) {
                write(c.ToString());
                await Task.Delay(Util.RandomGen.getInt(1, 4));
            } write(end);
        }

        public async Task fancyWrite(string value, int floor, int roof, string end)
        {
            foreach (var c in value) {
                write(c.ToString());
                await Task.Delay(Util.RandomGen.getInt(floor, roof));
            }
            write(end);
        }
    }
}
