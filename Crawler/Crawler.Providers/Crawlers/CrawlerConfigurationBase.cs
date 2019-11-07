using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.Crawlers
{
    public class CrawlerConfigurationBase
    {
        public string LoginUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonProperty("Properties")]
        public JsonHtmlProperty[] HtmlProperties { get; set; }
    }
}
