using System;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TextToSpeech
{
    public class CognitiveService
    {
        public static async Task<HttpResponseMessage> GenerateSpeechStream(string text)
        {
            
            const string host = "https://westus.tts.speech.microsoft.com/cognitiveservices/v1";

            string accessToken;
            Console.WriteLine("Attempting token exchange. Please wait...\n");


            var auth = new Authentication("https://westus.api.cognitive.microsoft.com/sts/v1.0/issuetoken", "e8e7e1c6282442cda55639d2738f3d82");
            try
            {
                accessToken = await auth.FetchTokenAsync().ConfigureAwait(false);
                Console.WriteLine("Successfully obtained an access token. \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to obtain an access token.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return null;
            }

            var body = @"<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaNeural)'>" +
                          text + "</voice></speak>";
            
         
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    // Set the HTTP method
                    request.Method = HttpMethod.Post;
                    // Construct the URI
                    request.RequestUri = new Uri(host);
                    // Set the content type header
                    request.Content = new StringContent(body, Encoding.UTF8, "application/ssml+xml");
                    // Set additional header, such as Authorization and User-Agent
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Connection", "Keep-Alive");
                    // Update your resource name
                    request.Headers.Add("User-Agent", "lookupofficial");
                    request.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                    // Create a request
                    Console.WriteLine("Calling the TTS service. Please wait... \n");
                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    
                    response.EnsureSuccessStatusCode();
                    return response;
                    }    
                }
            }
        }
    
}