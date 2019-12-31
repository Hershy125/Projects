using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class TransmissionTypeRepoQA : ITransmissionTypeRepository
    {
        public List<TransmissionType> GetAll()
        {
            return _transmissionTypes;
        }

        public TransmissionType GetById(int typeId)
        {
            switch(typeId)
            {
                case 1:
                    return _automatic;
                default:
                    return _manual;

            }
        }

        private static TransmissionType _automatic = new TransmissionType()
        {
            TransmissionTypeId = 1,
            TransmissionTypeName = "Automatic"
        };

        private static TransmissionType _manual = new TransmissionType()
        {
            TransmissionTypeId = 2,
            TransmissionTypeName = "Manual"
        };

        private static List<TransmissionType> _transmissionTypes = new List<TransmissionType>()
        {
            _automatic,
            _manual
        };
    }
}
