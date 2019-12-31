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
    public class BodyStyleRepository : IBodyStyleRepository
    {
        public List<BodyStyle> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<BodyStyle>("BodyStylesSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public BodyStyle GetById(int styleId)
        {
            throw new NotImplementedException();
        }
    }
}
