using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Production
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        public VehicleMake GetById(int makeId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleMakeId", makeId);

                return cn.Query<VehicleMake>("MakesSelectById",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<MakesItem> GetMakesItems()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<MakesItem>("MakesItemsSelectAll",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<VehicleMake> GetVehicleMakes()
        {

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<VehicleMake>("MakesSelectAll",
                    commandType: CommandType.StoredProcedure);
            }

        }

        public void Insert(MakesItem make)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleMakeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@VehicleMakeName", make.VehicleMakeName);
                parameters.Add("@UserId", make.UserId);

                cn.Execute("InsertVehicleMake",
                parameters,
                commandType: CommandType.StoredProcedure);

                make.VehicleMakeId = parameters.Get<int>("@VehicleMakeId");
            }
        }
    }
}
