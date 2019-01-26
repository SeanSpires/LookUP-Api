using System.Linq;
using System.Threading.Tasks;
using LookUpApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Group = LookUpApi.Models.Group;

namespace LookUpApi.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly BlobManager _blobManager;
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService, BlobManager blobManager)
        {    
            _blobManager = blobManager;
            _groupService = groupService;
        }

        [HttpGet("{groupName}")]
        public async Task<Group> GetGroup(string groupName)
        {
            var group = _groupService.Get(groupName);
            return  await group;
        }
          
        [HttpPost("mediaUpload")]
        public async Task<string> UploadMediaFiles([FromBody]IFormFile file)
        {
          //  var files = Request.Form.Files;
         
            var fileName = file.FileName;
            var stream = file.OpenReadStream();
            var formFileUri =  await _blobManager.UploadFileAsBlob(stream, fileName);

            return formFileUri;
        }

        [HttpPost("create")]
        public async Task<CreatedAtActionResult> CreateGroup(Group group)
        {
            await _groupService.Create(group);
            return CreatedAtAction("CreateGroup", group);
        }
    }
}