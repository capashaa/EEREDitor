using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EEditor
{
    // http://stackoverflow.com/a/9753302/1175094
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar uploadProgressBar, int state)
        {
            SendMessage(uploadProgressBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
