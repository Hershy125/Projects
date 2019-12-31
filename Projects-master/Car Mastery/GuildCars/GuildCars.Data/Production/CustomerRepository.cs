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
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAll()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                return cn.Query<Customer>("CustomersSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Customer GetByEmail(string email)
        {
            Customer result = null;

            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);

                result = cn.Query<Customer>("CustomersSelectByEmail",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

            }

            return result;
        }

        public void Insert(Customer customer)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = Settings.GetConnectionString();

                var parameters = new DynamicParameters();
                parameters.Add("@CustomerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Name", customer.Name);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@Phone", customer.Phone);
                parameters.Add("@Street1", customer.Street1);
                parameters.Add("@Street2", customer.Street2);
                parameters.Add("@City", customer.City);
                parameters.Add("@State", customer.State);
                parameters.Add("@ZipCode", customer.ZipCode);

                cn.Execute("InsertCustomer",
                parameters,
                commandType: CommandType.StoredProcedure);

                customer.CustomerId = parameters.Get<int>("@CustomerId");
            }
        }
    }
}
