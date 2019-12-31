using GuildCars.Data.Factory;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("api/inventory/admin/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AdminSearch(string searchTerm, decimal? minMSRP, decimal? maxMSRP, int? minYear, int? maxYear, int? vehicleTypeId)
        {
            var repo = VehicleListingsFactory.GetRepository();

            try
            {
                var parameters = new ListingSearchParameters()
                {
                    SearchTerm = searchTerm,
                    MinMSRP = minMSRP,
                    MaxMSRP = maxMSRP,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    VehicleTypeId = vehicleTypeId

                };

                var result = repo.GetSearchResults(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/specials/delete/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int id)
        {
            try
            {
                var repo = SpecialsFactory.GetRepository();

                repo.Delete(id);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("api/admin/vehicles/delete/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteVehicle(int id)
        {
            try
            {
                var repo = VehicleListingsFactory.GetRepository();

                var vehicle = repo.GetById(id);

                var savepath = HttpContext.Current.Server.MapPath("~/Images");
                var filePath = Path.Combine(savepath, vehicle.ImageFileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }


                repo.Delete(id);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/models/getbymakeid/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModels(int id)
        {
            var repo = VehicleModelsFactory.GetRepository();

            try
            {
                var models = repo.GetByMakeId(id);

                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/admin/models/getbyid/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModel(int id)
        {
            var repo = VehicleModelsFactory.GetRepository();

            try
            {
                var model = repo.GetById(id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
       
    }
}
