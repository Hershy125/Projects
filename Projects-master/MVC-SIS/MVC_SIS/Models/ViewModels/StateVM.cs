using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.ViewModels
{
    public class StateVM : IValidatableObject
    {
        public string StateAbbreviation { get; set; }
        public string StateName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(StateName) || string.IsNullOrEmpty(StateAbbreviation))
            {
                errors.Add(new ValidationResult("Please enter a state name and abbreviation!", new[] { "StateName", "StateAbbreviation" }));
            }
            else if (StateAbbreviation.Any(Char.IsDigit) || StateAbbreviation.Length > 2)
            {
                errors.Add(new ValidationResult("Please enter a valid State Abbreviation", new[] { "StateAbbreviation" }));
            }

            return errors;
        }
    }
}