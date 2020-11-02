using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class InstagramModel
    {
    }

    public class InstagramAddPostModel
    {
        public string caption { get; set; }
        public IFormFile file { get; set; }
    }
}
