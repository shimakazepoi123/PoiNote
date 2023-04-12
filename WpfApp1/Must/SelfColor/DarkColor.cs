using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.Must.SelfColor
{
    /// <summary>
    /// 夜间模式颜色设置
    /// </summary>
    internal class DarkColor
    {
        public DarkColor() { }
        /// <summary>
        /// 设置Grid颜色
        /// </summary>
        public Color GridColor { get { return Color.FromRgb(71, 79, 108); }
}

        /// <summary>
        /// 设置DataGrid颜色
        /// </summary>
        public Color DataGridColor { get { return Color.FromRgb(64, 71, 97); }  }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        public Color FontColorDark { get { return Color.FromRgb(160, 160, 160); } }
    }
}
