using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
    public class UnpublishController : ApiController
    {
       
        
        [Route("api/unpublish")]
        public IHttpActionResult PostUnpublish(string name, int token)
        {
            string validateStatus = ValidateReq.Validate(token);
            if (validateStatus == "validated")
            {

                List<ServiceModel> services = new List<ServiceModel>();

                if (!File.Exists("services.txt"))
                {
                    return Ok(new { error = "services.txt not found " });
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

                    //remove service from list
                    services.RemoveAll(serv => serv.Name == name);

                    //write to file
                    string json = JsonConvert.SerializeObject(services, Formatting.Indented,
           new JsonConverter[] { new StringEnumConverter() });
                    File.WriteAllText("services.txt", json);

                    return Ok("Service unpublished");
                }
            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }
            
            

        }
    }
}
