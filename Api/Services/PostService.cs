using System.Threading.Tasks;
using LookUpApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LookUpApi.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;
        private readonly IMongoDatabase _database;

        public PostService(IConfiguration configuration)
        {    
            var client = new MongoClient(configuration.GetConnectionString("LookUpDb"));
            _database = client.GetDatabase("LookUP");
            _posts = _database.GetCollection<Post>("Post");        
        }
        
        public async Task<Post> Get(ObjectId postId)
        {
            var postsAsQueryable = _posts.AsQueryable();
            var postToGet = await postsAsQueryable.FirstAsync(post => post.Id == postId);
            return postToGet;
        }

        public async void Create(Post post)
        {
            await _posts.InsertOneAsync(post);
        }

        public async void Update(ObjectId postId, Post postIn)
        {
            await _posts.ReplaceOneAsync(post => post.Id == postId, postIn);
        }

        public async void Remove(Post postToRemove)
        {
            
            await _posts.DeleteOneAsync(post => post.Id == postToRemove.Id);
        }

        public async void Remove(ObjectId postId)
        {
            await _posts.DeleteOneAsync(post => post.Id == postId);
        }

    }
}