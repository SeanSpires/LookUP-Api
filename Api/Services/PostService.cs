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
        
        public Task<Post> Get(ObjectId postId)
        {
            var postsAsQueryable = _posts.AsQueryable();
            var postToGet = postsAsQueryable.FirstAsync(post => post.Id == postId);
            return postToGet;
        }

        public  void Create(Post post)
        {
            _posts.InsertOneAsync(post);
        }

        public void Update(ObjectId postId, Post postIn)
        {
            _posts.ReplaceOneAsync(post => post.Id == postId, postIn);
        }

        public void Remove(Post postToRemove)
        {
            
            _posts.DeleteOneAsync(post => post.Id == postToRemove.Id);
        }

        public void Remove(ObjectId postId)
        {
            _posts.DeleteOneAsync(post => post.Id == postId);
        }

    }
}