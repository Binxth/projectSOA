using Newtonsoft.Json;
using RegistryProj.Models;
using RegistryProj.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RegistryProj.Controllers
{
    public class AllServicesController : ApiController
    {
        
        
        [Route("api/allservices")]
        public IHttpActionResult GetAllServices(int token)
        {
            string validateStatus = ValidateReq.Validate(token);

            if (validateStatus == "validated")
            {
                if (!File.Exists("services.txt"))
                {
                    return Ok(new { error = "services.txt not found" });
                }
                else
                {
                    List<ServiceModel> services = new List<ServiceModel>();
                    using (StreamReader r = new StreamReader("services.txt"))
                    {
                        string jsonFile = File.ReadAllText("services.txt");
                        //if not null
                        if (jsonFile != "")
                        {
                            services = JsonConvert.DeserializeObject<List<ServiceModel>>(jsonFile);
                        }
                    }
                    return Ok(services);
                }
            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }

           
        }

    }
}
