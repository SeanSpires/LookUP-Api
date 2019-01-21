using System.Linq;
using System.Threading.Tasks;
using LookUpApi.Services;
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
            return await group;
        }
          
        [HttpPost("mediaUpload")]
        public async Task<string> UploadMediaFiles()
        {
            var files = Request.Form.Files;
            var fileName = files.First().FileName;
            var stream = files.First().OpenReadStream();
            var formFileUri =  _blobManager.UploadFileAsBlob(stream, fileName);

            return await formFileUri;
        }

        [HttpPost("create")]
        public ActionResult<Group> CreateGroup(Group group)
        {
            _groupService.Create(group);
            return CreatedAtAction("CreateGroup", group);
        }
    }
}