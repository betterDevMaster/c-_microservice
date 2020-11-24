using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaMicroservice.Helper.Drive;
using SocialMediaMicroservice.Model;

namespace SocialMediaMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriveController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get(DriveSearchModel model)
        {
            var result = await new DownloadFile().GetFileID(model);
            return Ok(result);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(DriveUploadModel model)
        {
            try
            {
                var file = Request.Form.Files[0];                
            }
            catch(Exception ex)
            {

            }            
            return Ok();
        }
    }
}