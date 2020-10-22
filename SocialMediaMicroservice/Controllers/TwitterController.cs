﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Twitter;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        public async Task<IActionResult> Tweet(string tweetText)
        {
            var result = await new PostTweet().Run(tweetText);

            return Ok(result ? "success" : "error");
        }
    }
}
