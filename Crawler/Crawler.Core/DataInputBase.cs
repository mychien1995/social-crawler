using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class DataInputBase
    {
        public string Agent { get; set; }
        public string AccountName { get; set; }
        public string Platform { get; set; }
        public string Region { get; set; }
        public string Owner { get; set; }
    }
}
