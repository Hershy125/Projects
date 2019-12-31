using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class SpecialRepoQA : ISpecialRepository
    {
        public List<Special> GetAll()
        {
            return _specials;
        }

        public void Insert(Special special)
        {
            Special newSpecial = new Special()
            {
                SpecialTitle = special.SpecialTitle,
                SpecialDetails = special.SpecialDetails
            }; ;

            var lastId = _specials.MaxBy(x => x.SpecialId).FirstOrDefault();
            int newId = lastId.SpecialId + 1;

            newSpecial.SpecialId = newId;

            _specials.Add(newSpecial);
        }

        public void Delete(int specialId)
        {
            _specials.RemoveAll(x => x.SpecialId == specialId);
        }

        private static Special _testSpecial = new Special()
        {
            SpecialId = 1,
            SpecialTitle = "Best Special Ever",
            SpecialDetails = "Buy one get one free cars! While Supplies last."
        };

        private static List<Special> _specials = new List<Special>()
        {
            _testSpecial
        };
    }
}
