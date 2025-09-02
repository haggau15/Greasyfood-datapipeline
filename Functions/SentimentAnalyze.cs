using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.TextAnalytics;
using Newtonsoft.Json.Linq;

namespace Greasyfood_datapipeline.Functions
{
    public class SentimentAnalyze
    {
       
        public static JObject Run(JObject data)
        {
            //Console.WriteLine("Oah");
            //Console.WriteLine(data);
            List<string> reviews = new List<string>();
            foreach (var review in data["reviews"])
            {
                reviews.Add(review.ToString());
            }
            JObject res = Analyze(reviews);
            return res;
        }

        private static JObject Analyze(List<string> reviews)
        {
            // Replace with your endpoint and key
            string endpoint = Environment.GetEnvironmentVariable("SETNIMENT_URI");
            string apiKey = Environment.GetEnvironmentVariable("SENTIMENT_TOKEN");

            var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            // Call sentiment analysis
            Response<AnalyzeSentimentResultCollection> response = client.AnalyzeSentimentBatch(reviews);
            JArray resultJson = new();
            // Loop through results
            int i = 1;
            foreach (AnalyzeSentimentResult result in response.Value)
            {
                if (result.HasError)
                {
                    Console.WriteLine($"Document {i} error: {result.Error.Message}");
                }
                else
                {
                    JObject review = new JObject
                    {
                        ["Positive"] = $"{result.DocumentSentiment.ConfidenceScores.Positive:0.00}",
                        ["Negative"] = $"{result.DocumentSentiment.ConfidenceScores.Negative:0.00}",
                        ["Neutral"] = $"{result.DocumentSentiment.ConfidenceScores.Neutral:0.00}"
                    };
                    resultJson.Add(review);
                }
                i++;
            }
            JObject res = new JObject();
            res["reviews"] = resultJson;
            return res;
        }
    }
}
