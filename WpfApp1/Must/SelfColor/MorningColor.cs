using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.Must.SelfColor
{
    internal class MorningColor
    {
        public MorningColor() { }
        /// <summary>
        /// 设置Grid颜色
        /// </summary>
        public Color GridColor { get { return Color.FromRgb(255,255,255); } }                  
        /// <summary>
        /// 设置DataGrid颜色
        /// </summary>
        public Color DataGridColor { get { return Color.FromRgb(255,255,255); } }               
        /// <summary>
        /// 设置字体颜色
         /// </summary>
        public Color FontColorDark { get { return Color.FromRgb(0,0,0); } }
    }
}
