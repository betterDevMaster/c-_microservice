using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.LinkedIn;

namespace SocialMediaMicroservice.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LinkedInController : ControllerBase
	{
		internal async Task<IActionResult> Search()
		{
			var redirectUrl = "http://localhost:65037/api/linkedin/search";
			//var redirectUrl = "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=86s5o30nk8bg5b&redirect_uri=http%3A%2F%2Fdev.example.com%2Fauth%2Flinkedin";
			string code = string.Empty;
			string linkedInClientId = "86s5o30nk8bg5b";
			string linkedInClientSecret = "UcMDWoJacePkX7MW";

			var profile = await new Search().ReadMyProfile(code, redirectUrl, linkedInClientId, linkedInClientSecret);
			var jsonProfile = Newtonsoft.Json.JsonConvert.SerializeObject(profile);
			return Content(jsonProfile);
		}
	}
}
