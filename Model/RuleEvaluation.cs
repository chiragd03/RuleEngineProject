using RuleEngine.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Model
{
    public class RuleEvaluation
    {
        public void Evaluate(InputRequest req, HashSet<Rule> rules)
        {
            Parallel.ForEach(rules, rule => {            
                var requestsMatchingRule = req.dataStream.Where(e => e.signal == rule.operand);
                if (requestsMatchingRule != null) {
                    switch (rule.operatorExp) {
                        case OperatorExp.NotMoreThan:
                        case OperatorExp.MoreThan:
                            printFailure(requestsMatchingRule.Where(e => e.value_type == "Integer").Where(e => Convert.ToDecimal(e.value) > Convert.ToDecimal(rule.value)).ToList());
                            break;
                        case OperatorExp.NotLessThan:
                        case OperatorExp.LessThan:
                            printFailure(requestsMatchingRule.Where(e => e.value_type == "Integer").Where(e => Convert.ToDecimal(e.value) < Convert.ToDecimal(rule.value)).ToList());
                            break;
                        case OperatorExp.NotEqual:
                        case OperatorExp.Equal:
                            printFailure(requestsMatchingRule.Where(e => e.value_type == "String").Where(e => Convert.ToString(e.value) == Convert.ToString(rule.value)).ToList());
                            break;                        
                        case OperatorExp.NotInFuture:
                            printFailure(requestsMatchingRule.Where(e => e.value_type == "Datetime").Where(e => DateTime.Parse(e.value) > DateTime.Now).ToList());
                            break;
                        case OperatorExp.NotInPast:
                            printFailure(requestsMatchingRule.Where(e => e.value_type == "Datetime").Where(e => DateTime.Parse(e.value) < DateTime.Now).ToList());
                            break;
                    }
                }
            });
        }

        private void printFailure(List<DataStream> inputWhichFailRule)
        {
            foreach (var req in inputWhichFailRule)
            {               
                //Create a stream to serialize the object to.  
                MemoryStream ms = new MemoryStream();
                // Serializer the User object to the stream.  
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DataStream));
                ser.WriteObject(ms, req);
                byte[] json = ms.ToArray();
                ms.Close();
                Console.WriteLine(Encoding.UTF8.GetString(json, 0, json.Length));
            }
        }
    }
}
