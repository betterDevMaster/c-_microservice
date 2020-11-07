using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaMicroservice.Model.GitModel
{
    public class GitModels
    {
        [Required]
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
        public string login { get; set; }
        public string scope { get; set; }//user:email
        public string state { get; set; }
        public string allow_signup { get; set; }//default true;
    }

    public class RedirectBack
    {
        [Required]
        public string client_id { get; set; }
        [Required]
        public string client_secret { get; set; }
        [Required]
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string state { get; set; }
    }
}
