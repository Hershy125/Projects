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
    public static class BodyStylesFactory
    {
        public static IBodyStyleRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new BodyStyleRepository();
                case "QA":
                    return new BodyStyleRepoQA();
                default:
                    throw new Exception("Could not find valid RepositoryType config value");
            }
        }
    }
}
