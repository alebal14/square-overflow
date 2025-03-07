using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SquareOverFlowApi.Middlewares
{
    public class ErrorHandlingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ErrorHandlingAttribute>>();

                logger.LogError(context.Exception, "API Error: {ExceptionType} - {Message}",
                    context.Exception.GetType().Name, context.Exception.Message);

                context.Result = new ObjectResult("An error occurred while processing your request.")
                {
                    StatusCode = 500
                };

                context.ExceptionHandled = true;
            }

            base.OnActionExecuted(context);
        }
    }
}
