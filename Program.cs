using RuleEngine.Entities;
using RuleEngine.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine
{
    class Program
    {

        public void start() {

            //Reading incoming stream
            var getReqThread = Task.Run(()=> readInputStream());            
           
            //Reading Rules from file
            var getRuleThread = Task.Run(()=>readRulesFromFile());

            Task.WhenAll(getReqThread, getRuleThread);
            //Pass through Rule Evaluation
            RuleEvaluation eval = new RuleEvaluation();
            eval.Evaluate(getReqThread.Result, getRuleThread.Result);
        }

        static void Main(string[] args)
        {
            Program ob = new Program();
            ob.start();
            //Console.ReadLine();
        }


        public InputRequest readInputStream() {

            string json = File.ReadAllText("raw_data.json");
            List<DataStream> deserializedUser = new List<DataStream>();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as List<DataStream>;
            ms.Close();
            return new InputRequest() {
                dataStream = deserializedUser
            };

        }

        public HashSet<Rule> readRulesFromFile() {

            HashSet<Rule> retVal = new HashSet<Rule>();
            string line;
            int counter = 0;
            
            System.IO.StreamReader file = new System.IO.StreamReader("Rules.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] rulePts = line.Trim().Split(' ');
                retVal.Add(new Rule()
                {
                    operand = rulePts[0],
                    operatorExp = (OperatorExp)Enum.Parse(typeof(OperatorExp), rulePts[1]),
                    value = rulePts.Length > 2 ? rulePts[2] : null

                });                
                counter++;
            }

            return retVal;
        }
        


    }
}
