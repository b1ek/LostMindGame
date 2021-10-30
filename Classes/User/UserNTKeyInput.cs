using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.User
{
    public class UserNTKeyInput
    {
        public const int WM_HOTKEY_MSG_ID = 0x0312;
        public const int STD_OUTPUT_HANDLE = -11;

        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        static IntPtr hWnd = GetStdHandle(STD_OUTPUT_HANDLE);


    }
}
