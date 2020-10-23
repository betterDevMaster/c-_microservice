using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMediaMicroservice.Helper.Twitter
{
    public class ReadRetweets
    {
        public List<TwitterUserTweeModel> Run(string tweetId)
        {
            List<TwitterUserTweeModel> userTweets = new List<TwitterUserTweeModel>();

            string resourceUrlFormat = "https://api.twitter.com/1.1/statuses/retweets/{0}.json";
            var retweetsUrl = string.Format(resourceUrlFormat, tweetId);

            // oauth application keys
            var oauth_token = "xxx";
            var oauth_token_secret = "xxx";
            string authHeader = TwitterCommonServices.GetRequestAuthHeader(retweetsUrl, "GET", oauth_token, oauth_token_secret);

            var response = TwitterCommonServices.ProcessWebRequest(authHeader, "GET", retweetsUrl);

            return userTweets;
        }
    }
}
