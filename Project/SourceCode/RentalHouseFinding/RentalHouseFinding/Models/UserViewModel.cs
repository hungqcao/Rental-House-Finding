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
        [MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự, xin vui lòng nhập lại.")]
        //[Required(ErrorMessage = "Xin vui lòng nhập email.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email không hợp lệ.")]
        [Remote("IsEmailEditAvailable", "Validation", AdditionalFields = "UserName")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu.")]
        [StringLength(20, ErrorMessage = @"{0} phải tối thiểu là {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = @"Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Số điện thoại")]
        [MaxLength(15, ErrorMessage = "Không được vượt quá 15 ký tự, xin vui lòng nhập lại.")]
        [RegularExpression("(([0+])([0-9]+))", ErrorMessage = "Sai định dạng,xin vui lòng nhập lại")]
        [RequiredIfOtherFieldIsNull("Email")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Tên thật")]
        public string Name { get; set; }
    }
}
