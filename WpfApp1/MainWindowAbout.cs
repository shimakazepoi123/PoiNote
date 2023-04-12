using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Extentions;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows;
using System.IO;
using System.Data.SQLite;
using Control = System.Windows.Controls.Control;
using ToolTip = System.Windows.Controls.ToolTip;
using Clipboard = System.Windows.Clipboard;
using Label = System.Windows.Controls.Label;
using System.Diagnostics;
using System.Windows.Media;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace WpfApp1
{
    class MainWindowAbout
    {
        public MainWindowAbout() { }

        public MainWindowAbout(string pattern, string path)
        {
            Pattern = pattern;
            Path = path;
        }

        protected string Pattern { get; set; }
        protected string Path { get; set; }

        public Dictionary<string, string> ExtractWenXian()
        {
            Extract extract = new Extract(Pattern, Path);
            return extract.ExtractNote();
        }
    }

    class SQLAbout
    {
        public SQLAbout() { }
        public SQLAbout(string path, string data) { filePath = path; data1 = data; }

        protected string filePath { get; set; }
        protected string data1 { get; set; }

        protected SQLRead sqlRead { get { SQLRead sQLRead = new SQLRead(filePath); return sQLRead; } }

        /// <summary>
        /// 显示列表中的全部内容
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable SQLSearch()
        {
            return sqlRead.SQL_Search("SELECT * FROM Wenxian", "read");
        }

        /// <summary>
        /// 创建SQL表，具体名称需要在SQLRead文件中修改
        /// </summary>
        public void SQLCreate() => sqlRead.SQL_Create();

        /// <summary>
        /// 往SQL中添加数据
        /// </summary>

        public void SQLTable() => sqlRead.SQL_CreatDatabase(filePath);

        public void SQLUpdate(string b) { sqlRead.SQL_Other(string.Format("INSERT INTO Wenxian VALUES({0})", b)); }

        public IEnumerable SQLGetHeder() { return sqlRead.SQL_Search("PRAGMA TABLE_INFO(Wenxian)", "read"); }
    }

    /// <summary>
    /// 文献上移下移的相关类
    /// </summary>
    class DataViewFunction
    {
        public DataViewFunction() { }
        /// <summary>
        /// 需要修改的DataGrid
        /// </summary>
        /// <param name="data"></param>
        public DataViewFunction(DataGrid data)
        {
            dataGrid = data;
        }

        protected DataGrid dataGrid { get; set; }

        /// <summary>
        /// 操作文献行的移动
        /// </summary>
        /// <param name="moveorder">操作的类型，上移为up，下移为down</param>
        public void DataGridCellMove(string moveorder)
        {
            object now_row = dataGrid.SelectedItem;
            int row = dataGrid.SelectedIndex;
            int count = (moveorder == "up") ? 0 : dataGrid.Items.Count - 1;
            int num = (moveorder == "up") ? row - 1 : row + 1;

            if (now_row is null || row == count)
            {
                System.Windows.MessageBox.Show("未选中任何数据", "警告");
            }
            else
            {
                dataGrid.Items.Remove(now_row);
                dataGrid.Items.Insert(num, now_row);
                dataGrid.SelectedItem = dataGrid.Items[num];
            }
        }
    }


    /// <summary>
    /// 按钮点击事件
    /// </summary>
    class ButtonEvent
    {
        public ButtonEvent() { }
        public ButtonEvent(List<Dictionary<string, string>> a) { b = a; }

        // 文献提取内容KeyValue对的集合，以List表示
        protected List<Dictionary<string, string>> b { get; set; }

        // 将参考文献复制到剪贴板中
        public void ExtractNote(object sender, RoutedEventArgs e)
        {
            string refer = string.Empty;

            foreach (Dictionary<string, string> i in b)
            {
                refer += i["Volume"] == "" ? string.Format("[1]{0}.{1}[J].{2}.{3}({4}):{5}\n", i["Author"], i["Title"], i["Jounral"], i["Year"], i["Period"], i["Page"]) : string.Format(
                    "[1]{0}.{1}[J].{2}.{3},{4}({5}):{6}\n", i["Author"], i["Title"], i["Jounral"], i["Year"], i["Volume"], i["Period"], i["Page"]);
            }

            Clipboard.SetText(refer);
            System.Windows.MessageBox.Show("已经复制到剪贴板中！");
        }

        public void SaveNote(object sender, RoutedEventArgs e)
        {
            using (SaveFileDialog fileDialog = new())
            {
                fileDialog.Filter = "数据库文件 (*.db)|*.db";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;
                    SQLAbout sQLAbout = new(fileName, "null");
                    try
                    {
                        if (File.Exists(fileName) is false)
                        {
                            sQLAbout.SQLTable();
                            sQLAbout.SQLCreate();
                        }
                    }
                    catch
                    {

                    }

                    IEnumerable objects = sQLAbout.SQLGetHeder();
                    string key = string.Empty;
                    foreach (Dictionary<string, string> r in b)
                    {
                        string data = string.Empty;
                        foreach (SQLiteDataReader obj in objects)
                        {
                            try
                            {
                                data += string.Format("'{0}',", r[obj.GetString(1)]);
                            }
                            catch
                            {
                                data += "' ',";
                            }
                        }
                        data = data.Remove(data.Length - 1);
                        sQLAbout.SQLUpdate(data);
                    }

                }

            }
        }

    }

    // 为主界面按钮添加ToolTip
    class ToolTipAdd
    {
        public ToolTipAdd() { }
        public ToolTipAdd(Control[] controls, string[] name) { Controls = controls; Name = name; }

        protected Control[] Controls;
        protected string[] Name;

        public void ToolTipAdd1()
        {
            for (int i = 0; i < Controls.Length; i++)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = Name[i];
                Controls[i].ToolTip = toolTip;
            };
        }
    }

    class DataSelection
    {
        public DataSelection(DataRow dataRow) { DataRow1 = dataRow; }
        protected DataRow DataRow1 { get; set; }

        public void HyperGet()
        {
            string filePath = DataRow1.FilePath;
            int pathNum = filePath.Split("\\").Length - 1;

            Directory.SetCurrentDirectory(string.Join("\\", filePath.Split("\\")[0..pathNum]));
            Process.Start(new ProcessStartInfo("File:\\" + filePath) { UseShellExecute = true });
        }


    }

    class TagAdd1
    {
        public TagAdd1(StackPanel stackPanel) { StackPanel1 = stackPanel; }
        protected StackPanel StackPanel1 { get; set; }
        public void TagToShow(int num, Tuple<string, string> tuple)
        {
            StackPanel panel = new StackPanel()
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal ,
                Margin = new Thickness(0,6,0,0),
            };
            Label tag = new Label()
            {
                Content = tuple.Item1,
                FontSize = 15,
                Name = "TagLeft",
            };
            Label num1 = new Label()
            {
                Content = tuple.Item2,
                Foreground = new SolidColorBrush(Color.FromRgb(52, 51, 51)),
                FontSize = 15,
                HorizontalAlignment = HorizontalAlignment.Center,
                Name = "Number",
                Margin = new Thickness(5, 0, 0, 0),
            };
            Grid.SetRow(panel,num+1);
            Grid.SetColumn(panel,1);

            panel.Children.Add(tag);
            panel.Children.Add(num1); 

            StackPanel1.Children.Add(panel);
        }
    }

}
