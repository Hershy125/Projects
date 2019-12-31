using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class ColorRepoQA : IColorRepository
    {
        public List<Color> GetAll()
        {
            return _colors;
        }

        public Color GetById(int colorId)
        {
            var colorItem = (from color in _colors
                             where color.ColorId == colorId
                             select color).FirstOrDefault();

            return colorItem;
        }

        private static Color _black = new Color()
        {
            ColorId = 1,
            ColorName = "Black"
        };

        private static Color _white = new Color()
        {
            ColorId = 2,
            ColorName = "White"
        };

        private static Color _blue = new Color()
        {
            ColorId = 3,
            ColorName = "Blue"
        };

        private static Color _beige = new Color()
        {
            ColorId = 4,
            ColorName = "Beige"
        };

        private static List<Color> _colors = new List<Color>()
        {
            _black,
            _white,
            _blue,
            _beige
        };


    }
}
