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
    public class ContactRequestRepository : IContactRequestRepository
    {
        public List<ContactRequest> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<ContactRequest>("ContactRequestsSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Insert(ContactRequest request)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@ContactRequestId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Name", request.Name);
                parameters.Add("@Email", request.Email);
                parameters.Add("@Phone", request.Phone);
                parameters.Add("@Message", request.Message);

                cn.Execute("InsertContactRequest",
                parameters,
                commandType: CommandType.StoredProcedure);

                request.ContactRequestId = parameters.Get<int>("@ContactRequestId");
            }
        }
    }
}
