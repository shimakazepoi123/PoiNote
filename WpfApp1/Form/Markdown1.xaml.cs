using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Must.Mark;
using Mustachio;
using System.Threading;
using System.Dynamic;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.Reflection.Metadata;
using Markdig;

namespace WpfApp1.Form
{
    /// <summary>
    /// Markdown1.xaml 的交互逻辑
    /// </summary>
    public partial class Markdown1 : Window
    {
        const string FileHTML = "D:\\PoiNote\\WpfApp1\\WpfApp1\\HTML\\Text.html";
        const string Templete = "D:\\PoiNote\\WpfApp1\\WpfApp1\\HTML\\Templete.html";
        public Markdown1()
        {
            InitializeComponent();
            Markdown.Text = "请在此处输入文字";
            Markdown.Foreground = Brushes.Gray;
            Markdown.TextChanged += new TextChangedEventHandler(Markdown_TextChanged);
            Markdown.SizeChanged += new SizeChangedEventHandler(Markdown_SizeChanged);

            Navigator.AllowDrop = false;
            try
            {
                File.Copy(Templete, FileHTML);
            }
            catch
            {
                
            }
            Navigator.Source = new Uri(FileHTML);

        }


        // 保存输入的文字
        protected string MarkdownText;
        // 主页面的颜色
        public Brush ForMatColor { get { return Format.Background; } set { Format.Background = value; } }

        // 计划添加行数、包括换行符的字符数等内容【通过点击显示页面】
        private void Markdown_TextChanged(object sender, TextChangedEventArgs args)
        {
            // 获取输入的文字，并且显示字数[不包括换行符]
            MarkdownText = Markdown.Text;
            Count.Content = MarkdownText.Replace("\n","").Length.ToString();

            string html = string.Empty;

            foreach (var item in MarkdownText.Split("\n")) {
                Markdown2 markdown1 = new Markdown2(item);
                html += markdown1.ToMarkdown();
            }

            // 模板
            string tempalte_text = File.ReadAllText(Templete);
            var option = new ParsingOptions()
            {
                DisableContentSafety = true,
            };
            var template = Parser.Parse(tempalte_text,option);

            dynamic model = new ExpandoObject();
            model.text = html;
            model.title = "Text";
            var content = template(model);

            File.WriteAllText(FileHTML, content);

            new Thread(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                   Navigator.Reload();

                }));
            }).Start();

        }

        // 点击输入框时，清空默认文字，同时改变颜色
        private void Markdown_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (Markdown.Text == "请在此处输入文字")
            {
                Markdown.Text = "";
            }
            Markdown.Foreground = Brushes.Black;
        }

        // 调整ScrollViewer的高度（随窗体改变而改变）
        private void Markdown_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            ScrollViewer1.Height = ActualHeight - Format.Height - UnderCount.Height - 35;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            using (var file = new SaveFileDialog())
            {
                file.Filter = "HTML文件|*.html|Markdown文件|*.md";
                file.ShowDialog();
                if (file.FileName != "")
                {
                    File.Move(FileHTML, file.FileName);
                }
            }
        }

        // Header相关事件
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            MarkdownAbout markdownAbout = new MarkdownAbout();

            FactoryFun factoryFun = new(markdownAbout.TitleAdd()[menuItem.Header.ToString()], " ", Markdown.SelectedText);
            Markdown.SelectedText = factoryFun.Header();
        }

        // 添加颜色
        private void AddColor(object sender, RoutedEventArgs e)
        {
            string html = "<font color='yellow'>";
            string html1 = "</font>";
            FactoryFun factoryFun = new(html,html1, Markdown.SelectedText);
            Markdown.SelectedText = factoryFun.Color();
        }

        // 

        // 关闭窗体的相关事件
        protected override void OnClosed(EventArgs e)
        {
            File.Delete(FileHTML);
            base.OnClosed(e);
        }
    }
}


class FactoryFun
{
    /// <summary>
    /// 工厂函数，包装格式的相关信息
    /// TextLeft是左侧文字
    /// TextLeft是右侧文字
    /// </summary>
    public FactoryFun(string textLeft, string textRight, string middleCotent) { TextLeft = textLeft; TextRight = textRight; MiddleCotent = middleCotent; }
    private string TextLeft { get; set; }
    private string TextRight { get; set; }

    // 该处值一般为Markdown文本框的Text
    private string MiddleCotent { get; set; }

    // 基础函数，此后的函数以这个为本
    private string Funtion1(string one,string two,string three)
    {
        return string.Format("{0}{1}{2}", one,two,three);
    }

    private string IfSelect(Func<string,string,string,string> Funtion1, bool ifTitle = false)
    {
        if (MiddleCotent != "")
        {
            return ifTitle ? Funtion1(TextLeft, TextRight, MiddleCotent) : Funtion1(TextLeft, MiddleCotent, TextRight);
        }
        return null;
    }

    public string Header()
    {
        return IfSelect(Funtion1, true);
    }

    public string Color()
    {
        return IfSelect(Funtion1);
    }
}

struct FormatAbout
{
    public FormatAbout(){}
    string Color = "";
}
