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

        public async Task<Group> Get(string groupName)
        {
            var groupsAsQueryable = _groups.AsQueryable();
            var groupToGet = await groupsAsQueryable.FirstAsync(group => group.GroupName == groupName);
            return groupToGet;
        }

        public async void Create(Group group)
        {
            await _groups.InsertOneAsync(group);
        }

        public async void Update(string groupName, Group groupIn)
        {
            var groupInName = groupIn.GroupName;
            await _groups.ReplaceOneAsync(group => group.GroupName == groupInName, groupIn);
        }

        public async void Remove(Group groupToRemove)
        {
            await _groups.DeleteOneAsync(group => group.GroupName == groupToRemove.GroupName);
        }

        public async void Remove(string groupName)
        {
            await _groups.DeleteOneAsync(group => group.GroupName == groupName);
        }

    }
}