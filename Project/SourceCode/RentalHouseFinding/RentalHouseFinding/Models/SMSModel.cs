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
        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại.")]
        [Display(Name = "Số điện thoại")]
        [StringLength(11, ErrorMessage = @"{0} tối đa là {1} ký tự. Vui lòng nhập lại")]
        [RegularExpression("(([0+])([0-9]+))", ErrorMessage = "Sai định dạng,xin vui lòng nhập lại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập nội dung theo cú pháp: MS Mã tin")]        
        [Display(Name = "Nội dung")]
        public string ContentSMS { get; set; }
        
    }
}
