using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistryProj.Models
{
    public class ServiceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string APIendpoint { get; set; }
        public int NumOperands { get; set; }
        public string OperandType { get; set; }
        
        
    }
}