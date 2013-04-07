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
using System.Web;
using RentalHouseFinding.Common;

namespace RentalHouseFinding.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        [MaxLength(100, ErrorMessage = "Không được vượt quá 100 ký tự, xin vui lòng nhập lại.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        [Display(Name = "Thể loại")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tỉnh thành phố")]
        [Display(Name = "Tỉnh, thành phố")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn quận, huyện")]
        [Display(Name = "Quận, huyện")]
        public int DistrictId { get; set; }

        [Display(Name = "Địa chỉ cụ thể")]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ cụ thể")]
        public string NumberHouse { get; set; }

        [Display(Name = "Đường phố")]
        [Required(ErrorMessage = "Xin vui lòng nhập tên đường phố.")]
        public string Street { get; set; }

        [Min(0, ErrorMessage = "Giá tiền phải lớn hơn hoặc bằng 0")]
        [Required(ErrorMessage = "Xin vui lòng nhập giá tiền.")]
        [Display(Name = "Giá tiền")]
        public double Price { get; set; }

        [Min(0, ErrorMessage = "Diện tích phải lớn hơn 0")]
        [Required(ErrorMessage = "Xin vui lòng nhập diện tích.")]
        [Display(Name = "Diện tích")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại.")]
        [Display(Name = "Số điện thoại để kích hoạt bài đăng")]
        [MaxLength(15, ErrorMessage = "Không được vượt quá 15 ký tự, xin vui lòng nhập lại.")]
        [RegularExpression("(([0+])([0-9]+))", ErrorMessage = "Sai định dạng,xin vui lòng nhập lại")]
        public string PhoneActive { get; set; }

        [Display(Name = "Mô tả cụ thể")]
        public string Description { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }

        [Display(Name = "Có Internet?")]
        public Boolean HasInternet { get; set; }

        [Display(Name = "Giá điện")]
        [Min(0, ErrorMessage = "Giá tiền phải lớn hơn hoặc bằng 0")]
        public double ElectricityFee { get; set; }

        [Display(Name = "Giá tiền nước")]
        [Min(0, ErrorMessage = "Giá tiền phải lớn hơn hoặc bằng 0")]
        public double WaterFee { get; set; }

        [Display(Name = "Có truyền hình cáp?")]
        public Boolean HasTVCable { get; set; }

        [Display(Name = "Có giường đi kèm?")]
        public Boolean HasBed { get; set; }

        [Display(Name = "Có bình nóng lạnh?")]
        public Boolean HasWaterHeater { get; set; }

        [Display(Name = "Cho phép nấu ăn trong nhà?")]
        public Boolean IsAllowCooking { get; set; }

        [Display(Name = "Có chỗ để xe máy, xe đạp?")]
        public Boolean HasMotorParking { get; set; }

        [Display(Name = "Có phòng vệ sinh khép kín?")]
        public Boolean HasToilet { get; set; }

        [Display(Name = "Có điều hòa?")]
        public Boolean HasAirConditioner { get; set; }

        [Display(Name = "Có gara để ô tô?")]
        public Boolean HasGarage { get; set; }

        [Display(Name = "Ở cùng với chủ?")]
        public Boolean IsStayWithOwner { get; set; }

        [Display(Name = "Giờ đóng cửa")]
        [MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự, xin vui lòng nhập lại.")]
        public string RestrictHours { get; set; }

        [Display(Name = "Có bảo vệ?")]
        public Boolean HasSecurity { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại liên lạc.")]
        [Display(Name = "Số điện thoại")]
        [MaxLength(15, ErrorMessage = "Không được vượt quá 15 ký tự, xin vui lòng nhập lại.")]
        [RegularExpression("(([0+])([0-9]+))", ErrorMessage = "Sai định dạng,xin vui lòng nhập lại")]
        public string PhoneContact { get; set; }

        [Display(Name = "Email liên lạc")]
        //[MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự, xin vui lòng nhập lại.")]
        //[Required(ErrorMessage = "Xin vui lòng nhập email.")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Display(Name = "Tên liên hệ")]
        public string NameContact { get; set; }

        [Display(Name = "Tài khoản Yahoo")]
        public string Yahoo { get; set; }

        [Display(Name = "Tài khoản skype")]
        public string Skype { get; set; }        
        public int Views { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Category { get; set; }
        public string Internet { get; set; }
        public string AirConditioner { get; set; }
        public string Bed { get; set; }
        public string Gara { get; set; }
        public string MotorParkingLot { get; set; }
        public string Security { get; set; }
        public string Toilet  { get; set; }
        public string TVCable { get; set; }        
        public string AllowCooking { get; set; }
        public string StayWithOwner { get; set; }
        public string WaterHeater { get; set; }
        public string Address { get; set; }
        public string Repost { get; set; }
        public int? UserId { get; set; }

        public string NearByPlace { get; set; }
        public Dictionary<int, string> lstNearByPlace { set; get; }
        public string ImagesPath { get; set; }
    } 
   
}