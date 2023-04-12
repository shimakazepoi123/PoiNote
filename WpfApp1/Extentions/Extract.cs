using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WpfApp1.Must.StringCheck;
using System.Text;
using System.Linq;
using static WpfApp1.Must.DataAbout.NoteBase.BaseClass;
using WpfApp1.Must.DataAbout;

namespace WpfApp1.Extentions
{
    class Extract
    {

        public Extract() { }
        public Extract(string pattern_import,string path)
        {
            Pattern = pattern_import;
            Path = path;
        }
        protected string Pattern { get; set; }
        protected string Path { get; set; }

        protected enum ArticleType
        {
            Journal,
            Conference,
            Thesis,
        }
        
        /// <summary>
        /// 根据不同模式提取不同的部分
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /*N是卷,V是期*/
        // 该方法需要重写
        public Dictionary<string,string> ExtractNote()
        {
            byte[] filePaths = File.ReadAllBytes(Path).Take(20).ToArray();
            /*检测编码*/
            Encoding encoding = StringCode.CheckCode(filePaths);
            string[] a = File.ReadAllLines(Path,encoding);
            Dictionary<string, string> WenXian = new Dictionary<string, string>();
            foreach (string b in a)
            {
                GroupCollection group1 = Regex.Match(b, Pattern).Groups;

                foreach (Group group2 in group1)
                {
                    if (group2.Success is true & group2.Name != "0")
                    {
                        if (group2.Name == "Author")
                        {
                            string Author = string.Empty;
                            Author = group2.Value;
                            try
                            {
                                WenXian.Add("Author", Author);
                            }
                            catch
                            {
                                string Author1;
                                Author1 = WenXian["Author"]+","+ group2.Value;
                                WenXian["Author"] = Author1;
                            }
                        }
                        // 只需要直接往ArticaleType添加新类型即可
                        else if (group2.Name == "Type")
                        {
                            string d = group2.Value.Split(" ")[1];
                            ArticleType articleType = (ArticleType)Enum.Parse(typeof(ArticleType), d);
                            switch (articleType)
                            {
                                case ArticleType.Journal :
                                    WenXian["Type"] = "期刊";
                                    break;
                                case ArticleType.Conference:
                                    WenXian["Type"] = "会议";
                                    break;
                                case ArticleType.Thesis:
                                    WenXian["Type"] = "硕士论文";
                                    break;
                                default:
                                    WenXian["Type"] = "未知";
                                    break;

                            }
                        }
                        else
                        {
                            try
                            {
                                WenXian.Add(group2.Name, group2.Value);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            if (WenXian["Type"] != "期刊") { WenXian.Add("Volume", ""); WenXian.Add("Period", ""); }
            else
            {
                try
                {
                    WenXian.Add("Volume", "");
                }
                catch
                {

                }
            }
            return WenXian;
        }

        public Dictionary<string, string> ExtractNote1()
        {

            byte[] filePaths = File.ReadAllBytes(Path).Take(15).ToArray();
            /*检测编码*/
            Encoding encoding = StringCode.CheckCode(filePaths);
            /*读取文件*/
            string[] lines = File.ReadAllLines(Path,encoding);

            /*tagPairs指明不同类型文献的导入方式*/
            Dictionary<string, TagExtract> tagPairs = (Pattern == "NCBI") ? NCBIExtract.TagDict1() : EndNoteExtract.TagDict1();

            /*用于存储数据*/
            Dictionary<string, string> WenXian = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                string[] tagContent = (line.Split(" - ")[0] == "") ? line.Split(" - ") : line.Split(" ");

                // 一个是标签，一个是内容
                string tag = tagContent[0];
                string content = tagContent[1];

                try
                {
                    if (tagPairs[tag].Type == "Type")
                    {
                        string d = content.Split(" ")[0];
                        ArticleType articleType = (ArticleType)Enum.Parse(typeof(ArticleType), d);
                        switch (articleType)
                        {
                            case ArticleType.Journal:
                                WenXian["Type"] = "期刊";
                                break;
                            case ArticleType.Conference:
                                WenXian["Type"] = "会议";
                                break;
                            case ArticleType.Thesis:
                                WenXian["Type"] = "硕士论文";
                                break;
                            default:
                                WenXian["Type"] = "未知";
                                break;

                        }
                    }
                    WenXian.Add(tagPairs[tag].Type, content);
                }
                catch
                {
                    string change = WenXian[tagPairs[tag].Type];
                    change += "," + content;
                    WenXian[tagPairs[tag].Type] = change;
                }
            }

            return WenXian;

        }
    }
};
