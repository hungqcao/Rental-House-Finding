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
        public float? AreaMax { get; set; }
        [Display(Name = "Diện tích nhỏ nhất")]
        [Remote("AreaMinLessThanAreaMax", "Validation", AdditionalFields = "AreaMax")]
        public float? AreaMin { get; set; }
        [Display(Name = "Giá cao nhất")]
        [Remote("PriceMaxGreaterThanPriceMin", "Validation", AdditionalFields = "PriceMin")]
        public float? PriceMax { get; set; }
        [Display(Name = "Giá thấp nhất")]        
        [Remote("PriceMinLessThanPriceMax", "Validation", AdditionalFields = "PriceMax")]
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

    //public sealed class ComapreGreaterThanAttribute : ValidationAttribute
    //{
    //    private const string _defaultErrorMessage = "'{0}' must be greater than '{1}'";
    //    private string _basePropertyName;
    //    private Type _type;

    //    public ComapreGreaterThanAttribute(string basePropertyName, Type type)
    //        : base(_defaultErrorMessage)
    //    {
    //        _basePropertyName = basePropertyName;
    //        _type = type;
    //    }

    //    //Override default FormatErrorMessage Method  
    //    public override string FormatErrorMessage(string name)
    //    {
    //        return string.Format(_defaultErrorMessage, name, _basePropertyName);
    //    }

    //    //Override IsValid  
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        //Get PropertyInfo Object
    //        var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

    //        //Get Value of the property    
    //        if (_type == typeof(int))
    //        {
    //            var otherField = (int)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
    //            var thisField = (int)value;
    //            //Actual comparision  
    //            if (thisField <= otherField)
    //            {
    //                var message = FormatErrorMessage(validationContext.DisplayName);
    //                return new ValidationResult(message);
    //            }
    //        }
    //        if (_type == typeof(float))
    //        {
    //            var otherField = (float)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
    //            var thisField = (float)value;
    //            //Actual comparision  
    //            if (thisField <= otherField)
    //            {
    //                var message = FormatErrorMessage(validationContext.DisplayName);
    //                return new ValidationResult(message);
    //            }
    //        }
    //        if (_type == typeof(DateTime))
    //        {
    //            var otherField = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
    //            var thisField = (DateTime)value;
    //            //Actual comparision  
    //            if (thisField <= otherField)
    //            {
    //                var message = FormatErrorMessage(validationContext.DisplayName);
    //                return new ValidationResult(message);
    //            }
    //        }


    //        //Default return - This means there were no validation error  
    //        return null;
    //    }

    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
    //        var rule = new ModelClientValidationRule
    //        {
    //            ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
    //            ValidationType = "requiredif",
    //        };
    //        rule.ValidationParameters.Add("other", _basePropertyName);
    //        yield return rule;
    //    }

    //}
}