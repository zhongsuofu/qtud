using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;  

namespace Qtud.Qtud
{ 

    //开启双缓冲  
    class MyPanel : Panel
    {
        public MyPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }
    }  
}
