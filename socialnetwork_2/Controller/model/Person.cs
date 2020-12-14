using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace socialnetwork_2.Controller.model
{
    class Person
    {
        [JsonProperty(PropertyName = "<id>")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public string Mail { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string NickName { get; set; }
    }
}
