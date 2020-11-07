using SocialMediaMicroservice.Model;
using SocialMediaMicroservice.Model.GitModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Helper.Git
{
    public class GitInfo
    {
        ResponseModel response = new ResponseModel();

        #region Authorize somthing else
        public ResponseModel AuthoriseGit(string access_token)
        {
            response.Success = false;
            try
            {
                var userInfoStr = CommonGitService.GitAuthenticatePage();
                if (!string.IsNullOrEmpty(userInfoStr))
                {
                    response.Data = userInfoStr;
                    response.Success = true;
                    response.Status_Code = 200;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region CallBack to site in Git
        public async Task<ResponseModel> CallBackToSite(RedirectBack redirect)
        {
            response.Success = false;
            try
            {
              response = await CommonGitService.GitCallback(redirect);
                if(response.Data != null)
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

