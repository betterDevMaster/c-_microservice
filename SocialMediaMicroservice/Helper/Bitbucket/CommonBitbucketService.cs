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
using Bitbucket.Net;
using Flurl.Http;
using System.Net.Http;

namespace SocialMediaMicroservice.Helper.Bitbucket
{
    public class CommonBitbucketService
    {

        private const string BitbucketKey = "a6x9YfwRArk8CXKmWB";//"t5GMUyeJxhXbsWCC2U";
        private const string BitbucketSecret = "Whbn8X58mGMdQzVxNPHZ4qHSrBYSnG9a";//"WsvTBUjcPMjGKuEy4dRYuaHmBMVMB5QC";
        private const string BitbucketAuthFormat = "https://bitbucket.org/site/oauth2/authorize?client_id={0}&response_type=token";
                                                 //"https://bitbucket.org/site/oauth2/authorize?client_id{0}&response_type=token";
        private const string BitbucketToken = "https://bitbucket.org/site/oauth2/access_token";

        #region Access token in bitbucket
        public static ResponseModel BitAccess_token()
        {
            ResponseModel responce = new ResponseModel();
            try
            {
                //string urls = "https://bitbucket.org/!api/2.0/user/";
                //string margecredentials = string.Format("{0}:{1}", "t5GMUyeJxhXbsWCC2U", "WsvTBUjcPMjGKuEy4dRYuaHmBMVMB5QC");
                //byte[] bytecredential = UTF8Encoding.UTF8.GetBytes(margecredentials);
                //var cre = Convert.ToBase64String(bytecredential);
                //var res = WebRequest.Create(urls) as HttpWebRequest;
                //res.ContentType = "application/json";
                //res.Method = "GET";

                //res.Headers.Add("Authorization", "Basic" + cre);
                //using (var resp = res.GetResponse() as HttpWebResponse)
                //{
                //    var reader = new StreamReader(resp.GetResponseStream());
                //    var json = reader.ReadToEnd();
                //}

                string accessToken = string.Empty;
                string url = string.Format(BitbucketAuthFormat, BitbucketKey, BitbucketSecret);

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                var OriginUrl = response.ResponseUri.OriginalString;

                responce.Data = url;
                responce.Status_Code = 200;
                if (accessToken.Trim().Length == 1)
                throw new Exception("No action token detected!");
            }
            catch (Exception ex)
            {
                responce.Message = ex.Message;
            }
            return responce;
        }
        #endregion

        #region Process of webclientRequest 
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
        #endregion
        public async static Task<ResponseModel> BitbucketCallback(string code)
        {
            BitbucketRedirect redirect = new BitbucketRedirect();
            redirect.client_id = BitbucketKey;
            redirect.client_secret = BitbucketSecret;
            redirect.code = code;
            redirect.grant_type = "authorization_code";
            ResponseModel response = new ResponseModel();
            try
            {
                //var Callback = new BitbucketRedirect();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(redirect), Encoding.UTF8, "application/x-www-form-urlencoded");
                    using (var re = await httpClient.PostAsync(BitbucketToken, content))
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
