using Crawler.Core;
using Crawler.Engine.Configurations;
using Crawler.Engine.Helpers;
using Crawler.Providers.FacebookCrawler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine.Services
{
    public class FileBasedAgentFactory : IAgentFactory
    {
        private static Dictionary<string, Tuple<Type, object>> _agentSkeletons;
        private static ConcurrentDictionary<string, List<SocialTrackingAgent>> _agentsPool;
        public FileBasedAgentFactory()
        {
        }

        public SocialTrackingAgent GetAgent(string name)
        {
            if (_agentSkeletons == null)
            {
                _agentSkeletons = GetAgents();
            }
            if (_agentsPool == null)
            {
                _agentsPool = new ConcurrentDictionary<string, List<SocialTrackingAgent>>();
            }
            SocialTrackingAgent agent = null;
            if (_agentsPool.ContainsKey(name))
            {
                agent = _agentsPool[name].FirstOrDefault(x => !x.IsActive);
            }
            else _agentsPool.TryAdd(name, new List<SocialTrackingAgent>());
            if (agent == null)
            {
                var agentConf = _agentSkeletons[name];
                agent = (SocialTrackingAgent)Activator.CreateInstance(agentConf.Item1, agentConf.Item2);
                _agentsPool[name].Add(agent);
            }
            return agent;
        }

        public Dictionary<string, Tuple<Type, object>> GetAgents()
        {
            LoadAssemblies();
            var result = new Dictionary<string, Tuple<Type, object>>();
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
                        result.Add(agentObject.Name, new Tuple<Type, object>(agentType, agentConfigurationObj));
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
