using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.ActionFilters
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

        private static void AddFromRouteParametersToModelState(ActionExecutingContext context)
        {
            IEnumerable<string> routeParameters = context.ActionDescriptor.Parameters
                .Where(p => p.BindingInfo?.BindingSource == BindingSource.Path)
                .Select(p => p.Name);

            foreach (string parameter in routeParameters)
            {
                if (context.ActionArguments.TryGetValue(parameter, out var value) && value == null)
                {
                    context.ModelState.AddModelError(parameter, $"{parameter} is required.");
                }
            }
        }
    }
}