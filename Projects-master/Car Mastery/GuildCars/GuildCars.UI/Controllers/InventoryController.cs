using GuildCars.Data.Factory;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public ActionResult Details(int id)
        {
            var repo = VehicleListingsFactory.GetRepository();
            var model = repo.GetDetails(id);

            return View(model);
        }

        [HttpGet]
        public ActionResult New()
        {
            SearchBarDropDownsVM model = new SearchBarDropDownsVM();
            return View(model);
        }

        [HttpGet]
        public ActionResult Used()
        {
            SearchBarDropDownsVM model = new SearchBarDropDownsVM();
            return View(model);
        }
    }
}