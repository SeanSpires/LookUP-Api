using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookUpApi.Models
{
    public class Comment
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("description")] public string Description { get; set; }
        
        [BsonElement("date")] public DateTime DateTime { get; set; }
        
        [BsonElement("user")] public User User { get; set; }
        
        [BsonElement("videoThumbnail")] public Uri VideoThumbnail { get; set; }
        
        [BsonElement("videoUri")] public Uri VideoUri { get; set; }
           
        [BsonElement("images")] public Uri[] Images { get; set; }
        
        [BsonElement("favourites")] public int Favourites { get; set; }
    }
}