using GuildCars.Data.Factory;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "sales")]
    public class SalesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            SearchBarDropDownsVM model = new SearchBarDropDownsVM();
           
            return View(model);
        }

        [HttpGet]
        public ActionResult Purchase(int id)
        {
            var listingRepo = VehicleListingsFactory.GetRepository();
            var purchaseTypeRepo = PurchaseTypesFactory.GetRepository();

            PurchaseVM model = new PurchaseVM()
            {
                VehicleDetails = listingRepo.GetDetails(id),
                NewPurchase = new Purchase(),
                Buyer = new Customer(),
                PurchaseTypes = purchaseTypeRepo.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseVM model)
        {
            var listingRepo = VehicleListingsFactory.GetRepository();
            var purchaseRepo = PurchasesFactory.GetRepository();
            var customerRepo = CustomersFactory.GetRepository();

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = customerRepo.GetByEmail(model.Buyer.Email);

                    if (customer == null)
                    {
                        customerRepo.Insert(model.Buyer);
                        var customerId = customerRepo.GetByEmail(model.Buyer.Email).CustomerId;

                        model.NewPurchase.CustomerId = customerId;
                    }
                    else
                    {
                        model.NewPurchase.CustomerId = customer.CustomerId;
                    }


                    var vehicle = listingRepo.GetById(model.VehicleDetails.VehicleListingId);
                    vehicle.Sold = true;

                    listingRepo.Update(vehicle);

                    model.NewPurchase.VehicleListingId = model.VehicleDetails.VehicleListingId;

                    var userId = User.Identity.GetUserId();


                    model.NewPurchase.UserId = userId;

                    purchaseRepo.Insert(model.NewPurchase);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                var purchaseTypeRepo = PurchaseTypesFactory.GetRepository();

                model.PurchaseTypes = purchaseTypeRepo.GetAll();
                model.VehicleDetails = listingRepo.GetDetails(model.VehicleDetails.VehicleListingId);

                return View("Purchase", model);
            }
        }
    }
}