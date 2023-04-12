using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Must.DataAbout.NoteBase
{
    internal class BaseClass
    {
        public class TagExtract
        {
            public TagExtract() { }
            public TagExtract(string type) { Type = type; }

            public string Type { get; set; }
        }
        public static Dictionary<string, TagExtract> TagDict1()
        {
            return new Dictionary<string, TagExtract>();
        }
    }
}
