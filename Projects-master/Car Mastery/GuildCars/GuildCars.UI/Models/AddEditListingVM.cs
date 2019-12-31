using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddEditListingVM : IValidatableObject
    {
        public IEnumerable<VehicleMake> VehicleMakes { get; set; }
        public IEnumerable<VehicleModel> VehicleModels { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
        public IEnumerable<BodyStyle> BodyStyles { get; set; }
        public IEnumerable<TransmissionType> TransmissionTypes { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public VehicleListing Listing { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if(Listing.VehicleMakeId == 0)
            {
                errors.Add(new ValidationResult("Vehicle Make is required"));
            }

            if (Listing.VehicleModelId == 0)
            {
                errors.Add(new ValidationResult("Vehicle Model is required"));
            }

            if (string.IsNullOrEmpty(Listing.VIN))
            {
                errors.Add(new ValidationResult("VIN is required"));
            }

            var regexItem = new Regex("^[A-Z0-9 ]*$");
            if(!regexItem.IsMatch(Listing.VIN))
            {
                errors.Add(new ValidationResult("VIN cannot contain special characters or lowercase letters"));
            }

            if(Listing.VIN.Count() != 17)
            {
                errors.Add(new ValidationResult("VIN must be 17 digits long"));
            }

            if (string.IsNullOrEmpty(Listing.Description))
            {
                errors.Add(new ValidationResult("Description is required"));
            }

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if(!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, or jpeg file"));
                }
            }
            else if(string.IsNullOrEmpty(Listing.ImageFileName))
            {
                errors.Add(new ValidationResult("Image file is required"));
            }

            if(Listing.MSRP <= 0)
            {
                errors.Add(new ValidationResult("MSRP must be greater than 0"));
            }

            if (Listing.SalePrice <= 0)
            {
                errors.Add(new ValidationResult("Sale Price must be greater than 0"));
            }
            else if(Listing.SalePrice > Listing.MSRP)
            {
                errors.Add(new ValidationResult("Sale Price must not be greater than MSRP"));
            }

            if(Listing.VehicleTypeId == 1 && Listing.Mileage > 1000)
            {
                errors.Add(new ValidationResult("New vehicles should not have more than 1000 miles on them"));
            }

            if(Listing.Mileage < 0)
            {
                errors.Add(new ValidationResult("Mileage cannot be negative"));
            }

            if(Listing.VehicleTypeId == 2 && Listing.Mileage < 1000)
            {
                errors.Add(new ValidationResult("Used vehicles must have more than 1000 miles on them"));
            }

            if(Listing.Year < 2000 || Listing.Year > DateTime.Today.Year+1)
            {
                errors.Add(new ValidationResult("Year must be between the year 2000 and most current model year"));
            }

            return errors;

        }
    }
}