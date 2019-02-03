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
            const string luisEndPoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/334ec72e-6a16-42e8-b2a3-bc0f2a4ce359?verbose=true&timezoneOffset=-360&subscription-key=bbab6d2fbfcd4aa9bcd57e4918c86d65&q=";
            
            var webClient = new WebClient();

            var response =  webClient.DownloadString(luisEndPoint + question);

            var luisResponse = JsonConvert.DeserializeObject<LuisResponse>(response);

            return luisResponse.topScoringIntent.intent;
        }
    }
}