using Microsoft.ApplicationInsights.Channel;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Greasyfood_datapipeline.Functions
{
    public static class DataBuilder
    {

        public static async Task<JObject> Build(JObject places)
        {
            foreach (JObject place in places["places"])
            {
              
                if ((place.ContainsKey("reviews")) && (place["reviews"] != null))
                {
                    JObject reviewOBJ = getReviews(place);
                    SentimentAnalyze.Run(reviewOBJ);
                }
                else
                {
                    Console.WriteLine("No reviews found");
                }
            }
            return places;
        }

        public static JObject getReviews(JObject place)
        {
            JArray tmp = new ();
            foreach (JObject review in place["reviews"])
            {
                tmp.Add(review["text"]["text"]);
            }
            JObject returnJson = new JObject();
            returnJson["reviews"] = tmp;
            return returnJson;
        }
    }
}
