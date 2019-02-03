using System.Net;
using System.Threading.Tasks;
using LookUpApi.Models;
using Newtonsoft.Json;

namespace LookUpApi.Services
{
    public class LuisService
    {
        public async Task<string> GetIntent(string question)
        {
            const string luisEndPoint = "https://australiaeast.api.cognitive.microsoft.com/luis/v2.0/apps/13e77df7-a129-4d7f-bbc9-fa9e36c6389e?verbose=true&timezoneOffset=600&subscription-key=599d4e5c2d4747378f3924452d3f2987&q=";
            
            var webClient = new WebClient();

            var response =  webClient.DownloadString(luisEndPoint + question);

            var luisResponse = JsonConvert.DeserializeObject<LuisResponse>(response);

            return luisResponse.topScoringIntent.intent;
        }
    }
}