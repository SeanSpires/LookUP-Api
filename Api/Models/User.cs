using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookUpApi.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("avatar")] public Uri Avatar { get; set; }
        
        [BsonElement("username")] public string Username { get; set; }
        
        [BsonElement("groups")] public Group[] Groups { get; set; }
    }
}