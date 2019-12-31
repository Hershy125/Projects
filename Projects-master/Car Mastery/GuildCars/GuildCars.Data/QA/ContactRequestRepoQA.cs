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
    public class ContactRequestRepoQA : IContactRequestRepository
    {
        public List<ContactRequest> GetAll()
        {
            return _contactRequests;
        }

        public void Insert(ContactRequest request)
        {

            ContactRequest newRequest = new ContactRequest()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Message = request.Message
            };

            var lastId = _contactRequests.MaxBy(x => x.ContactRequestId).FirstOrDefault();
            int newId = lastId.ContactRequestId + 1;

            newRequest.ContactRequestId = newId;

            _contactRequests.Add(newRequest);
        }

        private static ContactRequest _testRequest = new ContactRequest()
        {
            ContactRequestId = 1,
            Email = "test@test.com",
            Name = "Testy McTesterson",
            Phone = "123-456-7890",
            Message = "Test contact request"
        };

        private static List<ContactRequest> _contactRequests = new List<ContactRequest>()
        {
            _testRequest
        };

    }
}
