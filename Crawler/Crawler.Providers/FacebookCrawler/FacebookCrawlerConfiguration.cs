using Crawler.Core;
using Crawler.Providers.Crawlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler
{
    public class FacebookCrawlerConfiguration : CrawlerConfigurationBase, ISocialAgentConfiguration
    {
        public string LoginUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailXPathQuery { get; set; }
        public string PasswordXPathQuery { get; set; }
        public string LoginButtonXPathQuery { get; set; }
        public string FollowersXPathQuery { get; set; }
        public string PostedDateXPathQuery { get; set; }
        public string PostedDateXPathAttribute { get; set; }
    }
}
