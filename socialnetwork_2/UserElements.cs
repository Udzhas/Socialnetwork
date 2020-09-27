using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2
{
    class UserElements
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("username")]
        public String userName { get; set; }
        [BsonElement("password")]
        public String password { get; set; }
        [BsonElement("first_name")]
        public String first_name { get; set; }
        [BsonElement("last_name")]
        public String last_name { get; set; }

        [BsonElement("interests")]
        public List<string> interests { get; set; }


        public UserElements(string userName, string password, string first_name, string last_name, List<string> interests)
        {
            this.userName = userName;
            this.password = password;
            this.first_name = first_name;
            this.last_name = last_name;
            this.interests = new List<string>();
            this.interests.AddRange(interests);
        }

    }
}
