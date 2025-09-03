using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {

            //JObject jsonObject = JObject.Parse(jsonString);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            
            var json = await Program.Maine();
            JObject jsonObj = JObject.Parse(json);
            DataBuilder.Build(jsonObj);



            await response.WriteStringAsync(json, Encoding.UTF8); // ← write the JSON as-is
           
            return response;
        }
        
    }

    class Program
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_TOKEN");
        private static readonly string Endpoint = Environment.GetEnvironmentVariable("GOOGLE_MAPS_URI");

        public static async Task<string> Maine()
        {
            if (string.IsNullOrWhiteSpace(ApiKey)) throw new InvalidOperationException("GOOGLE_MAPS_TOKEN is missing.");
            if (string.IsNullOrWhiteSpace(Endpoint)) throw new InvalidOperationException("GOOGLE_MAPS_URI is missing.");

            using var httpClient = new HttpClient();

            // Required headers for Places API (field mask applies to response selection)
            httpClient.DefaultRequestHeaders.Add("X-Goog-Api-Key", ApiKey);
            httpClient.DefaultRequestHeaders.Add("X-Goog-FieldMask",
                "places.reviews,places.photos,places.reviewSummary,places.id,places.displayName,places.formattedAddress,places.location,places.types");

            var requestBody = new
            {
                includedTypes = new[] { "fast_food_restaurant" },
                locationRestriction = new
                {
                    circle = new
                    {
                        center = new { latitude = 59.913546, longitude = 10.7524953 },
                        radius = 100
                    }
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var resp = await httpClient.PostAsync(Endpoint, content);
            if (resp.IsSuccessStatusCode)
            {
                // This is already a JSON string
                return await resp.Content.ReadAsStringAsync();
            }
            else
            {
                var err = await resp.Content.ReadAsStringAsync();
                // Return an error payload as JSON so the function still responds with JSON
                return JsonSerializer.Serialize(new { error = true, status = (int)resp.StatusCode, message = err });
            }
        }
    }
}