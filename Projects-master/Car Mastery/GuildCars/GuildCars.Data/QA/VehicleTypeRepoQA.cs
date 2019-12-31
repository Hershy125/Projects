using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class VehicleTypeRepoQA : IVehicleTypeRepository
    {
        public List<VehicleType> GetAll()
        {
            return _vehicleTypes;
        }

        public VehicleType GetById(int typeId)
        {
            switch(typeId)
            {
                case 1:
                    return _new;
                default:
                    return _used;
            }
        }

        private static VehicleType _new = new VehicleType()
        {
            VehicleTypeId = 1,
            VehicleTypeName = "New"
        };

        private static VehicleType _used = new VehicleType()
        {
            VehicleTypeId = 2,
            VehicleTypeName = "Used"
        };

        private static List<VehicleType> _vehicleTypes = new List<VehicleType>()
        {
            _new,
            _used
        };
        
    }
}
