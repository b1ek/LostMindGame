using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.User
{
    /**<summary>
     * <b>UCO - User Console Output</b><br/>
     * Macros for easy console output
     * </summary>
     */
    public class UCO
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        public static void FlushConsole() {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            UserNativeLib.flushConsole();
        }

        /**<summary>
         * Alias to<br/>
         * Console.SetBuffer/WindowSize(width, height);
         * </summary>
         * <param name="width">Console width</param>
         * <param name="height">Console height</param>
         */
        public static void SetSize(int width, int height) {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }
        public static void TrySetSize(int width, int height) { try { SetSize(width, height); } catch (Exception) { } }

        public static Exception TrySetCurPos(int x, int y) {
            try {
                Console.SetCursorPosition(x, y);
                return null;
            } catch (Exception e) {
                return e;
            }
        }

        public static async Task BeepAsync(int freq, int duration) {
            Beep(freq, duration);
            await Task.Delay(0);
        }

        public static async Task WriteFancy(string val) {
            var cv = val.ToCharArray();
            foreach (var c in cv) {
                await Task.Delay(Util.RandomGen.getInt(1, 2));
                UserNativeLib.Write(c);
            }
            Console.Write("\n");
        }


        public static bool WriteXY(int x, int y, string val, ConsoleColor bgClr = ConsoleColor.Black, ConsoleColor txtClr = ConsoleColor.White, bool doTry = false) {
            if (doTry) {
                try {
                    Console.BackgroundColor = bgClr;
                    Console.ForegroundColor = txtClr;
                    UserNativeLib.printToXY(val, x, y);
                    return true;
                } catch (Exception) { return false; }
            }
            Console.BackgroundColor = bgClr;
            Console.ForegroundColor = txtClr;
            UserNativeLib.printToXY(val, x, y);
            return true;
        }
    }
}
