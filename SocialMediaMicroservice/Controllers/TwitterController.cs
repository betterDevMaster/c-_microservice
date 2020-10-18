using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaMicroservice.Controllers
{
    public class TwitterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
