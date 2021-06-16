using CQRSMediatRDemo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CQRSMediatRDemo.Filters
{
    public class ResponseMappingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //chaining to check response
            if (context.Result is ObjectResult objectResult && objectResult.Value is CQRSResponse cqrsResponse && cqrsResponse.StatusCode != HttpStatusCode.OK)
                context.Result = new ObjectResult(new { cqrsResponse.ErrorMessage }) { StatusCode = (int)cqrsResponse.StatusCode };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
             
        }
    }
}
