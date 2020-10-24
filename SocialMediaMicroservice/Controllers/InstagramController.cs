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
        public async Task<IActionResult> AddPost([FromBody]InstagramAddPostModel obj)
        {
            var result = await new AddPost().Run(obj);
            return Ok(result ? "success" : "error");
        }
    }
}