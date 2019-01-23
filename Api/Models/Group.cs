using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookUpApi.Models
{
    public class Group
    {
        public ObjectId Id { get; set; }
        [BsonElement("groupName")] public string GroupName { get; set; }
        
        [BsonElement("isPrivate")] public bool PrivateStatus { get; set; }
        
        [BsonElement("password")] public string Password { get; set; }
        
        [BsonElement("groupPhoto")] public Uri GroupPhoto { get; set; }
        
        [BsonElement("ownerId")] public string OwnerId { get; set; }    
        
        [BsonElement("posts")] public Post[] Posts { get; set; }
    }
}