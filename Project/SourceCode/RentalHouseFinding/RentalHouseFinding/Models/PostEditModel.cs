using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace RentalHouseFinding.Models
{
    public class PostEditModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mã số tin đăng mà chúng tôi cung cấp")]
        [Display(Name = "Mã số tin")]
        [Min(0, ErrorMessage = "Id phải lớn hơn hoặc bằng 0")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu chúng tôi cung cấp")]
        [Display(Name = "Mật khẩu")]
        [MaxLength(100, ErrorMessage = "Không được vượt quá 100 ký tự, xin vui lòng nhập lại.")]
        public string Password { set; get; }
    }
}