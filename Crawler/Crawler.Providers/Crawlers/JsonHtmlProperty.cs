using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.Crawlers
{
    public class JsonHtmlProperty
    {
        public string Property { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public string RetrieverType { get; set; }
    }
}
