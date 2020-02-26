using System;
using System.ComponentModel.DataAnnotations;

namespace Website.CustomAttributes{
    public class CheckStartDateAttribute: ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            DateTime startDate = (DateTime)value;
            if (startDate >= DateTime.UtcNow) 
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Project start dates can't be less than today's date");
        }
    }
}