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
    public class ColorRepository : IColorRepository
    {
        public List<Color> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<Color>("ColorsSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Color GetById(int colorId)
        {
            throw new NotImplementedException();
        }
    }
}
