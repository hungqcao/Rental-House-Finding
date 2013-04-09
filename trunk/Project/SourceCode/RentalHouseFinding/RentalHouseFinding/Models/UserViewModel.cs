using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalHouseFinding.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;
    using RentalHouseFinding.ValidateAttr;
    [KnownType(typeof(UserViewModel))]
    public class UserViewModel
    {
        [DisplayName(@"UserId")]
        public int UserId
        {
            get;
            set;
        }
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [RequiredIfOtherFieldIsNull("PhoneNumber")]
        [StringLength(50, ErrorMessage = @"{0} tối đa là {1} ký tự. Vui lòng nhập lại.")]
        //[Required(ErrorMessage = "Xin vui lòng nhập email.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email không hợp lệ.")]
        [Remote("IsEmailEditAvailable", "Validation", AdditionalFields = "UserName")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu.")]
        [StringLength(1000, ErrorMessage = @"{0} tối thiểu là {2} ký tự.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = @"Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(15, ErrorMessage = @"{0} tối đa là {1} ký tự.")]
        [RegularExpression("(([0+])([0-9]+))", ErrorMessage = "Sai định dạng,xin vui lòng nhập lại")]
        [RequiredIfOtherFieldIsNull("Email")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Họ và tên")]
        [StringLength(20, ErrorMessage = @"{0} tối đa là {1} ký tự.")]        
        public string Name { get; set; }
    }
}
