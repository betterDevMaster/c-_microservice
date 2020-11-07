using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class DriveModel
    {
    }

    public class DriveSearchModel
    {
        public string accesstoken { get; set; }
        public string filename { get; set; }
    }

    public class DriveUploadModel
    {
        public string accesstoken { get; set; }
        public string location { get; set; }
    }
}
