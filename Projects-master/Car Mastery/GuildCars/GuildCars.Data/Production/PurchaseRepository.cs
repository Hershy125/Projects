using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Production
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public List<Purchase> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<Purchase>("PurchasesSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<Purchase> GetByUserId(string userId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);

                return cn.Query<Purchase>("PurchasesSelectByUser",
                    parameters,
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Insert(Purchase purchase)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@PurchaseId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@VehicleListingId", purchase.VehicleListingId);
                parameters.Add("@CustomerId", purchase.CustomerId);
                parameters.Add("@UserId", purchase.UserId);
                parameters.Add("@PurchasePrice", purchase.PurchasePrice);
                parameters.Add("@PurchaseTypeId", purchase.PurchaseTypeId);
      

                cn.Execute("InsertPurchase",
                parameters,
                commandType: CommandType.StoredProcedure);

                purchase.PurchaseId = parameters.Get<int>("@PurchaseId");
            }
        }
    }
}
