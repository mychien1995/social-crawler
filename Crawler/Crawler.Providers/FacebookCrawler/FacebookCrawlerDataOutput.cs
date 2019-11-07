using Crawler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler
{
    public class FacebookCrawlerDataOutput : DataOutputBase
    {
        public int FollowersCount { get; set; }
        public decimal AvgPostPerWeek { get; set; }
    }
}
