using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBLogin.Models
{
    public class FacebookLoginModel
    {
        public string uid { get; set; }
        public string accessToken { get; set; }
    }
}