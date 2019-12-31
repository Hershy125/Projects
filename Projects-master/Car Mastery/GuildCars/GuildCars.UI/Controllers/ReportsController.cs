using GuildCars.Data.Factory;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{

    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            var repo = AdminFactory.GetRepository();

            InventoryReportVM model = new InventoryReportVM()
            {
                NewInventoryReport = repo.GetInventoryReport(1),
                UsedInventoryReport = repo.GetInventoryReport(2)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Sales()
        {
            var repo = AdminFactory.GetRepository();

            SalesReportDropDownVM model = new SalesReportDropDownVM();

            var users = repo.GetUsers();

            foreach (var user in users)
            {
                var userName = user.FirstName + " " + user.LastName;

                user.FullName = userName;
            }

            model.UserNames = new SelectList(users, "UserId", "FullName");

            return View(model);
        }
    }
}