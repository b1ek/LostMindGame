using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Util
{
    public static class RandomGen
    {
        public static Random random = new Random();
        /// <summary>
        /// Returns any int in range 0 to 2147483647
        /// </summary>
        public static int getInt() {
            return random.Next(0, int.MaxValue);
        }
        /// <summary>
        /// Returns any int in range floor to roof
        /// </summary>
        public static int getInt(int floor, int roof) {
            return random.Next(floor, roof);
        }
        /// <summary>
        /// Returns any int in range 0 to roof
        /// </summary>
        public static int getInt(int roof) {
            return random.Next(0, roof);
        }
        public static string getString(int len) {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!\"#$%&\\'()*+,-./:;<=>?@[\\\\]^_`{|}~ \t\n\r\x0b\x0c"; return new string(Enumerable.Repeat(chars, len).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string getString(int len, string chars) {
            return new string(Enumerable.Repeat(chars, len).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string getString() {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
