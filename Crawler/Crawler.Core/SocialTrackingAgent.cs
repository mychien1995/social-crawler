using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public abstract class SocialTrackingAgent
    {
        public string Platform;
        public SocialTrackingAgent(string platform)
        {
            this.Platform = platform;
        }
        public abstract DataOutputBase RetrieveData(DataInputBase input);
    }
}
