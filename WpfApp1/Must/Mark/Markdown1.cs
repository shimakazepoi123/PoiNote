using Markdig;

namespace WpfApp1.Must.Mark
{
    public class Markdown2
    {
        public Markdown2() { }
        public Markdown2(string text) { Text = text; }

        protected string Text { get; set; }

        public string ToMarkdown()
        {
            return Markdown.ToHtml(Text);
        }
    }
}
