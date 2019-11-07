using Crawler.Core;
using Crawler.Providers.Crawlers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler.Retriever
{
    public class FollowersDataRetriever : IHtmlPropertyRetriever
    {
        public object Retrieve(IWebDriver driver, DataInputBase input, Dictionary<string, object> parameters)
        {
            var followerXpathQuery = parameters["XPathQuery"].ToString();
            IWebElement followersLabel = driver.FindElement(By.XPath(followerXpathQuery));
            var followerText = followersLabel.Text.Replace(".", string.Empty).Replace(",", string.Empty);
            var followersPart = followerText.Split(' ').FirstOrDefault(x =>
            {
                int tmp;
                return int.TryParse(x, out tmp);
            });
            var followerCount = int.Parse(followersPart);
            return followerCount;
        }
    }
}
