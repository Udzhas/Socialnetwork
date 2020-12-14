using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace socialnetwork_2.Services
{
    class NickInfo
    {
        [DataContract]
        public class NickInfo
        {
            [DataMember]
            public string NickName { get; set; }

            public NickInfo()
            {

            }
            public NickInfo(string NickName)
            {
                this.NickName = NickName;
            }
        }
    }
}
