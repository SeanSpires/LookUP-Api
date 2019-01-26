using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextToSpeechController : ControllerBase
    {
        
        [HttpPost]
        public async Task<string> InputTextAndReturnSoundFile(Input input)
        {    
            var text = input.Text;
            var blobManager = new BlobManager("DefaultEndpointsProtocol=https;AccountName=lookupblob;AccountKey=X9VFCG96Of1wso1PdmyU8FxIL39xHgDdbmLUMIScKvYM1dQbnCWJ16PxA9rAwhXW6lIueXMzGc6p/hbT2itjDw==;EndpointSuffix=core.windows.net");
            var responseMessage = await TextToSpeech.CognitiveService.GenerateSpeechStream(text);
            var dataStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            var uri = await blobManager.UploadFileAsBlob(dataStream, id +".wav");
            return uri;

            //     return stream;
//            response.Content = new StreamContent(stream);
//            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
//            return response;
        }
        
        public class Input
        {
            public string Text { get; set; }
        }
    }
}