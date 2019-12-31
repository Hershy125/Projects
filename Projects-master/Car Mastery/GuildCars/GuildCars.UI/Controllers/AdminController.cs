using GuildCars.Data.Factory;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Makes()
        {
            var repo = VehicleMakesFactory.GetRepository();


            var model = new AddMakeVM()
            {
                VehicleMakes = repo.GetMakesItems(),
                NewMake = new MakesItem()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Makes(AddMakeVM model)
        {
            var makesRepo = VehicleMakesFactory.GetRepository();

            if(ModelState.IsValid)
            {
                try
                {
                    var userId = User.Identity.GetUserId();
                    model.NewMake.UserId = userId;

                    makesRepo.Insert(model.NewMake);

                    return RedirectToAction("Makes");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            model.VehicleMakes = makesRepo.GetMakesItems();

            return View("Makes", model);
        }

        [HttpGet]
        public ActionResult Models()
        {
            var modelRepo = VehicleModelsFactory.GetRepository();
            var makeRepo = VehicleMakesFactory.GetRepository();

            var model = new AddModelVM()
            {
                VehicleModels = modelRepo.GetModelsItems(),
                VehicleMakes = makeRepo.GetMakesItems(),
                NewModel = new ModelsItem()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Models(AddModelVM model)
        {
            var modelRepo = VehicleModelsFactory.GetRepository();

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.Identity.GetUserId();
                    var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var email = userMgr.FindById(userId).Email;
                    model.NewModel.UserId = userId;
                    model.NewModel.Email = email;
                    modelRepo.Insert(model.NewModel);

                    return RedirectToAction("Models");

                }
                catch(Exception ex)
                {
                    throw ex;
                }

                
            }
            var makeRepo = VehicleMakesFactory.GetRepository();
            model.VehicleMakes = makeRepo.GetMakesItems();
            model.VehicleModels = modelRepo.GetModelsItems();

            return View("Models", model);
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var repo = SpecialsFactory.GetRepository();

            var model = new AddSpecialVM()
            {
                Specials = repo.GetAll(),
                NewSpecial = new Special()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Specials(AddSpecialVM model)
        {
            var repo = SpecialsFactory.GetRepository();

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Insert(model.NewSpecial);

                    return RedirectToAction("Specials");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
            else
            {
                model.Specials = repo.GetAll();

                return View("Specials", model);
            }

        }

        [HttpGet]
        public ActionResult AddVehicle()
        {
            var makesRepo = VehicleMakesFactory.GetRepository();
            var typesRepo = VehicleTypesFactory.GetRepository();
            var bodyRepo = BodyStylesFactory.GetRepository();
            var transRepo = TransmissionTypesFactory.GetRepository();
            var colorRepo = ColorsFactory.GetRepository();

            AddEditListingVM model = new AddEditListingVM()
            {
                Listing = new VehicleListing(),
                VehicleMakes = makesRepo.GetVehicleMakes(),
                VehicleTypes = typesRepo.GetAll(),
                BodyStyles = bodyRepo.GetAll(),
                TransmissionTypes = transRepo.GetAll(),
                Colors = colorRepo.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicle(AddEditListingVM model)
        {
            var repo = VehicleListingsFactory.GetRepository();

            if (ModelState.IsValid)
            {
                try
                {
                    var savepath = Server.MapPath("~/Images");

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(model.ImageUpload.FileName);
                        var filePath = Path.Combine(savepath, fileName);

                        repo.Insert(model.Listing);

                        var extension = Path.GetExtension(filePath);
                        var vehicle = repo.GetById(model.Listing.VehicleListingId);
                        vehicle.ImageFileName = "inventory-" + vehicle.VehicleListingId + extension;

                        var newFilePath = Path.Combine(savepath, vehicle.ImageFileName);

                        if (new FileInfo(newFilePath).Exists)
                        {
                            new FileInfo(newFilePath).Delete();
                        }

                        model.ImageUpload.SaveAs(newFilePath);

                        repo.Update(vehicle);
                    }

                    return RedirectToAction("EditVehicle", new { id = model.Listing.VehicleListingId });
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
            else
            {
                var makesRepo = VehicleMakesFactory.GetRepository();
                var typesRepo = VehicleTypesFactory.GetRepository();
                var bodyRepo = BodyStylesFactory.GetRepository();
                var transRepo = TransmissionTypesFactory.GetRepository();
                var colorRepo = ColorsFactory.GetRepository();

                model.VehicleMakes = makesRepo.GetVehicleMakes();
                model.VehicleTypes = typesRepo.GetAll();
                model.BodyStyles = bodyRepo.GetAll();
                model.TransmissionTypes = transRepo.GetAll();
                model.Colors = colorRepo.GetAll();

                return View("AddVehicle", model);
            }
        }

        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            var listingRepo = VehicleListingsFactory.GetRepository();
            var makesRepo = VehicleMakesFactory.GetRepository();
            var modelsRepo = VehicleModelsFactory.GetRepository();
            var typesRepo = VehicleTypesFactory.GetRepository();
            var bodyRepo = BodyStylesFactory.GetRepository();
            var transRepo = TransmissionTypesFactory.GetRepository();
            var colorRepo = ColorsFactory.GetRepository();

            AddEditListingVM model = new AddEditListingVM()
            {
                Listing = listingRepo.GetById(id),
                VehicleMakes = makesRepo.GetVehicleMakes(),
                VehicleModels = modelsRepo.GetVehicleModels(),
                VehicleTypes = typesRepo.GetAll(),
                BodyStyles = bodyRepo.GetAll(),
                TransmissionTypes = transRepo.GetAll(),
                Colors = colorRepo.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(AddEditListingVM model)
        {
            var repo = VehicleListingsFactory.GetRepository();

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {

                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileName(model.ImageUpload.FileName);
                        string fileExtension = Path.GetExtension(model.ImageUpload.FileName);

                        fileName = "inventory-" + model.Listing.VehicleListingId + fileExtension;

                        var filePath = Path.Combine(savepath, fileName);

                        model.ImageUpload.SaveAs(filePath);

                        model.Listing.ImageFileName = fileName;
                    }

                    repo.Update(model.Listing);

                    return RedirectToAction("Vehicles");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
            else
            {

                var makesRepo = VehicleMakesFactory.GetRepository();
                var modelsRepo = VehicleModelsFactory.GetRepository();
                var typesRepo = VehicleTypesFactory.GetRepository();
                var bodyRepo = BodyStylesFactory.GetRepository();
                var transRepo = TransmissionTypesFactory.GetRepository();
                var colorRepo = ColorsFactory.GetRepository();

                model.VehicleMakes = makesRepo.GetVehicleMakes();
                model.VehicleModels = modelsRepo.GetVehicleModels();
                model.VehicleTypes = typesRepo.GetAll();
                model.BodyStyles = bodyRepo.GetAll();
                model.TransmissionTypes = transRepo.GetAll();
                model.Colors = colorRepo.GetAll();


                return View("EditVehicle", model);
            }
        }

        [HttpGet]
        public ActionResult Vehicles()
        {
            SearchBarDropDownsVM model = new SearchBarDropDownsVM();
            return View(model);
        }

        [HttpGet]
        public ActionResult Users()
        {
            var repo = AdminFactory.GetRepository();

            var model = repo.GetUsers();

            return View(model);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult AddUser()
        {
            UpdateUserVM model = new UpdateUserVM();

            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(UpdateUserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Role = model.Role };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    var addedUser = UserManager.FindByEmail(user.Email);

                    if (addedUser.Role == "Admin")
                    {
                        UserManager.AddToRole(addedUser.Id, "admin");
                    }
                    else if (addedUser.Role == "Sales")
                    {
                        UserManager.AddToRole(addedUser.Id, "sales");
                    }

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var user = UserManager.FindById(id);

            UpdateUserVM model = new UpdateUserVM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                UserId = id
            };

            return View(model);


        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UpdateUserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(model.UserId);
                if (model.Password != null)
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Role = model.Role;

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    return View(model);
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }
}