using System;
using System.Threading.Tasks;

namespace LookUpApi.Services
{
    public class TextToSpeechService
    {
        public async Task<string> GenerateAudioSpeech(string text)
        {
            var blobManager = new BlobManager("DefaultEndpointsProtocol=https;AccountName=lookupblob;AccountKey=X9VFCG96Of1wso1PdmyU8FxIL39xHgDdbmLUMIScKvYM1dQbnCWJ16PxA9rAwhXW6lIueXMzGc6p/hbT2itjDw==;EndpointSuffix=core.windows.net");
            var responseMessage = await TextToSpeech.CognitiveService.GenerateSpeechStream(text);
            var dataStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            var uri = await blobManager.UploadFileAsBlob(dataStream, id +".wav");
            return uri;
        }
    }
}