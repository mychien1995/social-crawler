using Crawler.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.Crawlers
{
    public class HtmlProperty
    {
        public HtmlProperty(string propertyName, IHtmlPropertyRetriever dataRetriever, Dictionary<string, object>  parameters)
        {
            Property = propertyName;
            DataRetriever = dataRetriever;
            Parameters = parameters;
        }

        public object GetValue(IWebDriver driver, DataInputBase input)
        {
            return DataRetriever.Retrieve(driver, input, Parameters);
        }
        public string Property { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        private IHtmlPropertyRetriever DataRetriever { get; set; }
    }
}
