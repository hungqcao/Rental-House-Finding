﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace RentalHouseFinding.Models
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu mới")]
        [StringLength(100, ErrorMessage = @"{0} phải có ít nhất {2} kí tự.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("NewPassword", ErrorMessage = @"Mật khẩu mới và mật khẩu xác nhận không trùng nhau.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        [Required(ErrorMessage = "xin vui lòng nhập tài khoản")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "ghi nhớ?")]
        public bool RememberMe { get; set; }


    }

    public class RegisterModel
    {
        [Required(ErrorMessage= "Xin vui lòng gõ tên đăng nhập.")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự, xin vui lòng nhập lại.")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự, xin vui lòng nhập lại.")]
        [Required(ErrorMessage = "Xin vui lòng nhập email.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage= "Xin vui lòng nhập mật khẩu")]
        [StringLength(20, ErrorMessage = @"{0} phải tối thiểu là {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = @"Mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại.")]
        [Display(Name = "Số điện thoại")]
        [MaxLength(15, ErrorMessage = "Không được vượt quá 15 ký tự, xin vui lòng nhập lại.")]
        [RegularExpression("(([0+])([0-9]+))", ErrorMessage = "Sai định dạng,xin vui lòng nhập lại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Tên thật")]
        public string Name { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date, ErrorMessage = "Xin vui lòng nhập đúng ngày tháng năm")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = @"Xin vui lòng chọn giới tính!")]
        [MaxLength(10, ErrorMessage = "Không được vượt quá 10 ký tự,xin vui lòng nhập lại")]
        [UIHint("Sex")]
        public string Sex { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }
    }

    public class UserDetailsModel
    {
        public string OpenID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
