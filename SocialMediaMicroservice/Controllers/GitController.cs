using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Git;
using SocialMediaMicroservice.Model;
using SocialMediaMicroservice.Model.GitModel;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GitController : ControllerBase
    {
        [HttpGet]
        public IActionResult GitGet(string access_token)
        {
           ResponseModel response = new GitInfo().AuthoriseGit(access_token);
            return Ok(response);
        }
        [HttpPost]
        //POST https://github.com/login/oauth/access_token
        public async Task<IActionResult> CallBackToSite(RedirectBack model)
        {
            ResponseModel response = await new GitInfo().CallBackToSite(model);
            return Ok(response);
        }
    }
}