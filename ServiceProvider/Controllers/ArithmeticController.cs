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

        ValidateReq validator = new ValidateReq();

        [Route("api/arithmetic/addtwonumbers")]
        public IHttpActionResult GetAddTwoNums(int num1, int num2)
        {
            string validateStatus = validator.Validate(1234);

            if (validateStatus == "validated")
            {
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
        public IHttpActionResult GetAddThreeNums(int num1, int num2, int num3)
        {


            string validateStatus = validator.Validate(1234);

            if (validateStatus == "validated")
            {
                int sum = ArithmaticService.AddThreeNums(num1, num2, num3);
                string json = JsonConvert.SerializeObject(new { sum = sum });

                return Ok(ResponseModel.CreateResponse(new { sum = sum }, "Success", ""));

            }
            else
            {
                
                return Ok(ResponseModel.CreateResponse(new { }, "Denied", "Authentication Error"));




            }

        }

        [Route("api/arithmetic/multwonumbers")]
        public IHttpActionResult GetMulTwoNums(int num1, int num2)
        {


            string validateStatus = validator.Validate(1234);

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
        public IHttpActionResult GetMulThreeNums(int num1, int num2, int num3)
        {


            string validateStatus = validator.Validate(1234);

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
