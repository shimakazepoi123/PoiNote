using System.Collections.Generic;

namespace WpfApp1.Must.Mark
{
    internal class MarkdownAbout
    {

        public Dictionary<string,string> TitleAdd()
        {
            return new Dictionary<string, string>() 
            {
                {"一级标题","#" },
                {"二级标题","##" },
                {"三级标题","###" },
                {"四级标题","####" },
                {"五级标题","#####" },
            };
        }

        public Dictionary<string,string> Format() 
        {
            return new Dictionary<string, string>
            {
                {"**","粗体" },
                {"*","斜体" },
                {"~~","删除线" },
                {"`","行内代码" },
                {"```","代码块" },
                {"[","链接" },
                {"![","图片" },
                {">","引用" },
                {"-","无序列表" },
                {"---","分割线" },
                {"\\","转义" },
            };
        }
    }
}
