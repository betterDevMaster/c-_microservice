using Newtonsoft.Json;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitbucket.Net;

namespace SocialMediaMicroservice.Helper.Bitbucket
{
    public class GetBitBInfo
    {
        #region Post token for Bitbucket user
        public bool Run(string access_token)
        {
            bool isSuccess = false;

            try
            {
                string url = string.Format("https://bitbucket.org/!api/2.0/user?access_token={0}", access_token); 

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
        #endregion

        #region Access token for bitbucket
        public async Task<bool> RunAccess(string message)
        {
            bool isSuccess = false;
            try
            {
                var accessToken = CommonBitbucketService.BitAccess_token();
            }
            catch(Exception ex) { }
            return isSuccess;
        }
        #endregion
    }
}
