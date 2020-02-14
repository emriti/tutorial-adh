using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TutorialADF
{
    public static class PublishCourseCodeController
    {
        [FunctionName("PublishCourseCode")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "PublishCourseCode")] HttpRequest req,
            [EventHub("bcs.coursecode", Connection = "evh-bm7-dev")] IAsyncCollector<string> outputEvents,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            await outputEvents.AddAsync(requestBody);
        }
    }
}
