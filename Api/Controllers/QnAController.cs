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
        [HttpPost]
        public async Task<TextAndAudioWrapper> InputQuestionAndReturnAnswer(TextInput textInput)
        {
            var luisService = new LuisService();
            var wrapper = new TextAndAudioWrapper();
            var question = textInput.Text;
            
            var qnaMaker = new QnAMakerService("https://lookupqna.azurewebsites.net", 
                "d282ad63-9701-4548-afac-a421f0ec43ed", "3f35c105-e3c4-40de-b3af-68e42a29da98");
            
            var groupKB = new QnAMakerService("https://lookup-groupkb.azurewebsites.net", 
                "e2d2c2c2-c4b4-4435-b81d-039d116d1b4a", "23d18d1b-9d9a-4a62-8d80-35481c3f5ff7");


            var intent = await luisService.GetIntent(question);

            string answer;
            switch (intent)
            {
                case "Group":
                    answer = await groupKB.GetAnswer(question);
                    break;
                case "None":
                    answer = "Sorry, I don't know that.";
                    break;
                default:
                    answer = "Sorry, I don't know that.";
                    break;
            }

//            Console.WriteLine("The intent is " + intent);
//            Console.WriteLine("The answer is");
            
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