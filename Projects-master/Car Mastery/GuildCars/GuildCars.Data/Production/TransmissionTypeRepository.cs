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
    public class TransmissionTypeRepository : ITransmissionTypeRepository
    {
        public List<TransmissionType> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<TransmissionType>("TransmissionTypesSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public TransmissionType GetById(int typeId)
        {
            throw new NotImplementedException();
        }
    }
}
