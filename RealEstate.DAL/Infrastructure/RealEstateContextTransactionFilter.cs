namespace RealEstate
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class RealEstateContextTransactionFilter : IAsyncActionFilter
    {
        private readonly RealEstateContext _realEsateContext;

        public RealEstateContextTransactionFilter(RealEstateContext realestatetContext)
        {
            _realEsateContext = realestatetContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method != HttpMethod.Get.Method)
            {
                _realEsateContext.BeginTransaction();

                var executed = await next();

                if (executed.Exception != null && !executed.ExceptionHandled)
                {
                    _realEsateContext.RollbackTransaction();
                }
                else
                {
                    await _realEsateContext.CommitTransactionAsync();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
