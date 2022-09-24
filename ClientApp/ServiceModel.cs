using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class ServiceModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string APIendpoint { get; set; }
        public int NumOperands { get; set; }
        public string OperandType { get; set; }

        public ServiceModel()
        {
        }
        public ServiceModel(string name, string description, string APIendpoint, int numOperands, string operandType)
        {
            Name = name;
            Description = description;
            this.APIendpoint = APIendpoint;
            NumOperands = numOperands;
            OperandType = operandType;
        }


    }
}
