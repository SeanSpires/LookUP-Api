using System.Linq;
using System.Threading.Tasks;
using LookUpApi.Models;
using LookUpApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly BlobManager _blobManager;
        private readonly PostService _postService;

        public PostController(PostService postService, BlobManager blobManager)
        {    
            _blobManager = blobManager;
            _postService = postService;
        }

        [HttpGet("{postId}")]
        public Task<Post> getPost(ObjectId postId)
        {
            var post = _postService.Get(postId);
            return post;
        }
        
        [HttpPost("mediaUpload")]
        public string UploadMediaFiles()
        {
            var files = Request.Form.Files;
            var fileName = files.First().FileName;
            var stream = files.First().OpenReadStream();
            var formFileUri =  _blobManager.UploadFileAsBlob(stream, fileName);

            return formFileUri;
        }
        
        [HttpPost("create")]
        public ActionResult<Post> CreatePost(Post post)
        {
            _postService.Create(post);
            return CreatedAtAction("CreatePost", post);
        }


    }
}