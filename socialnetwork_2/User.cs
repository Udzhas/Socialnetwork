using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2
{
    class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("username")]
        public String userName { get; set; }
        [BsonElement("password")]
        public String password { get; set; }
        public User(String userName, String password)
        {
            this.userName = userName;
            this.password = password;
        }
    }
}
