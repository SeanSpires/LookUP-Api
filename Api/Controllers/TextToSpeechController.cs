using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Policy;
using System.Threading.Tasks;
using LookUpApi.Models;
using LookUpApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextToSpeechController : ControllerBase
    {
        
        [HttpPost]
        public async Task<string> InputTextAndReturnSoundFile(TextInput input)
        {    
            var text = input.Text;
            var textToSpeechService = new TextToSpeechService();
            var uri = textToSpeechService.GenerateAudioSpeech(text);
            return uri.ToString();
        }
        

    }
}