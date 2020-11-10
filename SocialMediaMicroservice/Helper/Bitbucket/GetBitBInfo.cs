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
        ResponseModel response = new ResponseModel();

        #region Post token for Bitbucket user
        public ResponseModel Run(string access_token)
        {
            response.Success = false; 
            try
            {
                string url = string.Format("https://bitbucket.org/!api/2.0/user?access_token={0}", access_token); 
                var userInfoStr = CommonBitbucketService.ProcessWebClientRequest(url);
                if (!string.IsNullOrEmpty(userInfoStr))
                {
                    response.Success = true;
                    //var userInfo = JsonConvert.DeserializeObject<BitbucketModel>(userInfoStr);
                    response.Data = userInfoStr;
                    response.Status_Code = 200;
                }
                else
                {
                    response.Status_Code = 400;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
        #endregion

        #region Access token for bitbucket
        public async Task<ResponseModel> RunAccess(string message)
        {
            response.Success = false;
            //bool isSuccess = false;
            try
            {
                response = CommonBitbucketService.BitAccess_token();
                response.Success = true;
            }
            catch(Exception ex) { }
            return response;
        }
        #endregion
        #region CallBack to site in Git
        public async Task<ResponseModel> CallBackToBitbucket(string code)
        {
            response.Success = false;
            try
            {
                response = await CommonBitbucketService.BitbucketCallback(code);
                if (response.Data != null)
                {
                    response.Success = true;
                    response.Status_Code = 200;
                }
            }
            catch (Exception ex) { response.Message = ex.Message; }
            return response;
        }
        #endregion
    }
}
