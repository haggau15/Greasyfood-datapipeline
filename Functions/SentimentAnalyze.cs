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
            Console.WriteLine("Oah");
            Console.WriteLine(data);
            JObject sentiment = JObject.Parse(sentimentScores);
            return sentiment;
        }

        static string Analyze()
        {
            // Replace with your endpoint and key
            string endpoint = Environment.GetEnvironmentVariable("SETNIMENT_URI");
            string apiKey = Environment.GetEnvironmentVariable("SENTIMENT_TOKEN");

            var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            // Batch of documents
            var documents = new List<string>
        {
            "It took 8 minutes for them to take my order at the drive through speaker. They had no ice cream for floats. Paid at the window and was asked to pull to the side and wait for the food. Relieved my order and the burger was not plain. They fixed it. Checked my bag and was missing fries. Got fries. Left, drove about 10 miles out and noticed the chicken sandwich was not spicy and the sprite was missing flavor. Drove back and got my sandwich switched to spicy and they gave me a root beer. Whole thing took 59 minutes.",
            "Dårlig og uspiselig mat, frekk og lite høflig staff.  Ikke billig.  Styr unna. Anbefales ikke.",
            "\r\nKdyby to bylo možné, dal bych 0 hvězdiček. Po snědení kuřecího kebabu jsem měl silné bolesti břicha, které trvaly celý večer a následující den. Velmi zklamání a nepřijatelné."
        };

            // Call sentiment analysis
            Response<AnalyzeSentimentResultCollection> response = client.AnalyzeSentimentBatch(documents);

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
                    Console.WriteLine($"Document {i} sentiment: {result.DocumentSentiment.Sentiment}");
                    Console.WriteLine($"  Positive: {result.DocumentSentiment.ConfidenceScores.Positive:0.00}");
                    Console.WriteLine($"  Neutral : {result.DocumentSentiment.ConfidenceScores.Neutral:0.00}");
                    Console.WriteLine($"  Negative: {result.DocumentSentiment.ConfidenceScores.Negative:0.00}");
                }
                i++;
            }
            return "";
        }
        static string sentimentScores = @"{
  ""documents"": [
    {
      ""id"": ""1"",
      ""sentiment"": ""positive"",
      ""confidenceScores"": {
        ""positive"": 0.98,
        ""neutral"": 0.01,
        ""negative"": 0.01
      },
      ""sentences"": [
        {
          ""text"": ""I love using Azure services."",
          ""sentiment"": ""positive"",
          ""confidenceScores"": {
            ""positive"": 0.98,
            ""neutral"": 0.01,
            ""negative"": 0.01
          },
          ""offset"": 0,
          ""length"": 28
        }
      ],
      ""warnings"": []
    },
    {
      ""id"": ""2"",
      ""sentiment"": ""negative"",
      ""confidenceScores"": {
        ""positive"": 0.02,
        ""neutral"": 0.05,
        ""negative"": 0.93
      },
      ""sentences"": [
        {
          ""text"": ""The weather today is terrible."",
          ""sentiment"": ""negative"",
          ""confidenceScores"": {
            ""positive"": 0.02,
            ""neutral"": 0.05,
            ""negative"": 0.93
          },
          ""offset"": 0,
          ""length"": 30
        }
      ],
      ""warnings"": []
    },
    {
      ""id"": ""3"",
      ""sentiment"": ""neutral"",
      ""confidenceScores"": {
        ""positive"": 0.15,
        ""neutral"": 0.70,
        ""negative"": 0.15
      },
      ""sentences"": [
        {
          ""text"": ""This product is just okay, nothing special."",
          ""sentiment"": ""neutral"",
          ""confidenceScores"": {
            ""positive"": 0.15,
            ""neutral"": 0.70,
            ""negative"": 0.15
          },
          ""offset"": 0,
          ""length"": 40
        }
      ],
      ""warnings"": []
    }
  ],
  ""errors"": [],
  ""modelVersion"": ""2022-10-01""
}";
    }
}
