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
    /// RoundTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class RoundTextBox : UserControl
    {
        public RoundTextBox()
        {
            InitializeComponent();
            TextBox1.Cursor = Cursors.IBeam;
        }

        public string TextBoxText
        {
            get => TextBox1.Text;
            set => TextBox1.Text = value;
        }
    }
}
