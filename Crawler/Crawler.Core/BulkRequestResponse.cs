using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class BulkRequestResponse
    {
        private object _lock = new object();
        public BulkRequestResponse()
        {
            Responses = new List<BulkRequestResponseItem>();
        }

        public void Add(DataInputBase dataInput, DataOutputBase dataOutput)
        {
            lock (_lock)
            {
                Responses.Add(new BulkRequestResponseItem(dataInput, dataOutput));
            }
        }
        public List<BulkRequestResponseItem> Responses { get; set; }
    }

    public class BulkRequestResponseItem
    {
        public BulkRequestResponseItem()
        {

        }
        public BulkRequestResponseItem(DataInputBase dataInput, DataOutputBase dataOutput)
        {
            Input = dataInput;
            Output = dataOutput;
        }
        public DataInputBase Input { get; set; }
        public DataOutputBase Output { get; set; }
    }
}
