using Newtonsoft.Json;
using ServiceProvider.Models;
using ServiceProvider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    public class ArithmeticController : ApiController
    {
        //Object from validator class is used to validate the token.
        //This is located in the folder /Services/ValidateReq.cs
        ValidateReq validator = new ValidateReq();


        //Attribute Routing is used to map the URL to the method.
        [Route("api/arithmetic/addtwonumbers")]
        public IHttpActionResult GetAddTwoNums(int num1, int num2, int token)
        {
            string validateStatus = validator.Validate(token);

            if (validateStatus == "validated")
            {
                //logic of each calculation is seperated into different class for reability
                //This is located in the folder /Services/ArithmaticService.cs
                int sum = ArithmaticService.AddTwoNums(num1, num2);
                
                string json = JsonConvert.SerializeObject(new { sum = sum });

                return Ok(new { sum = sum });

            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }



        }

        [Route("api/arithmetic/addthreenumbers")]
        public IHttpActionResult GetAddThreeNums(int num1, int num2, int num3, int token)
        {


            string validateStatus = validator.Validate(token);

            if (validateStatus == "validated")
            {
                int sum = ArithmaticService.AddThreeNums(num1, num2, num3);
                string json = JsonConvert.SerializeObject(new { sum = sum });

                return Ok(new { sum = sum });

            }
            else
            {

                return Ok(new { Status = "Denied", Reason = "Authentication Error" });




            }

        }

        [Route("api/arithmetic/multwonumbers")]
        public IHttpActionResult GetMulTwoNums(int num1, int num2, int token)
        {


            string validateStatus = validator.Validate(token);

            if (validateStatus == "validated")
            {
                int sum = ArithmaticService.MulTwoNums(num1, num2);
                string json = JsonConvert.SerializeObject(new { sum = sum });

                return Ok(new { sum = sum });

            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }



        }

        [Route("api/arithmetic/multhreenumbers")]
        public IHttpActionResult GetMulThreeNums(int num1, int num2, int num3, int token)
        {


            string validateStatus = validator.Validate(token);

            if (validateStatus == "validated")
            {
                int sum = ArithmaticService.MulThreeNums(num1, num2, num3);
                string json = JsonConvert.SerializeObject(new { sum = sum });

                return Ok(new { sum = sum });

            }
            else
            {
                return Ok(new { Status = "Denied", Reason = "Authentication Error" });
            }
        }
    }
}
