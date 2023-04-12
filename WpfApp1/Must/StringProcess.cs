using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Must
{
    /// <summary>
    /// 对String进行处理，以让其符合Markdown格式，最终转换为HTML
    /// </summary>
    public class StringProcess
    {
        public StringProcess() { }
        public StringProcess(string text, int titleClass=0) {
            Text = text;
            TitleClass = titleClass;
        }

        protected string Text { get; set; }
        protected int TitleClass { get; set; }

        public string TitleAdd()
        {
            var bulider = new StringBuilder(TitleClass);

            for (int i = 0; i < TitleClass; i++) { bulider.Append("#"); }

            string text_join = bulider.ToString() + " " + Text;

            return text_join;
        }


    }
}
