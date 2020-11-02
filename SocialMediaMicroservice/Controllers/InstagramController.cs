using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Instagram;
using SocialMediaMicroservice.Model;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstagramController : Controller
    {
        [HttpPost]
        public IActionResult UserMedia(string access_token)
        {
            var result = new UserMedia().Run(access_token);
            return Ok(result ? "success" : "error");
        }
    }
}