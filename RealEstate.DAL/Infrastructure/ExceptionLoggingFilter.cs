namespace RealEstate
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class ExceptionLoggingFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionLoggingFilter(ILoggerFactory logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger.CreateLogger("Global Exception Filter");
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Global exception filter: {0}", context.Exception);
        }
    }
}
