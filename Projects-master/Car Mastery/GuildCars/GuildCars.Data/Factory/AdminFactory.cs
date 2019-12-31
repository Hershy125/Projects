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
    public static class AdminFactory
    {
        public static IAdminRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new AdminRepository();
                case "QA":
                    return new AdminRepoQA();
                default:
                    throw new Exception("Could not find valid RepositoryType config value");
            }
        }
    }
}
