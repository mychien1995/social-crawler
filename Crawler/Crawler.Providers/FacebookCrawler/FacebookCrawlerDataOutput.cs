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
        public int? FollowersCount
        {
            get
            {
                if (this.PropertyBag == null || !this.PropertyBag.ContainsKey("Followers") || this.PropertyBag["Followers"] == null) return null;
                return int.Parse(this.PropertyBag["Followers"].ToString());
            }
        }
        public decimal? AvgPostPerWeek
        {
            get
            {
                if (this.PropertyBag == null || !this.PropertyBag.ContainsKey("AvgPostPerWeek") || this.PropertyBag["AvgPostPerWeek"] == null) return null;
                return decimal.Parse(this.PropertyBag["AvgPostPerWeek"].ToString());
            }
        }
    }
}
