﻿using GuildCars.Data.Factory;
using GuildCars.Data.Production;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {
        [Route("api/inventory/new/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult NewSearch(string searchTerm, decimal? minMSRP, decimal? maxMSRP, int? minYear, int? maxYear, int? vehicleTypeId)
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/inventory/used/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult UsedSearch(string searchTerm, decimal? minMSRP, decimal? maxMSRP, int? minYear, int? maxYear, int? vehicleTypeId)
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

    }

}
