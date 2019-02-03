using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LookUpApi.Models;
using Newtonsoft.Json;

namespace LookUpApi.Services
{
    
    [Serializable]
    public class QnAMakerService
    {
        private readonly string _qnaServiceHostName;
        private readonly string _knowledgeBaseId;
        private readonly string _endpointKey;

        public QnAMakerService(string hostName, string kbId, string endpointkey)
        {
            _qnaServiceHostName = hostName;
            _knowledgeBaseId = kbId;
            _endpointKey = endpointkey;

        }

        private async Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + _endpointKey);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
        public async Task<string> GetAnswer(string question)
        {     
            var uri = _qnaServiceHostName + "/qnamaker/knowledgebases/" + _knowledgeBaseId + "/generateAnswer";
            var questionJSON = @"{'question': '" + question + "'}";

            var response = await Post(uri, questionJSON);

            var answers = JsonConvert.DeserializeObject<QnAAnswer>(response);
            if (answers.answers != null && answers.answers.Count > 0)
            {
                return answers.answers[0].answer;
            }

            return "";
        }
    }
}