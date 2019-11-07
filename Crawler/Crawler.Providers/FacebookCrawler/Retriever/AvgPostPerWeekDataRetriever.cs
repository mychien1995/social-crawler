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
    public class AvgPostPerWeekDataRetriever : IHtmlPropertyRetriever
    {
        public object Retrieve(IWebDriver driver, DataInputBase input, Dictionary<string, object> parameters)
        {
            int count = 0;
            var postdDateXPathQuery = parameters["XPathQuery"].ToString();
            var fbInput = input as FacebookCrawlerDataInput;
            var startTime = (fbInput.StartDate.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            var endTime = (fbInput.EndDate.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            var result = new List<int>();
            int maxRetryCount = 20;
            int retryCount = 0;
            while (true)
            {
                driver.ScrollToBottom();
                Thread.Sleep(3000);
                var postedDateElements = driver.FindElements(By.XPath(postdDateXPathQuery));
                var enough = false;
                int newItemInBatch = 0;
                foreach (var postedDateEl in postedDateElements)
                {
                    var utcTime = int.Parse(postedDateEl.GetAttribute("data-utime"));
                    if (!result.Any(x => x == utcTime) && utcTime >= startTime && utcTime <= endTime)
                    {
                        newItemInBatch++;
                        result.Add(utcTime);
                    }
                    if (utcTime < startTime)
                    {
                        enough = true;
                        break;
                    }
                }
                if (enough)
                {
                    count = result.Count;
                    break;
                }
                if (newItemInBatch == 0)
                {
                    retryCount++;
                }
                if (retryCount == maxRetryCount) break;
            }
            return count;
        }
    }
}
