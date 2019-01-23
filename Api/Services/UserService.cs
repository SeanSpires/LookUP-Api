using System.Threading.Tasks;
using LookUpApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LookUpApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoDatabase _database;
        
        public UserService(IConfiguration configuration)
        {    
            var client = new MongoClient(configuration.GetConnectionString("LookUpDb"));
            _database = client.GetDatabase("LookUP");
            _users = _database.GetCollection<User>("User");        
        }
        
        public Task<User> Get(ObjectId userId)
        {
            var usersAsQueryable = _users.AsQueryable();
            var userToGet = usersAsQueryable.FirstAsync(user => user.Id == userId);
            return userToGet;
        }
        
        public void Create(User user)
        {
            _users.InsertOneAsync(user);
        }
        
        public void Update(ObjectId userId, User userIn)
        {
            _users.ReplaceOneAsync(user => user.Id == userId, userIn);
        }

        public void Remove(User userToRemove)
        {
            
            _users.DeleteOneAsync(user => user.Id == userToRemove.Id);
        }

        public void Remove(ObjectId userId)
        {
            _users.DeleteOneAsync(user => user.Id == userId);
        }
        
        
    }
}