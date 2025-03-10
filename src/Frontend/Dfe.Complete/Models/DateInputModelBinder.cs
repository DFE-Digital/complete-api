using Dfe.Complete.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Dfe.Complete.Models
{
    public class DateInputModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Type modelType = ValidateBindingContext(bindingContext);

            var dayModelName = $"{bindingContext.ModelName}-day";
            var monthModelName = $"{bindingContext.ModelName}-month";
            var yearModelName = $"{bindingContext.ModelName}-year";

            var dayValueProviderResult = bindingContext.ValueProvider.GetValue(dayModelName);
            var monthValueProviderResult = bindingContext.ValueProvider.GetValue(monthModelName);
            var yearValueProviderResult = bindingContext.ValueProvider.GetValue(yearModelName);

			if (IsOptionalOrFieldTypeMismatch(bindingContext, dayValueProviderResult, monthValueProviderResult, yearValueProviderResult))
			{
				if (modelType == typeof(DateTime?))
				{
					bindingContext.Result = ModelBindingResult.Success(null);
				}
                else
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                }

                return Task.CompletedTask;
            }

            var displayName = bindingContext.ModelMetadata.DisplayName;

            IDateValidationMessageProvider page =
                (bindingContext.ActionContext as Microsoft.AspNetCore.Mvc.RazorPages.PageContext)?.ViewData.Model as IDateValidationMessageProvider;

            var validator = new DateValidationService(page);
            (bool dateValid, string validationMessage) =
                validator.Validate(dayValueProviderResult.FirstValue, monthValueProviderResult.FirstValue, yearValueProviderResult.FirstValue, displayName);

            if (dateValid)
            {
                int day = int.Parse(dayValueProviderResult.FirstValue);
                int month = int.Parse(monthValueProviderResult.FirstValue);
                int year = int.Parse(yearValueProviderResult.FirstValue);

                var date = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);
                (bool validDateRange, string message) = IsInValidDateRange(date, bindingContext, displayName);

                if (validDateRange)
                {
                    bindingContext.Result = ModelBindingResult.Success(date);
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }
            else
            {
                bindingContext.ModelState.SetModelValue(dayModelName, dayValueProviderResult);
                bindingContext.ModelState.SetModelValue(monthModelName, monthValueProviderResult);
                bindingContext.ModelState.SetModelValue(yearModelName, yearValueProviderResult);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, validationMessage);
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }

        private static Type ValidateBindingContext(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelType = bindingContext.ModelType;
            if (modelType != typeof(DateTime) && modelType != typeof(DateTime?))
            {
                throw new InvalidOperationException($"Cannot bind {modelType.Name}.");
            }

            return modelType;
        }

        private static (bool, string) IsInValidDateRange(DateTime date, ModelBindingContext bindingContext, string displayName)
        {
            if (bindingContext.ModelMetadata is DefaultModelMetadata defaultModelMetadata
                && defaultModelMetadata.Attributes.Attributes.FirstOrDefault(a => a.GetType() == typeof(DateValidationAttribute)) is DateValidationAttribute dateValidation)
            {
                return DateRangeValidationService.Validate(date, dateValidation.DateValidationEnum, displayName);
            }

            return (true, string.Empty);
        }

        private static bool IsOptionalOrFieldTypeMismatch(ModelBindingContext bindingContext, ValueProviderResult dayValueProviderResult, ValueProviderResult monthValueProviderResult, ValueProviderResult yearValueProviderResult)
        {
            return string.IsNullOrWhiteSpace(dayValueProviderResult.FirstValue)
                   && string.IsNullOrWhiteSpace(monthValueProviderResult.FirstValue)
                   && string.IsNullOrWhiteSpace(yearValueProviderResult.FirstValue)
                   && !bindingContext.ModelMetadata.IsRequired
                   || dayValueProviderResult == ValueProviderResult.None
                   && monthValueProviderResult == ValueProviderResult.None
                   && yearValueProviderResult == ValueProviderResult.None;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DateValidationAttribute : Attribute
    {
        public DateValidationAttribute(DateRangeValidationService.DateRange dateValidationEnum)
        {
            DateValidationEnum = dateValidationEnum;
        }

        public DateRangeValidationService.DateRange DateValidationEnum { get; }
    }
}