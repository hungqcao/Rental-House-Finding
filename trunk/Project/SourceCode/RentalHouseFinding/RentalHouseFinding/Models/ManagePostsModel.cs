using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace RentalHouseFinding.Models
{
    public class ManagePostsModel
    {
        
        public int? UserId { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }

        public int? CategoryId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? CreatedDateFrom { get; set; }

        public DateTime? CreatedDateTo { get; set; }

        public DateTime? EditedDateFrom { get; set; }

        public DateTime? EditedDateTo { get; set; }

        public DateTime? RenewedDateFrom { get; set; }

        public DateTime? RenewedDateTo { get; set; }

        public DateTime? ExpireDateFrom { get; set; }

        public DateTime? ExpireDateTo { get; set; }

        public WebGrid Grid { get; set; }
    }
}