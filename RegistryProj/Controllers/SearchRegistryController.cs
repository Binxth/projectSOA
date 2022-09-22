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
    public class SearchRegistryController : ApiController
    {
       

        [Route("api/searchregistry")]
        public IHttpActionResult GetSearch(string description, int token)
        {

            string validateStatus = ValidateReq.Validate(token);

            if (validateStatus == "validated")
            {

                List<ServiceModel> services = new List<ServiceModel>();
                List<ServiceModel> servicesFound = new List<ServiceModel>();

                if (!File.Exists("services.txt"))
                {
                    return Ok(new { error = "services.txt not found" });
                }
                else
                {

                    using (StreamReader r = new StreamReader("services.txt"))
                    {
                        string jsonFile = File.ReadAllText("services.txt");

                        if (jsonFile != "")
                        {
                            services = JsonConvert.DeserializeObject<List<ServiceModel>>(jsonFile);
                        }

                    }
                }

                //search for service from description 
                foreach (ServiceModel service in services)
                {
                    if (service.Description.ToLower().Contains(description.ToLower()))
                    {
                        servicesFound.Add(service);
                    }
                }

                if (servicesFound.Count == 0)
                {
                    return Ok(new { error = "No services found" });
                }
                else
                {
                    return Ok(servicesFound);
                }

            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }

        }
    }
}
