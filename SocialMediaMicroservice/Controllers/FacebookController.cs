using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Facebook;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        public IActionResult GetUserInfo(string access_token)
        {
            var result = new GetFBUserInfo().Run(access_token);

            return Ok(result ? "success" : "error");
        }
    }
}