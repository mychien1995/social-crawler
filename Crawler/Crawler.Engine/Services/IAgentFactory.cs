using Crawler.Core;
using Crawler.Engine.Configurations;
using Crawler.Engine.Helpers;
using Crawler.Providers.FacebookCrawler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Crawler.Engine.Services
{
    public interface IAgentFactory
    {
        SocialTrackingAgent GetAgent(string name);
    }
}
