using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Form;
using WpfApp1.Data.DataAbout;
using System.Data.SQLite;
using WpfApp1.UserMade;
using MessageBox = System.Windows.MessageBox;
using System.Threading;
using WpfApp1.Must.SelfColor;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

// 计划添加思维导图模式

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AllowDrop = false;
            contextMenu();
            Wenxian.SelectedCellsChanged += new SelectedCellsChangedEventHandler(DataGridCellChange);
            SizeChanged += new SizeChangedEventHandler(HeighChange);
            ToolTipA();
            
        }

        /*初始化全局变量，该变量是有关Note*/
        public static string TitleSelected;
        /*初始化两个变量，一个变量WenXian用于临时存储读入的文献资料，另一个则用于存储标签*/
        public static StringDict WenXian = new StringDict();
        protected Dictionary<string, int> Tag = new Dictionary<string, int>();
        /* 夜间模式和早间模式,0是早间模式，1是夜间模式 */
        public int Mode = 1;

        public string FilePath1;

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            Information information = new Information();
            string pattern = "(?<=%0)(?<Type>.+)|(?<=%A)(?<Author>.+)|(?<=%T)(?<Title>.+)|(?<=%J)(?<Jounral>.+)|(?<=%D)(?<Year>.+)|(?<=%N)(?<Period>.+)|(?<=%P)(?<Page>.+)|(?<=%V)(?<Volume>.+)|(?<=%K)(?<Keyword>.+)|(?<=%R)(?<DOI>.+)";
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            /*根据返回值判断不同类型*/
            if (information.ShowDialog() == true)
            {
                MainWindowAbout Extract = new MainWindowAbout(pattern, information.TextBoxText);

                switch (information.CodeText)
                {
                    case 1:
                        keyValues = Extract.ExtractWenXian();
                        keyValues.Add("FilePath", "");
                        var ItemRow = AddContent(keyValues);

                        /*检测是否有重复添加的文献*/
                        bool ifAdd = WenXian.StringExit(ItemRow.Title);
                        if (ifAdd == false)
                        {
                            WenXian.StringAdd(keyValues);
                            Wenxian.Items.Add(ItemRow);
                            break;
                        }
                        else
                        {
                            MessageBox.Show("已经添加！", "提示");
                            break;
                        }
                    case 2:
                        SQLAbout sQLAbout = new SQLAbout(information.TextBoxText, "null");
                        IEnumerable result = sQLAbout.SQLSearch();
                        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                        /*此处与SQLRead中的创建表一一对应*/
                        string[] SQList = { "ID", "Type", "Title", "Author", "Jounral", "Year", "Period", "Page", "Volume", "Tag", "Keyword", "DOI", "FilePath" };
                        Thread thread = new Thread(() =>
                        {
                            foreach (SQLiteDataReader read in result)
                            {
                                for (int i = 0; i < read.FieldCount; i++)
                                {
                                    keyValuePairs.Add(SQList[i], read.GetString(i));
                                }
                                string[] tagSplit = keyValuePairs["Tag"].Split(",");
                                foreach (string tag in tagSplit)
                                {
                                    if (tag == " ")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (Tag.ContainsKey(tag))
                                            Tag[tag] += 1;
                                        else
                                            Tag.Add(tag, 1);
                                    }
                                }
                            }
                        });
                        thread.Start();
                        thread.Join();
                        // 将标签添加进入左侧
                        TagAdd1 tagAdd1 = new TagAdd1(TagShow);
                        int i = 0;
                        foreach(var item in Tag)
                        {
                            if (i < Tag.Count)
                            {
                                tagAdd1.TagToShow(i,new Tuple<string,string>(item.Key, item.Value.ToString()));
                            }
                            i++;
                        }
                        if (thread.IsAlive == false)
                        {
                            var ItemRow1 = AddContent(keyValuePairs);
                            WenXian.StringAdd(keyValuePairs);
                            Wenxian.Items.Add(ItemRow1);

                            keyValuePairs.Clear();
                        }
                        break;

                }
            }
        }

        // 目前不会即时生效
        private void Tag1_Click(object sender, RoutedEventArgs e)
        {
            TagAdd tagAdd = new TagAdd();
            if (tagAdd.ShowDialog() is true)
            {
                WenXian.StringUpdate(new Tuple<string, string>(
                    "Tag", 
                    tagAdd.TextBoxText
                    ),TitleSelected);
            }
            


        }

        private void Note_Click(object sender, RoutedEventArgs e)
        {
            Markdown1 markdown1 = new();
            markdown1.Show();
        }

        /*添加左侧滑出面板和遮罩*/
        /*有明显卡顿感*/
        private void More_Click(object sender, RoutedEventArgs e)
        {
            // 需要解决硬编码问题
            string[] text = { "保存", "导出", "返回" };
            string[] path =
            {
                    "pack://application:,,,/Resources/保存__1_.png",
                    "pack://application:,,,/Resources/导出.png",
                    "pack://application:,,,/Resources/退出.png",
            };
            StackPanel stackPanel = new StackPanel()
            {
                Background = new SolidColorBrush(Color.FromRgb(71, 79, 108)),
                Orientation = Orientation.Vertical,
                Name = "LeftPannel"
            };

            /*遮罩层*/
            Border border1 = new()
            {
                Background = new SolidColorBrush(Color.FromRgb(71, 79, 108)),
                Opacity = 0.6,
                Name = "RightPannel"
            };
            
            Grid1.Children.Add(stackPanel);
            Grid.SetColumn(stackPanel, 0);
            Grid.SetRow(stackPanel, 1);
            Grid.SetRowSpan(stackPanel, 8);

            Grid1.Children.Add(border1);
            Grid.SetColumn(border1, 1);
            Grid.SetRow(border1, 1);
            Grid.SetRowSpan(border1, 8);
            Grid.SetColumnSpan(border1, 6);


            /*点击更多后的按钮事件*/
            new Thread(
                () =>
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ButtonEvent buttonEvent = new((List<Dictionary<string, string>>)WenXian);
                        for (int i = 0; i < path.Length; i++)
                        {
                            LabelSelf labelSelf = new()
                            {
                                FontSize = 16,
                                Foreground = Brushes.Gray,
                                ButtonText = text[i],
                            };
                            labelSelf.Photo.Source = new ImageSourceConverter().ConvertFromString(path[i]) as ImageSource;
                            stackPanel.Children.Add(labelSelf);
                            if (labelSelf.ButtonText == text[1]) labelSelf.Button1.Click += new RoutedEventHandler(buttonEvent.ExtractNote);
                            else if (labelSelf.ButtonText == text[0]) labelSelf.Button1.Click += new RoutedEventHandler(buttonEvent.SaveNote);
                            else if (labelSelf.ButtonText == text[2]) labelSelf.Button1.Click += new RoutedEventHandler((s, arg) => { Grid1.Children.Remove(stackPanel); Grid1.Children.Remove(border1); });
                        }
                    }));
                }).Start();

        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            DataViewFunction dataView = new DataViewFunction(Wenxian);
            dataView.DataGridCellMove("up");
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            DataViewFunction dataView = new DataViewFunction(Wenxian);
            dataView.DataGridCellMove("down");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItem = new();
            addItem.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Wenxian.SelectedItem != null)
            {

                DataRow selection = (DataRow)Wenxian.SelectedItem;

                Wenxian.Items.Remove(selection);
                WenXian.StringDelete(WenXian.StringIndex(selection.Title));
            }
            else
                MessageBox.Show("没有选择内容！");
        }

        private void DataGridCellChange(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Wenxian.SelectedItem is not null) { var a = Wenxian.SelectedItem as DataRow; TitleSelected = a.Title; }
        }

        private void HeighChange(object sender,SizeChangedEventArgs s)
        {
            ControlPanel.Margin = new(0, (int)new AutoChange(-0.05,ActualHeight), 0, 0);
        }

        private void ToolTipA()
        {
            Control[] control =
            {
                Import,
                Tag1,
                Note,
            };
            string[] name =
            {
                "导入",
                "标签",
                "笔记",
            };
            ToolTipAdd toolTipAdd = new ToolTipAdd(control,name);
            toolTipAdd.ToolTipAdd1();
        }

        private void contextMenu()
        {
            ContextMenu menu = new ContextMenu()
            {
                Name = "Context",

            };
            MenuItem menuItem = new MenuItem()
            {
                Header = "关联文件",
            };
            menuItem.Click += (s, e) =>
            {
                AddFile addFile = new AddFile();
                if (addFile.ShowDialog() is true)
                {

                    FilePath1 = addFile.TextBoxText;
                    WenXian.StringUpdate(new ValueTuple<string, string, string>(
                        TitleSelected,
                        "FilePath",
                        FilePath1
                        ));
                    List<Dictionary<string, string>> a = (List<Dictionary<string, string>>) WenXian;
                    int b = WenXian.StringIndex(TitleSelected);

                    var ItemRow = AddContent(a[b]);
                    // 删除原纪录，添加新记录
                    Wenxian.Items.Remove(Wenxian.SelectedItem);

                    Wenxian.Items.Add(ItemRow);

                }
            };
            menu.Items.Add(menuItem);
            Wenxian.ContextMenu = menu;
        }

        // 超链接至文件
        private void Hyper_Click(object sender, RoutedEventArgs e)
        {
            DataSelection dataSelection = new DataSelection((DataRow)Wenxian.SelectedItem);
            dataSelection.HyperGet();
        }

        private void ColorMode_Click(object sender, RoutedEventArgs e)
        {
            ColorChange();
        }

        // 硬编码设置DataRow内容
        private DataRow AddContent(Dictionary<string, string> keyValues)
        {
            
            return
                new DataRow
                {
                    Title = keyValues["Title"],
                    Author = keyValues["Author"],
                    Period = keyValues["Period"] == null ? "" : keyValues["Period"],
                    Volume = keyValues["Volume"],
                    Keyword = keyValues["Keyword"],
                    Year = keyValues["Year"],
                    FilePath = keyValues["FilePath"] == null ? "" : keyValues["FilePath"],
                };
        }

        // 颜色改变的相关类
        private void ColorChange()
        {
            DarkColor darkColor = new DarkColor();
            MorningColor morningColor = new MorningColor();

            int StyleChange(int mode)
            {
                //创建一个style对象
                Style style = new Style(typeof(DataGridColumnHeader));
                Setter setter;
                if (mode == 0)
                {
                    //创建一个setter对象，设置背景色为蓝色
                    setter = new Setter(BackgroundProperty, new SolidColorBrush(darkColor.DataGridColor));
                }
                else
                {
                    //创建一个setter对象，设置背景色为白色
                    setter = new Setter(BackgroundProperty, new SolidColorBrush(morningColor.DataGridColor));
                }
                Setter setter1 = new Setter(BorderBrushProperty, Brushes.Black);
                Setter setter2 = new Setter(BorderThicknessProperty, new Thickness(0, 1, 0, 1));
                Setter[] setter3 =
                {
                    setter,
                    setter1,
                    setter2
                };
                foreach (Setter i in setter3)
                {
                    style.Setters.Add(i);
                };
                //将style对象赋值给datagrid对象的ColumnHeaderStyle属性
                Wenxian.ColumnHeaderStyle = style;

                return 0;
            }
            
            // 如果识别到早间模式，则改为夜间模式
            if (Mode == 0)
            {
                // 修改Grid和DataGrid颜色
                Grid1.Background = new SolidColorBrush(darkColor.GridColor);
                Wenxian.Background = new SolidColorBrush(darkColor.DataGridColor);

                Image image = new Image();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/月亮.png", UriKind.Absolute));
                ColorMode.Content = image;

                StyleChange(Mode);
                // 将Mode改为夜间模式
                Mode = 1;
            }
            // 如果识别到夜间模式，则改为早间模式
            else if (Mode == 1)
            {
                // 修改Grid和DataGrid颜色
                Grid1.Background = new SolidColorBrush(morningColor.GridColor);
                Wenxian.Background = new SolidColorBrush(morningColor.DataGridColor);

                Image image = new Image();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/太阳.png",UriKind.Absolute));
                ColorMode.Content = image;

                StyleChange(Mode);
                // 将Mode改为早间模式
                Mode = 0;
            }
        }
    }

    public class DataRow{
        public string Title { get; set; }
        public string Author { get; set; }
        public string Volume { get; set; }
        public string Period { get; set; }

        public string Keyword { get; set; }
        public string Year { get; set; }

        public string FilePath { get; set; }
    }
}
