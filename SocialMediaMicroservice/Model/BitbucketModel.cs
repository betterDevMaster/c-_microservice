using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class BitbucketModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("repositories")]
        public string Repositories { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("self")]
        public string Self { get; set; }
        [JsonProperty("links")]
        public string Links { get; set; }
        [JsonProperty("account_id")]
        public string Account_id { get; set; }
        [JsonProperty("snippets")]
        public string Snippets { get; set; }
    }
    public class BitbucketAuthModel
    {
        [JsonProperty("access_token")]
        public string accessToken { get; set; }
        [JsonProperty("token_type")]
        public string tokenType { get; set; }
    }
}
