using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddMakeVM : IValidatableObject
    {
        public MakesItem NewMake  { get; set; }
        public IEnumerable<MakesItem> VehicleMakes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if(string.IsNullOrEmpty(NewMake.VehicleMakeName))
            {
                errors.Add(new ValidationResult("Make name must not be blank"));
            }

            return errors;
        }
    }
}