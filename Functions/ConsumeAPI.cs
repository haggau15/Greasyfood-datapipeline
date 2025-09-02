using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

            JObject jsonObject = JObject.Parse(jsonString);
            var response = req.CreateResponse(HttpStatusCode.OK);
            DataBuilder.Build(jsonObject);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            /**
            var json = await Program.Maine();  // ← await the task

            

            await response.WriteStringAsync(json, Encoding.UTF8); // ← write the JSON as-is
            **/
            return response;
        }
        string jsonString = @"{
  ""places"": [
    {
      ""id"": ""ChIJp1fMLxFuQUYR2unxx_mdGUU"",
      ""rating"": 4.6,
      ""types"": [
        ""meal_takeaway"",
        ""fast_food_restaurant"",
        ""restaurant"",
        ""food"",
        ""point_of_interest"",
        ""establishment""
      ],
      ""formattedAddress"": ""Storgata 32, 0184 Oslo, Norway"",
      ""location"": {
        ""latitude"": 59.9142737,
        ""longitude"": 10.753284
      },
      ""displayName"": {
        ""text"": ""Crispy Clubs Gunerius"",
        ""languageCode"": ""en""
      },
      ""reviews"": [
        {
          ""name"": ""places/ChIJp1fMLxFuQUYR2unxx_mdGUU/reviews/Ci9DQUlRQUNvZENodHljRjlvT2t4a2VXUXhRVlZTTUdadE9GVjRSa0Y0T1VNelpXYxAB"",
          ""relativePublishTimeDescription"": ""in the last week"",
          ""rating"": 5,
          ""text"": {
            ""text"": ""I’ve been been a handful of times, although service can sometimes be a bit slow, it’s made up by being fresh and warm.\n\nAlthough today, 17-18 o’clock, I got two bones in my crispy chicken fries, i didn’t expect it so I bit hard and now my teeth hurt. I noticed that the employees today seemed unfamiliar to me— that they were probably new or maybe I’m mistaken.\n\nEvery other time have been satisfactory, I’ll write this incident off as a fluke. Hopefully this never happens again."",
            ""languageCode"": ""en""
          },
          ""originalText"": {
            ""text"": ""I’ve been been a handful of times, although service can sometimes be a bit slow, it’s made up by being fresh and warm.\n\nAlthough today, 17-18 o’clock, I got two bones in my crispy chicken fries, i didn’t expect it so I bit hard and now my teeth hurt. I noticed that the employees today seemed unfamiliar to me— that they were probably new or maybe I’m mistaken.\n\nEvery other time have been satisfactory, I’ll write this incident off as a fluke. Hopefully this never happens again."",
            ""languageCode"": ""en""
          },
          ""authorAttribution"": {
            ""displayName"": ""Kathy Nguyen"",
            ""uri"": ""https://www.google.com/maps/contrib/115131797172746019403/reviews"",
            ""photoUri"": ""https://lh3.googleusercontent.com/a-/ALV-UjVqNa6jo3JRxY-SWLbJTZVr7TO_GL_T4ZzCIuWjcOy3NQe9MEQ=s128-c0x00000000-cc-rp-mo-ba3""
          },
          ""publishTime"": ""2025-08-14T19:43:04.950843892Z"",
          ""flagContentUri"": ""https://www.google.com/local/review/rap/report?postId=Ci9DQUlRQUNvZENodHljRjlvT2t4a2VXUXhRVlZTTUdadE9GVjRSa0Y0T1VNelpXYxAB&d=17924085&t=1"",
          ""googleMapsUri"": ""https://www.google.com/maps/reviews/data=!4m6!14m5!1m4!2m3!1sCi9DQUlRQUNvZENodHljRjlvT2t4a2VXUXhRVlZTTUdadE9GVjRSa0Y0T1VNelpXYxAB!2m1!1s0x46416e112fcc57a7:0x45199df9c7f1e9da""
        },
        {
          ""name"": ""places/ChIJp1fMLxFuQUYR2unxx_mdGUU/reviews/ChZDSUhNMG9nS0VJQ0FnTUNJeFpqcmNnEAE"",
          ""relativePublishTimeDescription"": ""4 months ago"",
          ""rating"": 5,
          ""text"": {
            ""text"": ""Shop located inside a small mall in the heart of Oslo.\n\nFood was very nice, the staff is nice and helpful. We came here follow the TikTok reviews (and we are from Thailand😂)\n\nWorth a visit!"",
            ""languageCode"": ""en""
          },
          ""originalText"": {
            ""text"": ""Shop located inside a small mall in the heart of Oslo.\n\nFood was very nice, the staff is nice and helpful. We came here follow the TikTok reviews (and we are from Thailand😂)\n\nWorth a visit!"",
            ""languageCode"": ""en""
          },
          ""authorAttribution"": {
            ""displayName"": ""Patty Dangwang"",
            ""uri"": ""https://www.google.com/maps/contrib/108876759157235690933/reviews"",
            ""photoUri"": ""https://lh3.googleusercontent.com/a-/ALV-UjXdiYSTDatoVec6YRNsBXGCA0gtl8f2JJYDvGDBhSg9CMoAPFd0=s128-c0x00000000-cc-rp-mo-ba3""
          },
          ""publishTime"": ""2025-04-03T12:08:14.073934Z"",
          ""flagContentUri"": ""https://www.google.com/local/review/rap/report?postId=ChZDSUhNMG9nS0VJQ0FnTUNJeFpqcmNnEAE&d=17924085&t=1"",
          ""googleMapsUri"": ""https://www.google.com/maps/reviews/data=!4m6!14m5!1m4!2m3!1sChZDSUhNMG9nS0VJQ0FnTUNJeFpqcmNnEAE!2m1!1s0x46416e112fcc57a7:0x45199df9c7f1e9da""
        },
        {
          ""name"": ""places/ChIJp1fMLxFuQUYR2unxx_mdGUU/reviews/Ci9DQUlRQUNvZENodHljRjlvT25aNE1XeG5ZM1pqVDBneWNrRkRlREJXVFZCTWRHYxAB"",
          ""relativePublishTimeDescription"": ""a month ago"",
          ""rating"": 5,
          ""text"": {
            ""text"": ""The location is convenient, inside a mall and easy to reach.\nThe food was very delicious with generous portions.\nPrices are a bit high, but overall, it’s worth trying."",
            ""languageCode"": ""en""
          },
          ""originalText"": {
            ""text"": ""The location is convenient, inside a mall and easy to reach.\nThe food was very delicious with generous portions.\nPrices are a bit high, but overall, it’s worth trying."",
            ""languageCode"": ""en""
          },
          ""authorAttribution"": {
            ""displayName"": ""lulu neela"",
            ""uri"": ""https://www.google.com/maps/contrib/108939546477410079329/reviews"",
            ""photoUri"": ""https://lh3.googleusercontent.com/a/ACg8ocLX-56t275YErI3Tf3QReoFaW5aXRQQgl8r_jfgsPXRlx6gkg=s128-c0x00000000-cc-rp-mo-ba4""
          },
          ""publishTime"": ""2025-07-17T20:27:51.673336183Z"",
          ""flagContentUri"": ""https://www.google.com/local/review/rap/report?postId=Ci9DQUlRQUNvZENodHljRjlvT25aNE1XeG5ZM1pqVDBneWNrRkRlREJXVFZCTWRHYxAB&d=17924085&t=1"",
          ""googleMapsUri"": ""https://www.google.com/maps/reviews/data=!4m6!14m5!1m4!2m3!1sCi9DQUlRQUNvZENodHljRjlvT25aNE1XeG5ZM1pqVDBneWNrRkRlREJXVFZCTWRHYxAB!2m1!1s0x46416e112fcc57a7:0x45199df9c7f1e9da""
        },
        {
          ""name"": ""places/ChIJp1fMLxFuQUYR2unxx_mdGUU/reviews/ChZDSUhNMG9nS0VJQ0FnSUNYbXFicERBEAE"",
          ""relativePublishTimeDescription"": ""10 months ago"",
          ""rating"": 5,
          ""text"": {
            ""text"": ""Don’t walk, RUNNN to crispy clubs.\nHot buffalo wing was good, we went twice 😆\nGreat for desperate option for halal fried chicken.\n\nLocated inside the mall"",
            ""languageCode"": ""en""
          },
          ""originalText"": {
            ""text"": ""Don’t walk, RUNNN to crispy clubs.\nHot buffalo wing was good, we went twice 😆\nGreat for desperate option for halal fried chicken.\n\nLocated inside the mall"",
            ""languageCode"": ""en""
          },
          ""authorAttribution"": {
            ""displayName"": ""anis fadzil"",
            ""uri"": ""https://www.google.com/maps/contrib/115480064669359436000/reviews"",
            ""photoUri"": ""https://lh3.googleusercontent.com/a-/ALV-UjXb_2x4L4IwcYodhiGPB32GXrEsjlJUzSmzGw6-lrYH3gxN0Am8=s128-c0x00000000-cc-rp-mo-ba3""
          },
          ""publishTime"": ""2024-10-15T12:01:36.389498Z"",
          ""flagContentUri"": ""https://www.google.com/local/review/rap/report?postId=ChZDSUhNMG9nS0VJQ0FnSUNYbXFicERBEAE&d=17924085&t=1"",
          ""googleMapsUri"": ""https://www.google.com/maps/reviews/data=!4m6!14m5!1m4!2m3!1sChZDSUhNMG9nS0VJQ0FnSUNYbXFicERBEAE!2m1!1s0x46416e112fcc57a7:0x45199df9c7f1e9da""
        },
        {
          ""name"": ""places/ChIJp1fMLxFuQUYR2unxx_mdGUU/reviews/ChZDSUhNMG9nS0VJQ0FnTURRdmFuc1NREAE"",
          ""relativePublishTimeDescription"": ""5 months ago"",
          ""rating"": 5,
          ""text"": {
            ""text"": ""Fried chicken burger is the best. I love their taste of dressing and fried. Yum!"",
            ""languageCode"": ""en""
          },
          ""originalText"": {
            ""text"": ""Fried chicken burger is the best. I love their taste of dressing and fried. Yum!"",
            ""languageCode"": ""en""
          },
          ""authorAttribution"": {
            ""displayName"": ""Artitaya Andersen"",
            ""uri"": ""https://www.google.com/maps/contrib/109088439927431389389/reviews"",
            ""photoUri"": ""https://lh3.googleusercontent.com/a-/ALV-UjU1sIb7DuU_7YivkCTcJ609_f8d2i9md5SfwZ_SlIP3scoJOjEf4g=s128-c0x00000000-cc-rp-mo-ba3""
          },
          ""publishTime"": ""2025-03-13T11:26:40.465340Z"",
          ""flagContentUri"": ""https://www.google.com/local/review/rap/report?postId=ChZDSUhNMG9nS0VJQ0FnTURRdmFuc1NREAE&d=17924085&t=1"",
          ""googleMapsUri"": ""https://www.google.com/maps/reviews/data=!4m6!14m5!1m4!2m3!1sChZDSUhNMG9nS0VJQ0FnTURRdmFuc1NREAE!2m1!1s0x46416e112fcc57a7:0x45199df9c7f1e9da""
        }
      ]
    },
    {
      ""id"": ""ChIJ2TROTABvQUYRWgI5sMnPs8A"",
      ""rating"": 1.6,
      ""types"": [
        ""fast_food_restaurant"",
        ""restaurant"",
        ""food"",
        ""point_of_interest"",
        ""establishment""
      ],
      ""formattedAddress"": ""Oslo Pizza beside kaka pan shop, college rd, GRW, Norway"",
      ""location"": {
        ""latitude"": 59.9138614,
        ""longitude"": 10.7522174
      },
      ""displayName"": {
        ""text"": ""Oslo Pizza"",
        ""languageCode"": ""en""
      }
    }
  ]
}";
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
                "places.reviews,places.reviewSummary,places.id,places.displayName,places.formattedAddress,places.location,places.types");

            var requestBody = new
            {
                includedTypes = new[] { "fast_food_restaurant" },
                locationRestriction = new
                {
                    circle = new
                    {
                        center = new { latitude = 59.9139, longitude = 10.7522 },
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