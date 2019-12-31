using Dapper;
using GuildCars.Data.Interfaces;
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
    public class SpecialRepository : ISpecialRepository
    {
        public void Delete(int specialId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@SpecialId", specialId);

                cn.Execute("DeleteSpecial",
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Special> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<Special>("SpecialsSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Insert(Special special)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@SpecialId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@SpecialTitle", special.SpecialTitle);
                parameters.Add("@SpecialDetails", special.SpecialDetails);

                cn.Execute("InsertSpecial",
                parameters,
                commandType: CommandType.StoredProcedure);

                special.SpecialId = parameters.Get<int>("@SpecialId");
            }
        }
    }
}
