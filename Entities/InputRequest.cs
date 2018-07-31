using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Entities
{
    public class InputRequest
    {
        public List<DataStream> dataStream { get; set; }
    }

    public class DataStream
    {
        public string signal { get; set; }
        public string value { get; set; }
        public string value_type { get; set; }
    }


}
