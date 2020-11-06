using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public int Status_Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
