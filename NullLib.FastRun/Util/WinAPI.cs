using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NullLib.FastRun.Util
{
    public static class WinAPI
    {
        public const uint SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1;
        public const uint SYMBOLIC_LINK_FLAG_ALLOW_UNPRIVILEGED_CREATE = 0x2;

        /// <summary>
        /// Creates a symbolic link.
        /// </summary>
        /// <param name="lpFileName">The symbolic link to be created.</param>
        /// <param name="lpExistingFileName">The name of the target for the symbolic link to be created.</param>
        /// <param name="flags">Indicates whether the link target, <paramref name="lpExistingFileName"/>, is a directory.</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public extern static bool CreateSymbolicLink(string lpFileName, string lpExistingFileName, uint flags);

        /// <summary>
        /// Establishes a hard link between an existing file and a new file. This function is only supported on the NTFS file system, and only for files, not directories.
        /// </summary>
        /// <param name="lpFileName">The name of the new file.</param>
        /// <param name="lpExistingFileName">The name of the existing file.</param>
        /// <param name="lpSecurityAttributes">Reserved; must be IntPtr.Zero.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);
    }
}
