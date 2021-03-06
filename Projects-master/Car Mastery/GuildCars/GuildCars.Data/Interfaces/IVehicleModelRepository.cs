﻿using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleModelRepository
    {
        IEnumerable<VehicleModel> GetVehicleModels();
        IEnumerable<ModelsItem> GetModelsItems();
        void Insert(ModelsItem model);
        VehicleModel GetById(int modelId);
        IEnumerable<VehicleModel> GetByMakeId(int makeId);

    }
}
