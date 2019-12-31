using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Production
{
    public class AdminRepository : IAdminRepository
    {
        public void EditUser(UserModel user)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", user.FirstName);
                parameters.Add("@LastName", user.LastName);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Role", user.Role);
                parameters.Add("@UserId", user.UserId);

                cn.Execute("UpdateUser",
                parameters,
                commandType: CommandType.StoredProcedure);

            }
        }

        public IEnumerable<InventoryReportItem> GetInventoryReport(int vehicleTypeId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleTypeId", vehicleTypeId);

                return cn.Query<InventoryReportItem>("InventoryReport",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SalesReportItem> GetSalesReport(SalesReportParameters parameters)
        {
            List<SalesReportItem> items = new List<SalesReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT FirstName, LastName, UserId, SUM(PurchasePrice) as 'TotalSales', COUNT(PurchasePrice) as 'TotalVehicles' " +
                    "FROM Purchases p " +
                    "INNER JOIN AspNetUsers a ON p.UserId = a.Id " +
                    "WHERE 1 = 1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.FromDate.HasValue)
                {
                    query += $"AND p.CreatedDate >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate.Value);
                }

                if (parameters.ToDate.HasValue)
                {
                    query += $"AND p.CreatedDate <= @ToDate ";
                    cmd.Parameters.AddWithValue("@Todate ", parameters.ToDate.Value);
                }

                if (!string.IsNullOrEmpty(parameters.UserId))
                {
                    query += $"AND a.Id = @UserId ";
                    cmd.Parameters.AddWithValue("@UserId", parameters.UserId);
                }

                query += "GROUP BY FirstName, LastName, p.UserId ";
                query += "ORDER BY TotalSales DESC";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReportItem row = new SalesReportItem();
                        row.FirstName = dr["FirstName"].ToString();
                        row.LastName = dr["LastName"].ToString();
                        row.TotalVehicles = (int)dr["TotalVehicles"];
                        row.UserId = dr["UserId"].ToString();
                        row.TotalSales = (decimal)dr["TotalSales"];

                        items.Add(row);
                    }
                }
            }

            return items;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<UserModel>("UsersSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
