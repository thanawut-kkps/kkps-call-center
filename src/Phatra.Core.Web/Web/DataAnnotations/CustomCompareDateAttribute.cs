using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
    public class CustomCompareDateAttribute : ValidationAttribute, IClientValidatable
    {
        public CustomCompareDateAttribute(string startDate, string endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var startDate = containerType.GetProperty(this.StartDate);
            var endDate = containerType.GetProperty(this.EndDate);

            if (startDate != null && endDate != null)
            {
                DateTime startDateValue = (DateTime)startDate.GetValue(validationContext.ObjectInstance, null);
                DateTime endDateValue = (DateTime)endDate.GetValue(validationContext.ObjectInstance, null);

                if (startDateValue.AddYears(5) >= endDateValue)
                {
                    return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                }              
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "comparedategreaterthanfiveyears",
            };

            rule.ValidationParameters.Add("startdate", this.StartDate);
            rule.ValidationParameters.Add("enddate", this.EndDate);

            yield return rule;
        }

    }
}
