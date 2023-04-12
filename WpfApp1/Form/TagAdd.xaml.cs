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
using System.Windows.Shapes;
using WpfApp1.UserMade;
using System.Windows.Forms;

namespace WpfApp1.Form
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class TagAdd : Window
    {
        public TagAdd()
        {
            InitializeComponent();
        }

        public string TextBoxText { get { return TextBox2.TextBoxText; } }
        public int CodeText { get; set; }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
