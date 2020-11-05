using Newtonsoft.Json;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Bitbucket
{
    public class GetBitBInfo
    {
        public bool Run(string access_token)
        {
            bool isSuccess = false;

            try
            {
                string url = string.Format("https://bitbucket.org/me?access_token={0}&fields=email,link", access_token);

                var userInfoStr = CommonBitbucketService.ProcessWebClientRequest(url);

                if (!string.IsNullOrEmpty(userInfoStr))
                {
                    isSuccess = true;
                    var userInfo = JsonConvert.DeserializeObject<FacebookUserInfoModel>(userInfoStr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return isSuccess;
        }
    }
}
