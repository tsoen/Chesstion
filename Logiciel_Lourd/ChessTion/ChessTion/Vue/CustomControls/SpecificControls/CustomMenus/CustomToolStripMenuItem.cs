using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessTion.Vue.CustomControls.SpecificControls.CustomMenus
{
    class CustomToolStripMenuItem : ToolStripMenuItem
    {
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            double darkness = 1 - (0.299 * ForeColor.R + 0.587 * ForeColor.G + 0.114 * ForeColor.B) / 255;
            if (darkness < 0.5)
                ForeColor = Theme.Style.MenuBackColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
             ForeColor = Theme.Style.MenuForeColor;
        }
    }
}
