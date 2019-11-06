using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine.Configurations
{
    public class JsonAgentObject
    {
        [JsonProperty("agentType")]
        public string AgentType { get; set; }
        [JsonProperty("configurationType")]
        public string ConfigurationType { get; set; }
        [JsonProperty("configuration")]
        public object Configuration { get; set; }
    }
}
