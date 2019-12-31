using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class BodyStyleRepoQA : IBodyStyleRepository
    {
        public List<BodyStyle> GetAll()
        {
            return _bodyStyles;
        }

        public BodyStyle GetById(int styleId)
        {
            var bodyStyle = (from style in _bodyStyles
                           where style.BodyStyleId == styleId
                           select style).FirstOrDefault();

            return bodyStyle;
        }

        private static BodyStyle _convertible = new BodyStyle()
        {
            BodyStyleId = 1,
            BodyStyleName = "Convertible"
        };

        private static BodyStyle _hatchback = new BodyStyle()
        {
            BodyStyleId = 2,
            BodyStyleName = "Hatchback"
        };

        private static BodyStyle _minivan = new BodyStyle()
        {
            BodyStyleId = 3,
            BodyStyleName = "Minivan"
        };

        private static BodyStyle _sedan = new BodyStyle()
        {
            BodyStyleId = 4,
            BodyStyleName = "Sedan"
        };

        private static BodyStyle _SUV = new BodyStyle()
        {
            BodyStyleId = 5,
            BodyStyleName = "SUV"
        };

        private static List<BodyStyle> _bodyStyles = new List<BodyStyle>()
        {
            _convertible,
            _hatchback,
            _minivan,
            _sedan,
            _SUV
        };
    }
}
