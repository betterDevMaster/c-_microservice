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

namespace SocialMediaMicroservice.Helper.Bitbucket
{
    public class CommonBitbucketService
    {
        private const string BitbucketKey = "t5GMUyeJxhXbsWCC2U";
        private const string BitbucketSecret = "WsvTBUjcPMjGKuEy4dRYuaHmBMVMB5QC";
        private const string BitbucketScopes = "snippet+repository%3Awrite+project%3Awrite+pipeline+account+pullrequest+webhook&expires_in=7200";
        private const string BitbucketAuthFormat = "https://bitbucket.org/site/oauth2/authorize?client_id={0}&response_type=token";

        //"https://bitbucket.org/site/oauth2/authorize?client_id{0}&response_type=token";
        public static string BitAccess_token()
        {
            string accessToken = string.Empty;
            string url = string.Format(BitbucketAuthFormat, BitbucketKey, BitbucketSecret);

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

            //var request = WebRequest.Create(url) as HttpWebRequest;
            //string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(BitbucketKey));
            //request.Headers.Add("Authorization", "Basic " + credentials);

            //using (var response = request.GetResponse() as HttpWebResponse)
            //{
            //    var reader = new StreamReader(response.GetResponseStream());
            //    string json = reader.ReadToEnd();
            //}

            if (accessToken.Trim().Length == 0)
                throw new Exception("No action token detected!");
            return accessToken;
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
