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
using System.Collections;
using System.ComponentModel;

namespace RentalHouseFinding.Models
{
    public class SearchViewModel
    {
        public bool IsNormalSearch { get; set; }
        public bool IsAdvancedSearch { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        [Display(Name = "Thể loại")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh thành phố")]
        [Display(Name = "Tỉnh, thành phố")]
        public int ProvinceId { get; set; }

        [Display(Name = "Quận, huyện")]
        public int DistrictId { get; set; }

        [Display(Name = "Từ khóa")]
        public string KeyWord { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PostIdSuggest { get; set; }

        //Advanced search
        [Display(Name = "Diện tích lớn nhất")]
        [Remote("AreaMaxGreaterThanAreaMin", "Validation", AdditionalFields = "AreaMin")]
        [Min(0, ErrorMessage = "Phải lớn hơn hoặc bằng 0")]
        public float? AreaMax { get; set; }
        [Display(Name = "Diện tích nhỏ nhất")]
        [Remote("AreaMinLessThanAreaMax", "Validation", AdditionalFields = "AreaMax")]
        [Min(0, ErrorMessage = "Phải lớn hơn hoặc bằng 0")]
        public float? AreaMin { get; set; }
        [Display(Name = "Giá cao nhất")]
        [Remote("PriceMaxGreaterThanPriceMin", "Validation", AdditionalFields = "PriceMin")]
        [Min(0, ErrorMessage = "Phải lớn hơn hoặc bằng 0")]
        public float? PriceMax { get; set; }
        [Display(Name = "Giá thấp nhất")]
        [Remote("PriceMinLessThanPriceMax", "Validation", AdditionalFields = "PriceMax")]
        [Min(0, ErrorMessage = "Phải lớn hơn hoặc bằng 0")]
        public float? PriceMin { get; set; }


        [Display(Name = "Có Internet?")]
        public Boolean HasInternet { get; set; }
        public int HasInternetScore { get; set; }

        [Display(Name = "Có truyền hình cáp?")]
        public Boolean HasTVCable { get; set; }
        public int HasTVCableScore { get; set; }

        [Display(Name = "Có giường đi kèm?")]
        public Boolean HasBed { get; set; }
        public int HasBedScore { get; set; }

        [Display(Name = "Có bình nóng lạnh?")]
        public Boolean HasWaterHeater { get; set; }
        public int HasWaterHeaterScore { get; set; }

        [Display(Name = "Cho phép nấu ăn trong nhà?")]
        public Boolean IsAllowCooking { get; set; }
        public int IsAllowCookingScore { get; set; }

        [Display(Name = "Có chỗ để xe máy, xe đạp?")]
        public Boolean HasMotorParking { get; set; }
        public int HasMotorParkingScore { get; set; }

        [Display(Name = "Có phòng vệ sinh khép kín?")]
        public Boolean HasToilet { get; set; }
        public int HasToiletScore { get; set; }

        [Display(Name = "Có điều hòa?")]
        public Boolean HasAirConditioner { get; set; }
        public int HasAirConditionerScore { get; set; }

        [Display(Name = "Có gara để ô tô?")]
        public Boolean HasGarage { get; set; }
        public int HasGarageScore { get; set; }

        [Display(Name = "Ở cùng với chủ?")]
        public Boolean IsStayWithOwner { get; set; }
        public int IsStayWithOwnerScore { get; set; }

        [Display(Name = "Có bảo vệ?")]
        public Boolean HasSecurity { get; set; }
        public int HasSecurityScore { get; set; }

        public string CenterMap { get; set; }
    }
}