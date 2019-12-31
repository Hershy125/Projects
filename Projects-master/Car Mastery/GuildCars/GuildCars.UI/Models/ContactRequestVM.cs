using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ContactRequestVM : IValidatableObject
    {
        public ContactRequest NewRequest { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(NewRequest.Name))
            {
                errors.Add(new ValidationResult("Name is required"));
            }
            if (string.IsNullOrEmpty(NewRequest.Email) && string.IsNullOrEmpty(NewRequest.Phone))
            {
                errors.Add(new ValidationResult("A phone number or email address is required"));
            }
            if (string.IsNullOrEmpty(NewRequest.Message))
            {
                errors.Add(new ValidationResult("Message is required"));
            }

            return errors;
        }
    }
}