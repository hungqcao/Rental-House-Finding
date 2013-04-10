﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalHouseFinding.Models
{
    public class InfoAndUserLogsViewModel
    {
        public Users User { get; set; }
        public IEnumerable<UserLogs> UserLogsList { get; set; }
        public bool IsOpenIdOrFBAcc { get; set; }
    }
}