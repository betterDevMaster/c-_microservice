using Newtonsoft.Json;
using SocialMediaMicroservice.Helper.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Instagram
{
    public class UserMedia
    {
        public bool Run(string access_token)
        {
            bool isSuccess = false;

            try
            {
                string url = string.Format("https://graph.instagram.com/me?access_token={0}&fields=email,name,first_name,last_name,link", access_token);

                var userInfoStr = CommonFacebookServices.ProcessWebClientRequest(url);

                if (!string.IsNullOrEmpty(userInfoStr))
                {
                    isSuccess = true;
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
