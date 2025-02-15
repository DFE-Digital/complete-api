﻿using Dfe.Complete.Application.Projects.Queries.GetProject;
using Dfe.Complete.Domain.ValueObjects;
using Dfe.Complete.Extensions;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dfe.Complete.Validators
{
    public class UrnAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Fetch the display name if it is provided
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var displayAttribute = property?.GetCustomAttribute<DisplayAttribute>();
            var displayName = displayAttribute?.GetName() ?? validationContext.DisplayName;

            var urn = value as string;

            if (string.IsNullOrEmpty(urn))
                return ValidationResult.Success;
            
            if (urn.Length != 6)
            {
                var errorMessage = $"The {displayName} must be 6 digits long. For example, 123456.";
                return new ValidationResult(errorMessage);
            }

            var sender = (ISender)validationContext.GetService(typeof(ISender));

            var result = sender.Send(new GetProjectByUrnQuery(new Urn(urn.ToInt())));

            if (!result.Result.IsSuccess)
            {
                throw new Exception(result.Result.Error);
            }

            if (result.Result?.Value != null)
            {
                var errorMessage = $"A project with the urn: {urn} already exists";

                return new ValidationResult(errorMessage);
            }

            // If valid, return success
            return ValidationResult.Success;
        }
    }
}
