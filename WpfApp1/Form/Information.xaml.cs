using System.Windows;
using System.Windows.Forms;

namespace WpfApp1.Form
{
    /// <summary>
    /// Information（即文献导入窗口） 的交互逻辑
    /// </summary>
    public partial class Information : Window
    {
        public Information()
        {
            InitializeComponent();
        }

        public string TextBoxText { get { return TextBox2.TextBoxText; } }
        public int CodeText { get; set; }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Throw(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog fileDialog = new())
            {
                fileDialog.Filter = "txt文件 (*.txt)|*.txt|nbib文件 (*.nbib)|*.nbib|数据库文件 (*.db)|*.db";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TextBox2.TextBoxText = fileDialog.FileName;

                    CodeText = fileDialog.SafeFileName.Split(".")[1] == "txt" ? 1 : 2;
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
