using Crawler.Core;
using Crawler.Providers.Crawlers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler.Retriever
{
    public class PostDateListDataRetriever : IHtmlPropertyRetriever
    {
        public object Retrieve(IWebDriver driver, DataInputBase input, Dictionary<string, object> parameters)
        {
            var postdDateXPathQuery = parameters["XPathQuery"].ToString();
            driver.ScrollToBottom();
            Thread.Sleep(5000);
            var postedDateElements = driver.FindElements(By.XPath(postdDateXPathQuery));
            var result = new List<int>();
            foreach (var postedDateEl in postedDateElements)
            {
                var utcTime = postedDateEl.GetAttribute("data-utime");
                result.Add(int.Parse(utcTime));
            }
            return result;
        }
    }
}
