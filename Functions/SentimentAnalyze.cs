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
    internal class SentimentAnalyze
    {
        private readonly ILogger _logger;

        public SentimentAnalyze(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SentimentAnalyze>();
        }

        [Function("SentimentAnalyze")]
        public string Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("SentimentAnalyze triggered");
            return "Hello from SentimentAnalyze!";
        }
    }
}
