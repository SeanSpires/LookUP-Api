using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookUpApi.Models
{
    public class Comment
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("description")] public string Description { get; set; }
        
      //  [BsonElement("date")] public DateTime DateTime { get; set; }     
        
        //[BsonElement("user")] public User User { get; set; }
        
        [BsonElement("avatar")] public string Avatar { get; set; }
        
        [BsonElement("user")] public string User { get; set; }
        
        [BsonElement("videoThumbnail")] public string VideoThumbnail { get; set; }
        
        [BsonElement("videoURL")] public string VideoUrl { get; set; }
           
        [BsonElement("mediaFiles")] public string[] MediaFiles { get; set; }
        
       // [BsonElement("favourites")] public int Favourites { get; set; }
    }
}