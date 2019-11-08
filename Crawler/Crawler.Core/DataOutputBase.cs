using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class DataOutputBase
    {
        public DataOutputBase()
        {
            Messages = new List<string>();
        }
        public List<string> Messages { get; set; }
        public Dictionary<string, object> PropertyBag { get; set; }
    }
}
