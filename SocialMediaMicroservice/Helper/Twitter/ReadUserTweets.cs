using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialMediaMicroservice.Helper.Twitter
{
    public class ReadUserTweets
    {
        public List<TwitterUserTweeModel> Run(string screen_name, int maxNumberOfTweets)
        {
            List<TwitterUserTweeModel> userTweets = new List<TwitterUserTweeModel>();
            
            string resource_url = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        
            // oauth application keys
            var oauth_token = "xxx";
            var oauth_token_secret = "xxx";

            string authHeader = TwitterCommonServices.GetRequestAuthHeader(resource_url, "GET", oauth_token, oauth_token_secret);

            // make the request
            var postBody = "screen_name =" + Uri.EscapeDataString(screen_name) + "&count =" + maxNumberOfTweets;
            resource_url += "?" + postBody;

            var response = TwitterCommonServices.ProcessWebRequest(authHeader, "GET", resource_url);
            return userTweets;
        }
    }
}