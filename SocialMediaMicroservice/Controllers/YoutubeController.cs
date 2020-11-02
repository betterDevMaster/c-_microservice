using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Youtube;
using SocialMediaMicroservice.Model;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YoutubeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Search(string searchtext)
        {
            var result = await new Search().Run(searchtext);
            return Ok(result);
        }

        [HttpPost]
        // Dinh Truong, 2nd November 2020 23:30, delete start
        //public async Task<IActionResult> UploadVideo([FromBody] YoutubeUploadModel obj)
        // Dinh Truong, 2nd November delete end

        public async Task<IActionResult> UploadVideo([FromForm]YoutubeUploadModel obj)
        {
            var result = await new UploadVideo().Run(obj);
            return Ok(result ? "success" : "error");
        }
    }
}