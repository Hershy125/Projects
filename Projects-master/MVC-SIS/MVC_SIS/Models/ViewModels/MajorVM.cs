using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.ViewModels
{
    public class MajorVM : IValidatableObject
    {
        public int MajorId { get; set; }
        public string MajorName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if(string.IsNullOrEmpty(MajorName))
            {
                errors.Add(new ValidationResult("Please enter a major", new[] { "MajorName" }));
            }

            return errors;
        }
    }
}