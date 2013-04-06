using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace RentalHouseFinding.Models
{
    public class ManagePaymentModel
    {
        public DateTime? CreatedDateFrom {get; set;}
        public DateTime? CreatedDateTo { get; set; }

        public WebGrid Grid { get; set; }
    }
}