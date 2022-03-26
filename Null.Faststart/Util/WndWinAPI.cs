using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Null.Faststart.Util
{
    public static class WndWinAPI
    {
        public const uint WM_COPYGLOBALDATA = 0x0049;
        public const uint WM_COPYDATA = 0x004A;
        public const uint WM_DROPFILES = 0x0233;

        public enum ChangeWindowMessageFilterAction : uint
        {
            Reset,
            Allow,
            DisAllow
        }

        public enum MessageFilterInfo : uint
        {
            None = 0, AlreadyAllowed = 1, AlreadyDisAllowed = 2, AllowedHigher = 3
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct ChangeFilterStruct
        {
            public uint size;
            public MessageFilterInfo info;
        }

        /// <summary>
        /// Adds or removes a message from the User Interface Privilege Isolation (UIPI) message filter.
        /// </summary>
        /// <param name="message">The message to add to or remove from the filter.</param>
        /// <param name="dwFlag">The action to be performed. One of the following values.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ChangeWindowMessageFilter(uint message, ChangeWindowMessageFilterAction dwFlag); 

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ChangeWindowMessageFilterEx(IntPtr hWnd, uint msg, ChangeWindowMessageFilterAction action, ref ChangeFilterStruct changeInfo);

        public static bool ChangeWindowMessageFilter(IntPtr hWnd, uint msg, ChangeWindowMessageFilterAction action)
        {
            ChangeFilterStruct changeFilterStruct = new ChangeFilterStruct();
            changeFilterStruct.size = (uint)Marshal.SizeOf(changeFilterStruct);
            changeFilterStruct.info = 0;
            return ChangeWindowMessageFilterEx(hWnd, msg, action, ref changeFilterStruct);
        }

        [DllImport("shell32.dll")]
        public static extern void DragAcceptFiles(IntPtr hwnd, bool fAccept);
    }
}
