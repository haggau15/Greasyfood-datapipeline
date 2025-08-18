using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greasyfood_datapipeline.Functions
{
    internal class DataBuilder
    {
        private readonly ILogger _logger;

        public DataBuilder(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DataBuilder>();
        }

        [Function("DataBuilder")]
        public string Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("DataBuilder triggered");
            return "Hello from DataBuilder!";
        }
    }
}
