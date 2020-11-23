using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class Http_Noise_Maker
    {
        ///
        /// Add query string parameters to change the http code returned
        /// ?pass=true
        /// ?notfound=true
        /// Default return is 418 i'm a tea pot
        ///
        [FunctionName("Http_Noise_Maker")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
             log.LogInformation("C# HTTP trigger function *NoiseMaker* processed a request.");

            string pass = req.Query["pass"];
            string notfound = req.Query["notfound"];
            string servererror = req.Query["servererror"];


            if (pass == "true")
            {
                var result = new ObjectResult("I'M OK 😁");
                result.StatusCode = StatusCodes.Status200OK;
                return result;
            }
            else if (notfound == "true")
            {
                var result = new ObjectResult("I'M NOT FOUND 😲");
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
            }
            else if (servererror == "true")
            {
                var result = new ObjectResult("I'M ERROR.I..N...G  💣");
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }
            else
            {
                var result = new ObjectResult("I'M A TEA POT 🍵");
                result.StatusCode = StatusCodes.Status418ImATeapot;
                return result;
            }
        }
    }
}
