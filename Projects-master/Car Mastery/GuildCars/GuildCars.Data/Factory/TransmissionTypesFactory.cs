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
    public static class TransmissionTypesFactory
    {
        public static ITransmissionTypeRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new TransmissionTypeRepository();
                case "QA":
                    return new TransmissionTypeRepoQA();
                default:
                    throw new Exception("Could not find valid RepositoryType config value");
            }
        }
    }
}
