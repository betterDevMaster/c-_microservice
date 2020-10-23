using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Twitter
{
    public class PostTweet
    {
        public async Task<bool> Run(string tweetText)
        {
            bool isTweetSuccess = false;
            string twitterURL = "https://api.twitter.com/1.1/statuses/update.json";

            string oauth_token = "oauth_token";
            string oauth_token_secret = "oauth_token_secret";

            // create oauth signature
            //string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";

            //string baseString = string.Format(
            //    baseFormat,
            //    oauth_consumer_key,
            //    oauth_nonce,
            //    oauth_signature_method,
            //    oauth_timestamp, oauth_token,
            //    oauth_version,
            //    Uri.EscapeDataString(tweetText)
            //);

            string authHeader = TwitterCommonServices.GetRequestAuthHeader(twitterURL, "POST", oauth_token, oauth_token_secret);

            var response = TwitterCommonServices.ProcessWebRequest(authHeader, "GET", twitterURL);

            return isTweetSuccess;
        }
    }
}
