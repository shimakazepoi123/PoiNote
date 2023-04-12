using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.UserMade
{
    /// <summary>
    /// LabelSelf.xaml 的交互逻辑
    /// </summary>
    public partial class LabelSelf : UserControl
    {
        public LabelSelf()
        {
            InitializeComponent();
            stackPanel.MouseEnter += new MouseEventHandler((s, arg) => { stackPanel.Background = new SolidColorBrush(Color.FromRgb(204, 202, 202)); });
            stackPanel.MouseLeave += new MouseEventHandler((s, arg) => { stackPanel.Background = Brushes.Transparent; });
        }

        public string ButtonText { get { return (string)Button1.Content; } set { Button1.Content = value; } }

    }
}
