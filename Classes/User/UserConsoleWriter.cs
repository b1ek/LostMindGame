using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.User {
    public class UserConsoleWriter {
        public int _sx; public int _sy;
        public int _x; public int _y;

        /**<summary>
         * Initialize new UserConsoleWriter instance.
         * </summary>
         * <param name="x">Left x pos</param>
         * <param name="y">Left y pos</param>
         */
        public UserConsoleWriter(int x, int y) {
            _x = x;
            _y = y;
            _sx = _x;
            _sy = _y;
        }


        /**<summary>
         * Write value to console.
         * </summary>
         * <param name="value">The value to print.</param>
         */
        public void Write(string value) {
            var valueSpl = value.Split('\n');
            int lx = 0;
            foreach (var val in valueSpl) {
                Console.SetCursorPosition(_x, _y);
                //UserNativeLib.Set
                UserNativeLib.Write(val);
                _y++;
                lx = _x;
                _x = _sx;
            } _x = lx+1; _y--; Console.SetCursorPosition(_x, _y);
        }

        /**<summary>
         * Write value to console, and print new line at the end.
         * </summary>
         * <param name="value">The value to print.</param>
         */
        public void WriteLine(string value) {
            var valueSpl = value.Split('\n');
            foreach (var val in valueSpl) {
                Console.SetCursorPosition(_x, _y);
                Console.Write(val);
                _y++;
                _x = _sx;
            } Console.SetCursorPosition(_x, _y);
        }

        public async Task FancyWrite(string value)
        {
            string toPrint = value;
            foreach (var c in value) {
                Write(c.ToString());
                toPrint = toPrint.Substring(1);
                if (UserKeyInput.isKeyPressed(ConsoleKey.Spacebar)) { Write(toPrint); break; }
                await Task.Delay(Util.RandomGen.getInt(1, 4));
            } _y++; _x = _sx; Console.SetCursorPosition(_x, _y);
        }

        public async Task FancyWrite(string value, int delay)
        {
            string toPrint = value;
            foreach (var c in value) {
                Write(c.ToString());
                toPrint = toPrint.Substring(1);
                if (UserKeyInput.isKeyPressed(ConsoleKey.Spacebar)) { Write(toPrint); break; }
                await Task.Delay(delay);
            } _y++; _x = _sx; Console.SetCursorPosition(_x, _y);
        }

        public async Task FancyWrite(string value, int floor, int roof)
        {
            string toPrint = value;
            foreach (var c in value) {
                Write(c.ToString());
                toPrint = toPrint.Substring(1);
                if (UserKeyInput.isKeyPressed(ConsoleKey.Spacebar)) { Write(toPrint); break; }
                await Task.Delay(Util.RandomGen.getInt(floor, roof));
            } _y++; _x = _sx; Console.SetCursorPosition(_x, _y);
        }

        public async Task FancyWrite(string value, string end)
        {
            string toPrint = value;
            foreach (var c in value) {
                Write(c.ToString());
                toPrint = toPrint.Substring(1);
                if (UserKeyInput.isKeyPressed(ConsoleKey.Spacebar)) { Write(toPrint); break; }
                await Task.Delay(Util.RandomGen.getInt(1, 4));
            } Write(end);
        }

        public async Task FancyWrite(string value, int floor, int roof, string end)
        {
            string toPrint = value;
            foreach (var c in value) {
                Write(c.ToString());
                toPrint = toPrint.Substring(1);
                if (UserKeyInput.isKeyPressed(ConsoleKey.Spacebar)) { Write(toPrint); break; }
                await Task.Delay(Util.RandomGen.getInt(floor, roof));
            }
            Write(end);
        }
    }
}
