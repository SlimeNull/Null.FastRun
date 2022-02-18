using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Null.Faststart.WinForm.Util
{
    class DragMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            return false;
        }
    }
}
