using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class BulkRequest
    {
        public BulkRequest()
        {
            Inputs = new List<DataInputBase>();
        }

        public List<DataInputBase> Inputs { get; set; }
    }
}
