using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentalHouseFinding.Models
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string ContentAnswer { get; set; }

        public int QuestionId { get; set; }
    }
}