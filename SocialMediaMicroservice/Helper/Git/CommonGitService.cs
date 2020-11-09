using Newtonsoft.Json;
using Octokit;
using SocialMediaMicroservice.Model;
using SocialMediaMicroservice.Model.GitModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialMediaMicroservice.Helper.Git
{
    public class CommonGitService
    {

        private const string GitClientID = "c06475b6dc80571ed5a4";
        //private const string GitClientID = "c06475b6dc80571ed5a4"; 
        private const string GitClientSecret = "039270f5d81269af6c6d35679d16ad63dff9db63";
        private const string GitAuthorize = "https://github.com/login/oauth/authorize?scope=user:email&client_id={0}";
        private const string AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        private const string TokenEndpoint = "https://github.com/login/oauth/access_token";
        private const string UserInformationEndpoint = "https://api.github.com/user";
        private static readonly HttpClient client = new HttpClient();
       
        public static string GitAuthenticatePage()
        {
            string accessToken = string.Empty;
            //gitclient.Credentials = new Credentials(accessToken);
            string url = string.Format(GitAuthorize, GitClientID);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            accessToken = response.ResponseUri.OriginalString;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                NameValueCollection query = HttpUtility.ParseQueryString(responseString);
                //var userAuthData = JsonConvert.DeserializeObject<BitbucketAuthModel>(responseString);
                //accessToken = userAuthData.accessToken;
            }
            return accessToken;
        }
        public async static Task<ResponseModel> GitCallback(RedirectBack redirect)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var Callback = new RedirectBack();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(redirect), Encoding.UTF8, "application/json");

                    using (var re = await httpClient.PostAsync(TokenEndpoint, content))
                    {
                        string apiResponse = await re.Content.ReadAsStringAsync();
                        response.Data = apiResponse;
                        //Callback = JsonConvert.DeserializeObject<RedirectBack>(apiResponse);
                    }
                }
            }
            catch (Exception ex) 
            {
                response.Message = ex.Message; 
            }
            return response;
        }
    }
}

