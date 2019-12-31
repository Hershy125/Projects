using GuildCars.Data.Interfaces;
using GuildCars.Data.Production;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class AdminRepoQA : IAdminRepository
    {

        public IEnumerable<InventoryReportItem> GetInventoryReport(int vehicleTypeId)
        {
            var vehicleRepo = new VehicleListingRepoQA();
            var vehicles = vehicleRepo.GetAll();
            var makesRepo = new VehicleMakeRepoQA();
            var makes = makesRepo.GetVehicleMakes();
            var modelsRepo = new VehicleModelRepoQA();
            var models = modelsRepo.GetVehicleModels();

            var reportItems = from v in vehicles
                              join make in makes on v.VehicleMakeId equals make.VehicleMakeId
                              join model in models on v.VehicleModelId equals model.VehicleModelId
                              where v.VehicleTypeId == vehicleTypeId
                              group v by new
                              {
                                  make.VehicleMakeName,
                                  model.VehicleModelName,
                                  v.Year
                              } into grp
                              select new InventoryReportItem
                              {
                                  Year = grp.Key.Year,
                                  VehicleMakeName = grp.Key.VehicleMakeName,
                                  VehicleModelName = grp.Key.VehicleModelName,
                                  Count = grp.Count(),
                                  StockValue = grp.Sum(v => v.MSRP)
                              };

            return reportItems;
        }

        public IEnumerable<SalesReportItem> GetSalesReport(SalesReportParameters parameters)
        {
            var purchaseRepo = new PurchaseRepoQA();
            var purchases = purchaseRepo.GetAll();
            var users = GetUsers();

            if (parameters.FromDate.HasValue)
            {
                purchases = purchases.Where(p => p.CreatedDate >= parameters.FromDate).ToList();
            }
            if (parameters.ToDate.HasValue)
            {
                purchases = purchases.Where(p => p.CreatedDate <= parameters.ToDate).ToList();
            }
            if (!string.IsNullOrEmpty(parameters.UserId))
            {
                purchases = purchases.Where(p => p.UserId == parameters.UserId).ToList();
            }

            var query = from p in purchases
                        join user in users on p.UserId equals user.UserId
                        group p by new
                        {
                            p.UserId,
                            user.FirstName,
                            user.LastName
                        } into  grp
                        select new SalesReportItem
                        {
                            FirstName = grp.Key.FirstName,
                            LastName = grp.Key.LastName,
                            TotalVehicles = grp.Count(),
                            TotalSales = grp.Sum(p => p.PurchasePrice)
                        };

            return query;

        }

        public IEnumerable<UserModel> GetUsers()
        {
            var userRepo = new AdminRepository();

            return userRepo.GetUsers();
            
        }

    }
}
