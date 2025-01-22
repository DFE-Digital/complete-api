﻿using Dfe.Complete.Application.Services.TrustService;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dfe.Complete.Validators
{
    public class UkprnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Fetch the display name if it is provided
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var displayAttribute = property?.GetCustomAttribute<DisplayAttribute>();
            var displayName = displayAttribute?.GetName() ?? validationContext.DisplayName;

            var ukprn = value as string;

            if (string.IsNullOrEmpty(ukprn))
            {
                var errorMessage = $"Enter a UKPRN";

                return new ValidationResult(errorMessage);
            }

            if (ukprn.Length != 8)
            {
                var errorMessage = $"The {displayName} must be 8 digits long and start with a 1. For example, 12345678.";

                return new ValidationResult(errorMessage);
            }

            var trustService = (ITrustService)validationContext.GetService(typeof(ITrustService));
            var result = trustService.GetTrustByUkprn(ukprn).Result;

            if (!result.Any())
            {
                var errorMessage = $"There's no trust with that UKPRN. Check the number you entered is correct";

                return new ValidationResult(errorMessage);
            }

            // If valid, return success
            return ValidationResult.Success;
        }
    }
}
