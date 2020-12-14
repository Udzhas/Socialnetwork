using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2.Controller.model
{
    class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [BsonIgnoreIfNull]
        public string Name { get; set; }
        [BsonIgnoreIfNull]
        public string Surname { get; set; }
        [BsonIgnoreIfNull]
        public string Mail { get; set; }
        [BsonIgnoreIfNull]
        public string NickName { get; set; }
        [BsonIgnoreIfNull]
        public string Password { get; set; }
        [BsonIgnoreIfNull]
        public List<string> Followers { get; set; }
        [BsonIgnoreIfNull]
        public List<string> Following { get; set; }
        [BsonIgnoreIfNull]
        public string Date { get; set; }
    }
}
