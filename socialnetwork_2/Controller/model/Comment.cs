using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2.Controller.model
{
    class Comment
    {
        [BsonIgnoreIfDefault]
        public ObjectId CommentOwnerId { get; set; }
        [BsonIgnoreIfNull]
        public string Text { get; set; }
        [BsonIgnoreIfNull]
        public string Date { get; set; }
    }
}
