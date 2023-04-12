using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp1.Must.DataAbout.NoteBase;

namespace WpfApp1.Must.DataAbout
{

    /// <summary>
    /// 存储NCBI文件中提取的内容
    /// </summary>
    internal class NCBIExtract : BaseClass
    {
        public new static Dictionary<string,TagExtract> TagDict1()
        {
            return new Dictionary<string, TagExtract>()
            {
                {"AU",new("Author") },
                {"LID",new("DOI") },
                {"TI",new("Title") },
                {"PT",new("Type") },
                {"JT",new("Jounral") },
                {"OT",new("Keyword") },
                {"DP",new("Year") },
                {"PG",new("Page") },
                {"DP",new("Year") },
                {"IP",new("Period") },
            };
        }
    }
}
