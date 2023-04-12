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

namespace WpfApp1.Form
{
    /// <summary>
    /// AddItem.xaml 的交互逻辑
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
            StackPanelGenerate();
        }

        // 产生手动添加文献的入口
        private void StackPanelGenerate()
        {
            int a;

            string[] b =
            {
                "标题",
                "类型",
                "刊名",
                "期数",
                "卷数",
            };

            int height = (int)Height / Main.RowDefinitions.Count;
            for (int i = 0; i < b.Length; i++)
            {
                StackPanel stackPanel = new()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                Label label = new()
                {
                    Height = 30,
                    Margin = new Thickness(0, 0, 30, 0),
                    Content = b[i],
                };
                RoundTextBox roundTextBox = new()
                {
                    Width = 300,
                    Height = height - 10,

                };

                Main.Children.Add(stackPanel);
                Grid.SetRow(stackPanel, i);

                stackPanel.Children.Add(label);
                stackPanel.Children.Add(roundTextBox);
            }
        }
    }
}
