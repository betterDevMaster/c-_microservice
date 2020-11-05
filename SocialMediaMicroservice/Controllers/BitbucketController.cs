using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Bitbucket;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BitbucketController : ControllerBase
    {
       
        [HttpGet]
        public IActionResult GetBitInfo(string access_token)
        {
            var result = new GetBitBInfo().Run(access_token);
            return Ok(result ? "success" : "error");
        }
    }
}