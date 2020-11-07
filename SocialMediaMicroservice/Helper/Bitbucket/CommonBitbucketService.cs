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

namespace SocialMediaMicroservice.Helper.Bitbucket
{
    public class CommonBitbucketService
    {
        private const string BitbucketKey = "t5GMUyeJxhXbsWCC2U";//U
        private const string BitbucketSecret = "WsvTBUjcPMjGKuEy4dRYuaHmBMVMB5QC";
        private const string BitbucketAuthFormat = "https://bitbucket.org/site/oauth2/authorize?client_id{0}&response_type=token";
        //"https://bitbucket.org/site/oauth2/authorize?client_id{0}&response_type=token";

        #region Access token in bitbucket
        public static ResponseModel BitAccess_token()
        {
            ResponseModel responce = new ResponseModel();
            try
            {
                string accessToken = string.Empty;
                string url = string.Format(BitbucketAuthFormat, BitbucketKey, BitbucketSecret);

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                var OriginUrl = response.ResponseUri.OriginalString;
                responce.Data = OriginUrl;
                responce.Status_Code = 200;
                BitbucketClient client = new BitbucketClient(url, BitbucketKey, BitbucketSecret);

                //***Token
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    
                    NameValueCollection query = HttpUtility.ParseQueryString(responseString);
                    var userAuthData = JsonConvert.DeserializeObject<BitbucketAuthModel>(responseString);
                    accessToken = userAuthData.accessToken;
                }
                if (accessToken.Trim().Length == 0)
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
    }
}
