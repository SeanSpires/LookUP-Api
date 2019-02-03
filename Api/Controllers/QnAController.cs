using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LookUpApi.Models;
using LookUpApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LookUpApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class QnAController : ControllerBase
    {
        private string _qnaHostName = "https://lookupqna.azurewebsites.net";
        private string _endPointKey = "3f35c105-e3c4-40de-b3af-68e42a29da98";
        
        [HttpPost]
        public async Task<TextAndAudioWrapper> InputQuestionAndReturnAnswer(TextInput textInput)
        {
            var luisService = new LuisService();
            var wrapper = new TextAndAudioWrapper();
            var question = textInput.Text;
            
            var generalKB = new QnAMakerService(_qnaHostName, 
                "d282ad63-9701-4548-afac-a421f0ec43ed", _endPointKey);
            
            var groupKB = new QnAMakerService(_qnaHostName, 
                "b51f7e68-706b-42e4-8b31-0dd34d392a54", _endPointKey);
            
            var postKB = new QnAMakerService(_qnaHostName, 
                "92e0fe0c-8826-4e60-8c47-9a4d36cb629e", _endPointKey);


            var intent = await luisService.GetIntent(question);

            string answer;
            switch (intent)
            {
                case "Group":
                    answer = await groupKB.GetAnswer(question);
                    break;
                case "Post":
                    answer = await postKB.GetAnswer(question);
                    break;
                case "None":
                    answer = await generalKB.GetAnswer(question);
                    break;
                default:
                    answer = "Sorry, I don't know that.";
                    break;
            }
            
            if (answer == "")
            {
                answer = "Sorry, I don't know that.";
            }
            
            var textToSpeechService = new TextToSpeechService();
            var audioAnswer = await textToSpeechService.GenerateAudioSpeech(answer);
            
            wrapper.Text = answer;
            wrapper.Audio = audioAnswer;
            return wrapper;
        }
        

    }
    
}