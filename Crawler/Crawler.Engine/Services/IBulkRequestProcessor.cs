using Crawler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine.Services
{
    public interface IBulkRequestProcessor
    {
        BulkRequestResponse Process(BulkRequest bulkRequests);
    }

    public class DefaultBulkRequestProcessor : IBulkRequestProcessor
    {
        private int BatchCount = 1;
        public BulkRequestResponse Process(BulkRequest bulkRequest)
        {
            var result = new BulkRequestResponse();
            var agentFactory = new FileBasedAgentFactory();
            var inputCount = bulkRequest.Inputs.Count;
            for (var i = 0; i < inputCount; i += BatchCount)
            {
                var taskList = new List<Task>();
                for (int j = 0; j < BatchCount; j++)
                {
                    if (i + j < inputCount)
                    {
                        var input = bulkRequest.Inputs[i + j];
                        var task = Task.Run(() =>
                        {
                            try
                            {
                                var agent = agentFactory.GetAgent(input.Agent);
                                var output = agent.RetrieveData(input);
                                result.Add(input, output);
                            }
                            catch (Exception ex)
                            {

                            }
                        });
                        taskList.Add(task);
                    }
                }
                Task.WaitAll(taskList.ToArray());
            }
            return result;
        }
    }
}
