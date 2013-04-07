using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RentalHouseFinding.Models
{
    public class SMSModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập Số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập nội dung theo cú pháp: MS Mã tin")]
        [Display(Name = "Nội dung")]
        public string ContentSMS { get; set; }
        
    }
}
