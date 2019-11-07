using Crawler.Engine.Services;
using Crawler.Providers.FacebookCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Presesentation
{
    class Program
    {
        static void Main(string[] args)
        {

            IAgentLoader agentLoader = new FileBasedAgentLoader();
            var agents = agentLoader.LoadAgents();
            if (agents.Count > 0)
            {
                var input = new FacebookCrawlerDataInput();
                input.EndDate = DateTime.Now;
                input.StartDate = DateTime.Now.AddDays(-7);
                input.PageUrl = "https://www.facebook.com/grantthorntonportugal/";
                agents.FirstOrDefault().Value.RetrieveData(input);
            }
            Console.ReadLine();
        }
    }
}
