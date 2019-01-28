using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LookUpApi.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("desc")] public string Desc { get; set; }
        
        [BsonElement("date")] public DateTime Date { get; set; }
        
        [BsonElement("avatar")] public string Avatar { get; set; }
        
        [BsonElement("user")] public string User { get; set; }
        
      //  [BsonElement("group")] public Group Group { get; set; }
        
        [BsonElement("videoThumbnail")] public string VideoThumbnail { get; set; }

        [BsonElement("videoURL")] public string VideoURL { get; set; }
        
        [BsonElement("mediaFiles")] public string[] MediaFiles { get; set; }
        
        [BsonElement("comments")] public Comment[] Comments { get; set; }
                
        [BsonElement("favourites")] public int Favourites { get; set; }
        
        [BsonElement("postOrigin")] public string PostOrigin { get; set; }
        
    }
}