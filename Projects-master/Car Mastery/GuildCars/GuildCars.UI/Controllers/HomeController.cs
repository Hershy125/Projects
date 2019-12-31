using GuildCars.Data.Factory;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            FeaturedandSpecialsVM model = new FeaturedandSpecialsVM();

            var specials = SpecialsFactory.GetRepository().GetAll();

            var featuredVehicles = VehicleListingsFactory.GetRepository().GetFeatured();

            model.FeaturedVehicles = featuredVehicles;
            model.Specials = specials;

            return View(model);
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var model = SpecialsFactory.GetRepository().GetAll();

            return View(model);
        }


        [HttpGet]
        public ActionResult Contact(int? id)
        {

            ContactRequestVM model = new ContactRequestVM()
            {
                NewRequest = new ContactRequest()
            };

            if (id != null)
            {
                var vehicle = VehicleListingsFactory.GetRepository().GetById((int)id);

                model.NewRequest.Message = vehicle.VIN;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactRequestVM model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var repo = ContactRequestsFactory.GetRepository();

                    repo.Insert(model.NewRequest);

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
            else
            {
                return View("Contact", model);
            }

        }

    }
}