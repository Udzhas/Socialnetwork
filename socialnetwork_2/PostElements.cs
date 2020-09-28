using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2
{
    class PostElements
    {
        [BsonId]
        public ObjectId Id {get;set;}
        [BsonElement("name")]
        public String name { get; set; }
        [BsonElement("content")]
        public String content { get; set; }
        [BsonElement("date")]
        public DateTime date { get; set; }
        [BsonElement("likes")]
        public int likes { get; set; }
        [BsonElement("comments")]
        public List<String> comments { get; set; }
        [BsonElement("user_id")]
        public String user_id { get; set; }

        public PostElements(string name, string content, DateTime date, int likes, List<String> comments, string user_id)
        {
            this.name = name;
            this.content = content;
            this.date = date;
            this.likes = likes;
            this.comments = new List<string>();
            this.comments.AddRange(comments);
            this.user_id = user_id;
        }
    }
}
