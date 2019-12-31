using GuildCars.Data.Interfaces;
using GuildCars.Data.Production;
using GuildCars.Data.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factory
{
    public static class VehicleTypesFactory
    {
        public static IVehicleTypeRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new VehicleTypeRepository();
                case "QA":
                    return new VehicleTypeRepoQA();
                default:
                    throw new Exception("Could not find valid RepositoryType config value");
            }
        }
    }
}
