using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
    public class RequiredItemAttribute : ValidationAttribute, IClientValidatable
    {
        private int _minimumItem;

        public RequiredItemAttribute()
        {
            _minimumItem = 1;
            this.ErrorMessage = "Please select item at least 1 item";
        }

        public int MinimumItem
        {
            get { return _minimumItem; }
            set { _minimumItem = value; }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
            if (value is ICollection<object>)
            {
                ICollection<object> list = (ICollection<object>)value;

                if (list == null || list.Count == 0)
                {
                    return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                }
            }
            else if (value is object[])
            {
                object[] list = (object[])value;

                if (list == null || list.Length == 0)
                {
                    return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "requireditem"
            };
        }
    }
}
