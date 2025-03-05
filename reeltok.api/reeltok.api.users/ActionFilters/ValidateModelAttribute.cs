using System.Reflection;
using reeltok.api.users.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace reeltok.api.users.ActionFilters
{
    // AttributeTargets.Class       : Allows the attribute to be applied to classes.
    // Inherited attribute = true   : Gets attributes from parent class
    // AllowMultiple = false        : Ensures a single instance of this class.
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ValidateRequest(context);
        }

        private static void ValidateRequest(ActionExecutingContext context)
        {
            // If there are any FromRoute parameters, they will be added to the ModelState
            AddFromRouteParametersToModelState(context);

            foreach (object? actionArgument in context.ActionArguments.Values)
            {
                ValidateProperties(actionArgument, context.ModelState);
            }

            if (!context.ModelState.IsValid)
            {
                List<string> errors = context.ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                string errorMessage = string.Join("; ", errors);
                FailureResponseDto failureResponse = new FailureResponseDto(errorMessage);

                context.Result = new BadRequestObjectResult(failureResponse);
            }
        }

        private static void ValidateProperties(object model, ModelStateDictionary modelState)
        {
            if (model == null)
                return;

            PropertyInfo[] properties = model.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(model);

                if (property.PropertyType == typeof(Guid) && (Guid)value == Guid.Empty)
                {
                    modelState.AddModelError(property.Name, $"{property.Name} cannot be an empty GUID.");
                }
                else if (property.PropertyType == typeof(DateTime) && (DateTime)value == DateTime.MinValue)
                {
                    modelState.AddModelError(property.Name, $"{property.Name} cannot be the minimum DateTime value.");
                }
            }
        }

        private static void AddFromRouteParametersToModelState(ActionExecutingContext context)
        {
            IEnumerable<string> routeParameters = context.ActionDescriptor.Parameters
                .Where(p => p.BindingInfo?.BindingSource == BindingSource.Path)
                .Select(p => p.Name);

            foreach (string parameter in routeParameters)
            {
                if (context.ActionArguments.TryGetValue(parameter, out object? value) && value == null)
                {
                    context.ModelState.AddModelError(parameter, $"{parameter} is required.");
                }
            }
        }
    }
}