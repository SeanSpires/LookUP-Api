using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LookUpApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace LookUpApi.Services
{
    public class GroupService
    {
        private readonly IMongoCollection<Group> _groups;
        private readonly IMongoDatabase _database;

        public GroupService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("LookUpDb"));
            _database = client.GetDatabase("LookUP");
            _groups = _database.GetCollection<Group>("Group");
        }

        public  Task<Group> Get(string groupName)
        {
            var groupsAsQueryable = _groups.AsQueryable();
            var groupToGet = groupsAsQueryable.FirstAsync(group => group.GroupName == groupName);
            return groupToGet;
        }

        public  void Create(Group group)
        {
            _groups.InsertOneAsync(group);
        }

        public void Update(string groupName, Group groupIn)
        {
            var groupInName = groupIn.GroupName;
             _groups.ReplaceOneAsync(group => group.GroupName == groupInName, groupIn);
        }

        public void Remove(Group groupToRemove)
        {
            _groups.DeleteOneAsync(group => group.GroupName == groupToRemove.GroupName);
        }

        public void Remove(string groupName)
        {
             _groups.DeleteOneAsync(group => group.GroupName == groupName);
        }

    }
}