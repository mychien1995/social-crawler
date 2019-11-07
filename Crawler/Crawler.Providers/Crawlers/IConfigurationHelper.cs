using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.Crawlers
{
    public interface ICrawlerConfigurationHelper
    {
        List<HtmlProperty> LoadHtmlProperties(CrawlerConfigurationBase crawlerConfiguration);
    }

    public class CrawlerConfigurationHelper : ICrawlerConfigurationHelper
    {
        public List<HtmlProperty> LoadHtmlProperties(CrawlerConfigurationBase crawlerConfiguration)
        {
            var result = new List<HtmlProperty>();
            foreach (var item in crawlerConfiguration.HtmlProperties)
            {
                var dataRetriever = (IHtmlPropertyRetriever)Activator.CreateInstance(Type.GetType(item.RetrieverType, true, true));
                var prop = new HtmlProperty(item.Property, dataRetriever, item.Parameters);
                result.Add(prop);
            }
            return result;
        }
    }
}
