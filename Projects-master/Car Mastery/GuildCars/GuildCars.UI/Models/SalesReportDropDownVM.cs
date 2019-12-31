using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class SalesReportDropDownVM
    {
        public IEnumerable<SelectListItem> UserNames { get; set; }
    }
}