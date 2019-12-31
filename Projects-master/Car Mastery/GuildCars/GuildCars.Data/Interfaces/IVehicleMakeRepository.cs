using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleMakeRepository
    {
        IEnumerable<VehicleMake> GetVehicleMakes();
        IEnumerable<MakesItem> GetMakesItems();
        void Insert(MakesItem make);
        VehicleMake GetById(int makeId);
    }
}
