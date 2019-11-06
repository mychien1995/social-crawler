using Crawler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler
{
    public class FacebookCrawlerConfiguration : ISocialAgentConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailXPathQuery { get; set; }
        public string PasswordXPathQuery { get; set; }
        public string LoginButtonXPathQuery { get; set; }
    }
}
