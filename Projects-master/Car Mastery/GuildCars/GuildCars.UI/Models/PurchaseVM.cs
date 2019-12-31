using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GuildCars.UI.Models
{
    public class PurchaseVM : IValidatableObject
    {
        public ListingDetailItem VehicleDetails { get; set; }
        public Purchase NewPurchase { get; set; }
        public Customer Buyer { get; set; }
        public IEnumerable<PurchaseType> PurchaseTypes { get; set; }
        public IEnumerable<string> StatesList
        {
            get
            {
                List<string> states = new List<string>()
                {
                    "AK",
                    "AL",
                    "AR",
                    "AZ",
                    "CA",
                    "CO",
                    "CT",
                    "DE",
                    "FL",
                    "GA",
                    "HI",
                    "IA",
                    "ID",
                    "IL",
                    "IN",
                    "KS",
                    "KY",
                    "LA",
                    "MA",
                    "MD",
                    "ME",
                    "MI",
                    "MN",
                    "MO",
                    "MS",
                    "MT",
                    "NC",
                    "ND",
                    "NE",
                    "NH",
                    "NJ",
                    "NM",
                    "NV",
                    "NY",
                    "OH",
                    "OK",
                    "OR",
                    "PA",
                    "RI",
                    "SC",
                    "SD",
                    "TN",
                    "TX",
                    "UT",
                    "VA",
                    "VT",
                    "WA",
                    "WI",
                    "WV",
                    "WY"

                };

                return states;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Buyer.Name))
            {
                errors.Add(new ValidationResult("Name is required"));
            }

            if (string.IsNullOrEmpty(Buyer.Phone))
            {
                errors.Add(new ValidationResult("A phone number is required"));
            }

            if (string.IsNullOrEmpty(Buyer.Email))
            {
                errors.Add(new ValidationResult("Email address is required"));
            }

            if (string.IsNullOrEmpty(Buyer.Street1))
            {
                errors.Add(new ValidationResult("Street address is required"));
            }

            if (string.IsNullOrEmpty(Buyer.City))
            {
                errors.Add(new ValidationResult("City is required"));
            }

            var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";

            if (!Regex.Match(Buyer.ZipCode, _usZipRegEx).Success)
            {
                errors.Add(new ValidationResult("Valid ZipCode is required"));
            }

            if (NewPurchase.PurchasePrice < (VehicleDetails.SalePrice * 0.95M))
            {
                errors.Add(new ValidationResult("Purchase price cannot be less than 95 % of sale price"));
            }

            if (NewPurchase.PurchasePrice > VehicleDetails.MSRP)
            {
                errors.Add(new ValidationResult("Purchase price cannot be greater than MSRP"));
            }


            return errors;
        }
    }
}