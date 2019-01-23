using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookUpApi.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("description")] public string Description { get; set; }
        
        [BsonElement("date")] public DateTime Date { get; set; }
        
        [BsonElement("user")] public User User{ get; set; }
        
        [BsonElement("group")] public Group Group { get; set; }
        
        [BsonElement("videoThumbnail")] public Uri Uri { get; set; }
        
        [BsonElement("videoUri")] public Uri VideoUri { get; set; }
        
        [BsonElement("images")] public Uri[] Images { get; set; }
        
        [BsonElement("comments")] public Comment[] Comments { get; set; }
    }
}