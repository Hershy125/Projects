using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddSpecialVM : IValidatableObject
    {
        public Special NewSpecial { get; set; }
        public IEnumerable<Special> Specials { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(NewSpecial.SpecialTitle))
            {
                errors.Add(new ValidationResult("A title is required"));
            }
            if (string.IsNullOrEmpty(NewSpecial.SpecialDetails))
            {
                errors.Add(new ValidationResult("Special details are required"));
            }

            return errors;
        }
    }
}