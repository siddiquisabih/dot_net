using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Runtime.Validation.Interception;
using Abp.Runtime.Validation;
using Abp.Web.Models;

namespace Global.Project.Validation
{
    public class CustomValidationInterceptor : IMethodParameterValidator
    {
        public IReadOnlyList<ValidationResult> Validate(object validatingObject)
        {
            var validationResults = new List<ValidationResult>();

            if (validatingObject != null)
            {
                var validationContext = new ValidationContext(validatingObject);

                if (!Validator.TryValidateObject(validatingObject, validationContext, validationResults, true)
                    && validationResults.Any())
                {
                    var firstError = validationResults.First();
                    throw new AbpValidationException(firstError.ErrorMessage)
                    {
                        ValidationErrors = new List<ValidationResult> { firstError }
                    };
                }
            }

            return validationResults.AsReadOnly();
        }
    }
}