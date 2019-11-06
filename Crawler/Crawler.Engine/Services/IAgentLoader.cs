using Crawler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine.Services
{
    public interface IAgentLoader
    {
        Dictionary<string, SocialTrackingAgent> LoadAgents();
    }
}
