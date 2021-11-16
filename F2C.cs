using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ITT.ITMD419519
{
    public static class F2C
    {
        [FunctionName("F2C")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string temp = req.Query["temp"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            temp = temp ?? data?.name;

            var tempAsNumber = Convert.ToDouble(temp);
            var convertedTemp = (tempAsNumber - 32) * 5 / 9;

            string responseMessage = string.IsNullOrEmpty(temp)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"{convertedTemp}";

            return new OkObjectResult(responseMessage);
        }
    }
}
