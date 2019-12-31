using GuildCars.Data.Factory;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class ReportsAPIController : ApiController
    {
        [Route("api/reports/sales")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SalesReport(string userId, DateTime? fromDate, DateTime? toDate)
        {
            var repo = AdminFactory.GetRepository();

            try
            {
                var parameters = new SalesReportParameters()
                {
                    UserId = userId,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                var result = repo.GetSalesReport(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
