using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Qtud.Qtud
{
    class PanelScorllTest
    {
        /// <summary>
        /// 初始化Panel
        /// </summary>
        /// <param name="panel"></param>
        public static void InitializePanelScroll(Panel panel)
        {
            panel.Click += (obj, arg) => { panel.Select(); };
            InitializePanelScroll(panel, panel);
            return;
        }

        /// <summary>
        /// 递归初始化Panel内部各容器和控件
        /// </summary>
        /// <param name="container"></param>
        /// <param name="panelRoot"></param>
        private static void InitializePanelScroll(Control container, Control panelRoot)
        {
            foreach (Control control in container.Controls)
            {
                if (control is Panel || control is GroupBox || control is SplitContainer ||
                    control is TabControl || control is UserControl)
                {
                    control.Click += (obj, arg) => { panelRoot.Select(); };
                    InitializePanelScroll(control, panelRoot);
                }
                else if (control is Label)
                {
                    control.Click += (obj, arg) => { panelRoot.Select(); };
                }
            }
        }
    }
}
