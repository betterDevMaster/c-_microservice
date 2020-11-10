using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Bitbucket;
using SocialMediaMicroservice.Model;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BitbucketController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBitInfo(string details)
        {
            ResponseModel result = new GetBitBInfo().Run(details);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> PostBitbucket(string message)
        {
            ResponseModel response;
            response = await new GetBitBInfo().RunAccess(message);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetToken(string code)
        {
            ResponseModel response = await new GetBitBInfo().CallBackToBitbucket(code);
            return Ok(response);
        }
    }
}