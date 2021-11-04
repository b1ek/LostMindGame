using System;
using System.Linq;
using System.Runtime.InteropServices;

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

        const int STD_OUTPUT_HANDLE = -11;
        const int TMPF_TRUETYPE = 4;
        const int LF_FACESIZE = 32;
        static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        static IntPtr hWnd = GetStdHandle(STD_OUTPUT_HANDLE);
        public static IntPtr StdOut { get => GetStdHandle(-11); }
        public static IntPtr StdIn { get => GetStdHandle(-10); }


        public static void setFont(string fontName = "Lucida Console", int fontWeight = 10) {
                unsafe {
                if (hWnd == INVALID_HANDLE_VALUE) {
                    hWnd = StdOut;
                    if (hWnd == INVALID_HANDLE_VALUE) {
                        throw new NoConsoleException("No console output was found!");
                    }
                }
                CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX();
                info.cbSize = (uint)Marshal.SizeOf(info);

                // Set console font to Lucida Console.
                CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
                newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                newInfo.FontFamily = TMPF_TRUETYPE;
                IntPtr ptr = new IntPtr(newInfo.FaceName);
                Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);

                // Get some settings from current font.
                newInfo.dwFontSize = new COORD(Convert.ToInt16(fontWeight), Convert.ToInt16(fontWeight * 1.75));
                newInfo.FontWeight = fontWeight;
                SetCurrentConsoleFontEx(hWnd, false, ref newInfo);
            }
        }
        public static void trySetFont(string fontName = "Lucida Console", int fontWeight = 10) {
            unsafe {
                try {
                    setFont(fontName);
                } catch (Exception) { /* ignored */ }
            }
        }
    }
    public class NoConsoleException : Exception { public NoConsoleException(string msg):base(msg) {} }
}
