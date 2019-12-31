using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class SearchBarDropDownsVM
    {
        public IEnumerable<decimal> PriceList
        {
            get
            {
                List<decimal> priceList = new List<decimal>();
                decimal price = 5000m;

                for (decimal i = price; i < 125000; i += 5000)
                {
                    priceList.Add(i);
                }

                return priceList;
            }
        }
        public IEnumerable<int> YearList
        {
            get
            {
            
                List<int> yearList = new List<int>();
                DateTime yearPlusOne = DateTime.Now.AddYears(1);
                int year = yearPlusOne.Year;
                
                for (int i = year; i >= 2000; i--)
                {
                    yearList.Add(i);
                }

                return yearList;
            }
        }



    }
}