using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEngine.Entities
{
    public class Rule
    {
        public string operand { get; set; }
        public OperatorExp operatorExp { get; set; }
        public string value { get; set; }
        public ValueTuple valueType { get; set; }
        public bool status { get; set; }
    }

    public enum OperatorExp{

        MoreThan,
        NotMoreThan,
        LessThan,
        NotLessThan,
        NotEqual,
        Equal,
        NotInFuture,
        NotInPast

    }

    
}
