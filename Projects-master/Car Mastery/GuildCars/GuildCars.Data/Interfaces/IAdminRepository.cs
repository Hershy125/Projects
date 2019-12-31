using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IAdminRepository
    {

        IEnumerable<InventoryReportItem> GetInventoryReport(int vehicleTypeId);
        IEnumerable<SalesReportItem> GetSalesReport(SalesReportParameters parameters);
        IEnumerable<UserModel> GetUsers();

    }
}
