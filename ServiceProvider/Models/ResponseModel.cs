using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceProvider.Models
{
    public class ResponseModel
    {
        public ResponseModel(object data, string status, string reason)
        {
            this.data = data;
            Status = status;
            Reason = reason;
        }

        public object data  { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

       

        public static  ResponseModel CreateResponse(object data, string Status, string Reason)
        {
            return new ResponseModel(data, Status, Reason);
        }

    }
}