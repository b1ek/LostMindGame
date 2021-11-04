using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

namespace LostMind.Classes.User
{
    public static class UserConsoleNativeInterface
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal uint nFont;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[LF_FACESIZE];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            internal short X;
            internal short Y;

            internal COORD(short x, short y) {
                X = x;
                Y = y;
            }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool GetCurrentConsoleFontEx(
               IntPtr consoleOutput,
               bool maximumWindow,
               ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
               IntPtr consoleOutput,
               bool maximumWindow,
               ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        const int STD_OUTPUT_HANDLE = -11;
        const int STD_INPUT_HANDLE = -10;
        const int TMPF_TRUETYPE = 4;
        const int LF_FACESIZE = 32;
        static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        /**<summary>Gets STD_OUTPUT_HANDLE pointer.</summary>*/
        public static IntPtr StdOut { get => GetStdHandle(STD_OUTPUT_HANDLE); }
        /**<summary>Gets STD_INPUT_HANDLE pointer.</summary>*/
        public static IntPtr StdIn { get => GetStdHandle(STD_INPUT_HANDLE); }
        public static IntPtr hWnd { get => Process.GetCurrentProcess().MainWindowHandle; }
        static IntPtr _stdout = StdOut;

        /**<summary>
         * Set entire console font.
         * Be careful with setting console size after this.
         * </summary>
         * <param name="fontName">Name of font</param>
         * <param name="charHeight">Height of every single char</param>
         * <param name="charWidth">Width of every single char(most fonts doesn't support this feature)</param>
         * <param name="fontWeight">Font weight</param>
         */
        public static void setFont(string fontName = "Lucida Console", short charWidth = 4, short charHeight = 12, short fontWeight = 1) {
                unsafe {
                if (_stdout == INVALID_HANDLE_VALUE) {
                    _stdout = StdOut;
                    if (_stdout == INVALID_HANDLE_VALUE) {
                        throw new NoConsoleException("No console output was found!");
                    }
                }
                CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
                newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                newInfo.FontFamily = TMPF_TRUETYPE;
                IntPtr ptr = new IntPtr(newInfo.FaceName);
                Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);
                // char width/height & font Width
                newInfo.dwFontSize = new COORD(charWidth, charHeight);
                newInfo.FontWeight = fontWeight;
                SetCurrentConsoleFontEx(_stdout, false, ref newInfo);
            }
        }
        /**<summary>
         * Calls setFont() function with try/catch surrounded.
         * </summary>
         */
        public static void trySetFont(string fontName = "Lucida Console", short charWidth = 4, short charHeight = 12, short fontWeight = 1) {
            try {
                setFont(fontName, charWidth, charHeight, fontWeight);
            } catch (Exception) { /* ignored */ }
        }

        public static bool setTransparency(byte alpha) {
            UInt32 color = (UInt32) ColorTranslator.ToWin32(Color.FromArgb(155, 155, 155));
            bool retval = SetLayeredWindowAttributes(GetConsoleWindow(), color, alpha, 2);
            if (!retval) {
                var errcode = GetLastError();
                throw new WinApiError(errcode);
            }
            return retval;
        }
    }
    public class NoConsoleException : Exception { public NoConsoleException(string msg):base(msg) {} }
    public class WinApiError : Exception { public WinApiError(uint errcode) : base("Error code: " + Convert.ToString(errcode) + " (reference: https://bit.ly/3EHMJUy)") { } }
}
