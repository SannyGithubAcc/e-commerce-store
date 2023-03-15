using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace e_commerce_store.Exceptions
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CustomException customException)
            {
                _logger.LogError(customException, $"Custom exception: {customException.Message}");
                context.Result = new ObjectResult(customException.Message)
                {
                    StatusCode = (int)customException.StatusCode
                };
            }
            else
            {
                _logger.LogError(context.Exception, $"Unhandled exception: {context.Exception.Message}");
                context.Result = new ObjectResult("An error occurred while processing your request.")
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
