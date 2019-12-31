using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddModelVM : IValidatableObject
    {
        public ModelsItem NewModel { get; set; }
        public IEnumerable<ModelsItem> VehicleModels { get; set; }
        public IEnumerable<MakesItem> VehicleMakes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if(string.IsNullOrEmpty(NewModel.VehicleModelName))
            {
                errors.Add(new ValidationResult("Vehicle Model Name is required"));
            }

            return errors;
        }
    }
}