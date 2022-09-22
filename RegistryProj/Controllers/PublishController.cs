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
    public class PublishController : ApiController
    {
        ValidateReq validator = new ValidateReq();

        [Route("api/publish")]
        public IHttpActionResult PostPublish(ServiceModel service, int token)
        {
            string validateStatus = validator.Validate(token);

            if (validateStatus == "validated")
            {


                List<ServiceModel> services = new List<ServiceModel>();


                if (!File.Exists("services.txt"))
                {
                    File.Create("services.txt");
                }
                else
                {
                    //read all services from file
                    using (StreamReader r = new StreamReader("services.txt"))
                    {
                        string jsonFile = File.ReadAllText("services.txt");
                        //if not null
                        if (jsonFile != "")
                        {
                            services = JsonConvert.DeserializeObject<List<ServiceModel>>(jsonFile);
                        }

                    }
                }

                services.Add(service);

                string json = JsonConvert.SerializeObject(services);
                File.WriteAllText("services.txt", json);

                return Ok("Service published");
            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }




        }
    }
}
