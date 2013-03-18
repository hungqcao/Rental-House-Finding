using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentalHouseFinding.Models
{
    public class QuestionViewModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string TitleQuestion { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string ContentQuestion { get; set; }

        public int UserId { get; set; }
    }
}