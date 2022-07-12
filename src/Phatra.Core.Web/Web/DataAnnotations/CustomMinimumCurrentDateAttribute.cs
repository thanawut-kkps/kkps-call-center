using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
    public class CustomMinimumCurrentDateAttribute : ValidationAttribute, IClientValidatable
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime objValue = (DateTime)value;

            DateTime mindate = DateTime.Now;
            mindate.AddYears(this.Year);
            mindate.AddMonths(this.Month);
            mindate.AddDays(this.Day);

            if (objValue <= mindate.Date) return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "dategreaterthancurrent",
            };

            rule.ValidationParameters.Add("addyear", this.Year);
            rule.ValidationParameters.Add("addmonth", this.Month);
            rule.ValidationParameters.Add("addday", this.Day);

            yield return rule;
        }


    }
}