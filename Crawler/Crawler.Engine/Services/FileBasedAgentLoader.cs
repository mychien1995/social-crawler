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
    public class FileBasedAgentLoader : IAgentLoader
    {
        public Dictionary<string, SocialTrackingAgent> LoadAgents()
        {
            LoadAssemblies();
            var result = new Dictionary<string, SocialTrackingAgent>();
            var availableAgents = new Dictionary<string, Type>();
            var availableConfigurations = new Dictionary<string, Type>();
            var assemblies = AssemblyHelper.GetAssemblies(x => x.Name.StartsWith("Crawler.")).ToArray();
            var agents = AssemblyHelper.GetClasses(assemblies, x => typeof(SocialTrackingAgent).IsAssignableFrom(x) && !x.IsAbstract);
            var agentConfigurations = AssemblyHelper.GetClasses(assemblies, x => typeof(ISocialAgentConfiguration).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);
            foreach (var item in agents)
            {
                availableAgents.Add(item.Name, item);
            }
            foreach (var item in agentConfigurations)
            {
                availableConfigurations.Add(item.Name, item);
            }
            var agentConfigurationFolderPath = ConfigurationManager.AppSettings["AgentFolder"];
            foreach (string file in Directory.EnumerateFiles(agentConfigurationFolderPath, "*.json"))
            {
                string contents = File.ReadAllText(file);
                JsonAgentObject agentObject;
                if (contents.TryParseToObject(out agentObject))
                {
                    var agentTypeName = agentObject.AgentType;
                    var agentType = availableAgents[agentTypeName];

                    var configurationTypeName = agentObject.ConfigurationType;
                    var configurationType = availableConfigurations[configurationTypeName];
                    var agentConfiguration = agentObject.Configuration;
                    object agentConfigurationObj;
                    if (agentConfiguration.ToString().TryParseToObject(configurationType, out agentConfigurationObj))
                    {
                        var agent = (SocialTrackingAgent)Activator.CreateInstance(agentType, agentConfigurationObj);
                        result.Add(agent.GetType().Name, agent);
                    }
                }
            }
            return result;
        }

        private void LoadAssemblies()
        {
            new FacebookCrawlerConfiguration();
        }
    }
}
