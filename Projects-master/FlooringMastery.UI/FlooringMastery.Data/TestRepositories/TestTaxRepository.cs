using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.TestRepositories
{
    public class TestTaxRepository : ITaxRepository
    {
        public ListTaxResponse LoadTaxInfo(string filePath)
        {
            ListTaxResponse response = new ListTaxResponse();
            response.Success = true;
            response.TaxList = _taxInfo;

            return response;
        }

        private static readonly TaxInfo _ohio = new TaxInfo()
        {
            StateAbbreviation = "OH",
            StateName = "Ohio",
            TaxRate = 6.25M
        };
        private static readonly TaxInfo _penn = new TaxInfo()
        {
            StateAbbreviation = "PA",
            StateName = "Pennsylvania",
            TaxRate = 6.75M
        };
        private static readonly TaxInfo _michigan = new TaxInfo()
        {
            StateAbbreviation = "MI",
            StateName = "Michigan",
            TaxRate = 5.75M
        };
        private static readonly TaxInfo _indiana = new TaxInfo()
        {
            StateAbbreviation = "IN",
            StateName = "Indiana",
            TaxRate = 6.00M
        };

        private static List<TaxInfo> _taxInfo = new List<TaxInfo>()
        {
            _ohio,
            _penn,
            _michigan,
            _indiana
        };
    }
}
