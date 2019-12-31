using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class VehicleMakeRepoQA : IVehicleMakeRepository
    {

        public IEnumerable<MakesItem> GetMakesItems()
        {
            return _makesItems;
        }

        public void Insert(MakesItem make)
        {
            var lastId = _vehicleMakes.MaxBy(x => x.VehicleMakeId).FirstOrDefault();
            int newId = lastId.VehicleMakeId + 1;
            make.VehicleMakeId = newId;

            _makesItems.Add(make);

            VehicleMake newMake = new VehicleMake()
            {
                VehicleMakeId = make.VehicleMakeId,
                VehicleMakeName = make.VehicleMakeName,
                UserId = make.UserId,
                CreatedDate = make.CreatedDate
            };

            _vehicleMakes.Add(newMake);


        }

        public VehicleMake GetById(int makeId)
        {
            var vehicleMake = (from make in _vehicleMakes
                                where make.VehicleMakeId == makeId
                                select make).FirstOrDefault();

            return vehicleMake;
        }

        public IEnumerable<VehicleMake> GetVehicleMakes()
        {
            return _vehicleMakes;
        }

        private static VehicleMake _ford = new VehicleMake()
        {
            VehicleMakeId = 1,
            VehicleMakeName = "Ford",
            UserId = "11111111-1111-1111-1111-111111111111"

        };

        private static VehicleMake _kia = new VehicleMake()
        {
            VehicleMakeId = 2,
            VehicleMakeName = "Kia",
            UserId = "11111111-1111-1111-1111-111111111111"

        };

        private static VehicleMake _bmw = new VehicleMake()
        {
            VehicleMakeId = 3,
            VehicleMakeName = "BMW",
            UserId = "11111111-1111-1111-1111-111111111111"

        };

        private static VehicleMake _toyota = new VehicleMake()
        {
            VehicleMakeId = 4,
            VehicleMakeName = "Toyota",
            UserId = "11111111-1111-1111-1111-111111111111"

        };

        private static List<VehicleMake> _vehicleMakes = new List<VehicleMake>()
        {
            _ford,
            _kia,
            _bmw,
            _toyota
        };

        private static MakesItem _testMake1 = new MakesItem()
        {
            VehicleMakeId = 1,
            VehicleMakeName = "Ford",
            CreatedDate = DateTime.Now,
            UserId = "11111111-1111-1111-1111-111111111111",
            Email = "test@test.com"
        };

        private static MakesItem _testMake2 = new MakesItem()
        {
            VehicleMakeId = 2,
            VehicleMakeName = "Kia",
            CreatedDate = DateTime.Now,
            UserId = "11111111-1111-1111-1111-111111111111",
            Email = "test@test.com"
        };

        private static MakesItem _testMake3 = new MakesItem()
        {
            VehicleMakeId = 3,
            VehicleMakeName = "BMW",
            CreatedDate = DateTime.Now,
            UserId = "11111111-1111-1111-1111-111111111111",
            Email = "test@test.com"
        };

        private static MakesItem _testMake4 = new MakesItem()
        {
            VehicleMakeId = 4,
            VehicleMakeName = "Toyota",
            CreatedDate = DateTime.Now,
            UserId = "11111111-1111-1111-1111-111111111111",
            Email = "test@test.com"
        };

        private static List<MakesItem> _makesItems = new List<MakesItem>()
        {
            _testMake1,
            _testMake2,
            _testMake3,
            _testMake4
        };

    }
}
