using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Controllers.Filters
{
    public class ContentTypeFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers["Content-Type"] != "application/json")
            {
                context.Result = new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Content = "Bad content-type in header",
                    ContentType = "application/json"
                };
            }
        }
    }
}