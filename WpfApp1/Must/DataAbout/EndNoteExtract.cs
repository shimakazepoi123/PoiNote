using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Must.DataAbout.NoteBase;

namespace WpfApp1.Must.DataAbout
{
    internal class EndNoteExtract : BaseClass
    {
        public new static Dictionary<string, TagExtract> TagDict1()
        {
            Dictionary<string, TagExtract> keyValuePairs = new Dictionary<string, TagExtract>()
            {
                { "%0",new("Type") },
                { "%A",new("Author") },
                { "%T",new("Title") },
                { "%J",new("Jounral") },
                { "%D",new("Year")},
                { "%N",new("Period")},
                { "%P",new("Page") },
                { "%K",new("Keyword")},
                { "%V",new("Volume")},
                { "%R",new("DOI")},
            };
            return keyValuePairs;
        }
    }
}
