using Crawler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler
{
    public class FacebookCrawlerDataInput : DataInputBase
    {
        public string PageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FacebookCrawlerDataInput()
        {
            this.Platform = SocialPlatform.Facebook;
        }
    }
}
