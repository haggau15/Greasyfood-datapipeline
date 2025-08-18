using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Greasyfood_datapipeline.Functions
{
    internal class ConsumeAPI
    {
        private readonly ILogger _logger;

        public ConsumeAPI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsumeAPI>();
        }

        [Function("ConsumeAPI")]
        public string Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("ConsumeAPI triggered");
            return "Hello from ConsumeAPI!";
        }
    }
}
