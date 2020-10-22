using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class FacebookModel
    {
    }

    public class FacebookUserInfoModel
    {
        public string id { get; set; }
        public string name { get; set; }
        [JsonProperty("firstName")]
        public string first_name { get; set; }
        [JsonProperty("lastName")]
        public string last_name { get; set; }
        [JsonProperty("emailAddress")]
        public string email { get; set; }
        
    }
}
