using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalHouseFinding.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    [KnownType(typeof(UserViewModel))]
    public class UserViewModel
    {
        [DisplayName(@"UserId")]
        public int UserId
        {
            get;
            set;
        }

    }
}
