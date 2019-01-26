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
    public class UserController : ControllerBase
    {
        private readonly BlobManager _blobManager;
        private readonly UserService _userService;

        public UserController(UserService userService, BlobManager blobManager)
        {    
            _blobManager = blobManager;
            _userService = userService;
        }
        
        [HttpGet("{userId}")]
        public async Task<User> GetGroup(ObjectId userId)
        {
            var user = _userService.Get(userId);
            return await user;
        }

        [HttpPost("create")]
        public async Task<CreatedAtActionResult> CreateUser(User user)
        {
            _userService.Create(user);
            return CreatedAtAction("CreateUser", user);
        }
    }
}