using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class CustomerRepoQA : ICustomerRepository
    {
        public List<Customer> GetAll()
        {
            return _customers;
        }

        public void Insert(Customer customer)
        {
 
            Customer newCustomer = new Customer()
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Street1 = customer.Street1,
                Street2 = customer.Street2,
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode
            };

            var lastId = _customers.MaxBy(x => x.CustomerId).FirstOrDefault();
            int newId = lastId.CustomerId + 1;

            newCustomer.CustomerId = newId;

            _customers.Add(newCustomer);
            
            
        }


        public Customer GetByEmail(string email)
        {
            var customer = (from person in _customers
                             where person.Email == email
                             select person).FirstOrDefault();

            return customer;

        }

        private static Customer _testCustomer = new Customer()
        {
            CustomerId = 1,
            Name = "Tester One",
            Phone = "122-222-3344",
            Email = "testing@tests.com",
            Street1 = "123 state street",
            Street2 = "Apt 101",
            City = "Newark",
            State = "NJ",
            ZipCode = "07992"
        };

        private static List<Customer> _customers = new List<Customer>()
        {
            _testCustomer
        };
    }
}
