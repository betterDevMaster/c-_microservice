using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class YoutubeModel
    {
    }
    public class YoutubeSearchModel
    {
        public string title { get; set; }
        public string url { get; set; }
    }
    public class YoutubeUploadModel
    {
        public string title { get; set; }
        public IFormFile filepath { get; set; }
        public string description { get; set; }


    }
}
