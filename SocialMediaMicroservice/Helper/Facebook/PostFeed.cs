using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Facebook
{
    public class PostFeed
    {
        public async Task<bool> Run(string message)
        {
            bool isSuccess = false;

            try
            {
                var accessToken = CommonFacebookServices.GetAccessToken();

                FacebookClient facebookClient = new FacebookClient(accessToken);

                dynamic messagePost = new ExpandoObject();
                messagePost.access_token = accessToken;
                //messagePost.picture = "[A_PICTURE]";
                //messagePost.link = "[SOME_LINK]";
                //messagePost.name = "[SOME_NAME]";
                //messagePost.caption = "my caption"; 
                messagePost.message = message;
                //messagePost.description = "my description";

                var userId = CommonFacebookServices.GetProfileId(accessToken);

                var result = facebookClient.Post(userId + "/feed", messagePost);

                if (!string.IsNullOrEmpty(result))
                {
                    isSuccess = true;
                }

                Console.WriteLine(result);
            }
            catch (FacebookOAuthException ex)
            {
                //handle something
            }
            catch (Exception ex)
            {
                //handle something else
            }

            return isSuccess;
        }
    }
}
