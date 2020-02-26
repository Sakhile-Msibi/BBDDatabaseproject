using System;
using System.ComponentModel.DataAnnotations; 
using Website.Models;

namespace Website.CustomAttributes{
    public class CheckDueDateAttribute: ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (ProjectModel)validationContext.ObjectInstance;
            DateTime StartDate = Convert.ToDateTime(model.StartDate);
            DateTime EndDate = Convert.ToDateTime(value);

            if (EndDate< StartDate)
            {
                return new ValidationResult(ErrorMessage ?? "Make sure your due date is greater than your start date");
            }
            
            return ValidationResult.Success;
            
        }
    }
}