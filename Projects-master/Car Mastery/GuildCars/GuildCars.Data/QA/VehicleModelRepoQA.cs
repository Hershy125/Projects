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
    public class VehicleModelRepoQA : IVehicleModelRepository
    {

        public IEnumerable<VehicleModel> GetVehicleModels()
        {
            return _vehicleModels;
        }

        public void Insert(ModelsItem model)
        {
            VehicleModel newModel = new VehicleModel()
            {
                VehicleMakeId = model.VehicleMakeId,
                VehicleModelName = model.VehicleModelName,
                UserId = model.UserId,
                CreatedDate = DateTime.Now
            };

            var lastId = _vehicleModels.MaxBy(x => x.VehicleModelId).FirstOrDefault();
            int newId = lastId.VehicleModelId + 1;
            var makeRepo = new VehicleMakeRepoQA();
            model.VehicleMakeName = makeRepo.GetById(model.VehicleMakeId).VehicleMakeName;

            newModel.VehicleModelId = newId;
            model.VehicleModelId = newId;
            model.CreatedDate = DateTime.Now;

            _vehicleModels.Add(newModel);
            _modelsItems.Add(model);
        }

        public VehicleModel GetById(int modelId)
        {
            var vehicleModel = (from model in _vehicleModels
                               where model.VehicleModelId == modelId
                               select model).FirstOrDefault();

            return vehicleModel;
        }

        public IEnumerable<VehicleModel> GetByMakeId(int makeId)
        {
            var vehicleModels = (from model in _vehicleModels
                                where model.VehicleMakeId == makeId
                                select model);

            return vehicleModels;
        }

        public IEnumerable<ModelsItem> GetModelsItems()
        {
            return _modelsItems;
        }


        private static VehicleModel _explorer = new VehicleModel()
        {
            VehicleModelId = 1,
            VehicleMakeId = 1,
            VehicleModelName = "Explorer",
            UserId = "123"

        };

        private static VehicleModel _soul = new VehicleModel()
        {
            VehicleModelId = 2,
            VehicleMakeId = 2,
            VehicleModelName = "Soul",
            UserId = "123"

        };

        private static List<VehicleModel> _vehicleModels = new List<VehicleModel>()
        {
            _explorer,
            _soul
        };

        private static ModelsItem _testModelsItem = new ModelsItem()
        {
            VehicleMakeId = 1,
            VehicleModelId = 1,
            VehicleMakeName = "Ford",
            VehicleModelName = "Explorer",
            CreatedDate = DateTime.Now,
            UserId = "123",
            Email = "test@test.com"
        };

        private static List<ModelsItem> _modelsItems = new List<ModelsItem>()
        {
            _testModelsItem
        };
    }
}
