namespace RealEstate
{
    using System;
    using System.Security.Authentication;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _environment;

        public ApiExceptionFilter(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            string stackTrace = null;

            if (!_environment.IsProduction())
            {
                stackTrace = context.Exception.StackTrace;
            }

            var result = new JsonResult(new ExceptionResult(context.Exception.Message, stackTrace));

            if (context.Exception is ArgumentException || context.Exception is InvalidOperationException)
            {
                result.StatusCode = 400;
            }
            else if (context.Exception is AuthenticationException)
            {
                result.StatusCode = 401;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                result.StatusCode = 403;
            }
            else
            {
                result.StatusCode = 500;
            }

            context.Result = result;
        }
    }
}
