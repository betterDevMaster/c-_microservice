using Facebook;
using Newtonsoft.Json;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialMediaMicroservice.Helper.Facebook
{
    public static class CommonFacebookServices
    {
        private const string FacebookApiId = "213056766646386";
        private const string FacebookApiSecret = "6720e0f89389885e142486a220e6fc89";

        private const string AuthenticationUrlFormat =
        "https://graph.facebook.com/oauth/access_token?client_id={0}&client_secret={1}&grant_type=client_credentials&scope=manage_pages,offline_access,publish_stream";


        public static string GetAccessToken()
        {
            string accessToken = string.Empty;
            string url = string.Format(AuthenticationUrlFormat, FacebookApiId, FacebookApiSecret);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();

                NameValueCollection query = HttpUtility.ParseQueryString(responseString);

                var userAuthData = JsonConvert.DeserializeObject<FacebookAuthModel>(responseString);

                accessToken = userAuthData.accessToken;
            }

            if (accessToken.Trim().Length == 0)
                throw new Exception("No action token detected!");

            return accessToken;
        }

        public static string GetProfileId(string accessToken)
        {

            if (!string.IsNullOrEmpty(accessToken))
            {
                try
                {
                    var _fb = new FacebookClient(accessToken);
                    dynamic resultMe = _fb.Get("me?fields=id");
                    return resultMe.id;
                }
                catch (FacebookOAuthException)
                {
                    return null;
                }
            }

            return null;
        }

        public static string ProcessWebClientRequest(string url)
        {
            string response = string.Empty;

            try
            {
                WebClient wc = new WebClient();
                Stream data = wc.OpenRead(url);
                StreamReader reader = new StreamReader(data);
                response = reader.ReadToEnd();
                data.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }
    }
}
