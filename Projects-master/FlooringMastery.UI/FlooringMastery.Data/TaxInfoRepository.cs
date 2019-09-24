using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class TaxInfoRepository : ITaxRepository
    {
        public ListTaxResponse LoadTaxInfo(string filePath)
        {
            ListTaxResponse response = new ListTaxResponse();
            response.Success = true;
            response.TaxList = new List<TaxInfo>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        TaxInfo stateTax = new TaxInfo();

                        string[] columns = line.Split(',');

                        stateTax.StateAbbreviation = columns[0];
                        stateTax.StateName = columns[1];
                        stateTax.TaxRate = decimal.Parse(columns[2]);

                        response.TaxList.Add(stateTax);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }
    }
}
