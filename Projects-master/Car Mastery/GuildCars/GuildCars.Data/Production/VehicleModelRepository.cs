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
    public class VehicleModelRepository : IVehicleModelRepository
    {
        public VehicleModel GetById(int modelId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleModelId", modelId);

                return cn.Query<VehicleModel>("ModelsSelectById",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<VehicleModel> GetByMakeId(int makeId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleMakeId", makeId);

                return cn.Query<VehicleModel>("ModelsSelectByMakeId",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<ModelsItem> GetModelsItems()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<ModelsItem>("ModelsItemsSelectAll",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<VehicleModel> GetVehicleModels()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<VehicleModel>("ModelsSelectAll",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Insert(ModelsItem model)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@VehicleModelId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@VehicleModelName", model.VehicleModelName);
                parameters.Add("@VehicleMakeId", model.VehicleMakeId);
                parameters.Add("@UserId", model.UserId);

                cn.Execute("InsertVehicleModel",
                parameters,
                commandType: CommandType.StoredProcedure);

                model.VehicleModelId = parameters.Get<int>("@VehicleModelId");
            }
        }

    }
}
