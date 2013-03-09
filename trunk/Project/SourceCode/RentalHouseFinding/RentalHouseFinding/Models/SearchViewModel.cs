using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace RentalHouseFinding.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        [Display(Name = "Thể loại")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh thành phố")]
        [Display(Name = "Tỉnh, thành phố")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn quận, huyện")]
        [Display(Name = "Quận, huyện")]
        public int DistrictId { get; set; }

        [Display(Name = "Từ khóa")]
        public string KeyWord { get; set; }
    }
}