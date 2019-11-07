using Crawler.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.Crawlers
{
    public interface IHtmlPropertyRetriever
    {
        object Retrieve(IWebDriver driver, DataInputBase input, Dictionary<string, object> parameters);
    }
}
