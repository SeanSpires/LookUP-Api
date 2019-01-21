using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


public class BlobManager
{
    private CloudStorageAccount _storageAccount;
    private readonly CloudBlobClient _blobClient;

    public BlobManager(IConfiguration config)
    {
        _storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("LookUpBlob"));
        _blobClient = _storageAccount.CreateCloudBlobClient();
    }

    public async Task<string> UploadFileAsBlob(Stream stream, string filename)
    { 
        var container = _blobClient.GetContainerReference("media-uploads");
 
        var blockBlob = container.GetBlockBlobReference(filename);
        
        await blockBlob.UploadFromStreamAsync(stream);
        
        stream.Dispose();
        return blockBlob.Uri.ToString();
    }

  
    public async Task<MemoryStream> DownloadFileAsync(string blobName)
    {
        
        var container = _blobClient.GetContainerReference("mediaUploads");

        var blockBlob = container.GetBlockBlobReference(blobName);

        using (var stream = new MemoryStream())
        {
            await blockBlob.DownloadToStreamAsync(stream);
            return stream;
        }
    }


}  