using Newtonsoft.Json;
using SocialMediaMicroservice.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace SocialMediaMicroservice.Helper.Facebook
{
    public class GetFBUserInfo
    {
        public const string FaceBookAppKey = "2557278674510925";

        public bool Run(string access_token)
        {
            bool isSuccess = false;
            try
            {
                string url = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name,first_name,last_name,link", access_token);
                var userInfoStr = CommonFacebookServices.ProcessWebClientRequest(url);

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
