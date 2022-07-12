using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
    public class DateRangeAttribute : ValidationAttribute, IClientValidatable
    {
        private DateTime? _minDate;
        private DateTime? _maxDate;

        public DateRangeAttribute(DateTime minDate)
        {
            this._minDate = minDate;
        }

        public DateRangeAttribute(DateTime minDate, DateTime maxDate)
        {
            this._minDate = minDate;
            this._maxDate = maxDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //// get a reference to the property this validation depends upon
            //var containerType = validationContext.ObjectInstance.GetType();
            //var field = containerType.GetProperty(this.DependentProperty);

            //if (field != null)
            //{
            //    // get the value of the dependent property
            //    var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

            //    // compare the value against the target value
            //    if ((dependentvalue == null && this.TargetValue == null) ||
            //        (dependentvalue != null && dependentvalue.Equals(this.TargetValue)))
            //    {
            //        // match => means we should try validating this field
            //        if (!_innerAttribute.IsValid(value))
            //            // validation failed - return an error
            //            return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
            //    }
            //}

            DateTime objValue = (DateTime)value;
            if (this._maxDate.HasValue)
            {
                if (this._minDate <= objValue && objValue <= this._maxDate) return ValidationResult.Success;
                return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
            }
            else
            {
                if (this._minDate <= objValue) return ValidationResult.Success;
                return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
            }

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //var rule = new ModelClientValidationRule()
            //{
            //    ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            //    ValidationType = "requiredif",
            //};

            //string depProp = BuildDependentPropertyId(metadata, context as ViewContext);

            //// find the value on the control we depend on;
            //// if it's a bool, format it javascript style 
            //// (the default is True or False!)
            //string targetValue = (this.TargetValue ?? "").ToString();
            //if (this.TargetValue.GetType() == typeof(bool))
            //    targetValue = targetValue.ToLower();

            //rule.ValidationParameters.Add("dependentproperty", depProp);
            //rule.ValidationParameters.Add("targetvalue", targetValue);
            //rule.ValidationParameters.Add("isvalidaterequiredif", "true");

            //yield return rule;
            return null;
        }
    }
}