using System;
using System.Net;
using System.Threading.Tasks;
using LookUpApi.Models;
using LookUpApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QnAController : ControllerBase
    {
        [HttpPost]
        public async Task<TextAndAudioWrapper> InputQuestionAndReturnAnswer(TextInput textInput)
        {
            var wrapper = new TextAndAudioWrapper();
            var question = textInput.Text;
            var qnaMaker = new QnAMakerService("https://lookupqna.azurewebsites.net", 
                "d282ad63-9701-4548-afac-a421f0ec43ed", "3f35c105-e3c4-40de-b3af-68e42a29da98");

            var answer = await qnaMaker.GetAnswer(question);
            
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