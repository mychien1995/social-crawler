using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public abstract class SocialTrackingAgent
    {
        public ISocialAgentConfiguration Configuration;
        public string Platform;
        public SocialTrackingAgent(ISocialAgentConfiguration configuration, string platform)
        {
            this.Configuration = configuration;
            this.Platform = platform;
        }
        public abstract DataOutputBase RetrieveData(DataInputBase input);
    }
}
