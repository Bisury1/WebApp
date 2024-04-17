using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Common;

public class RecordNotFoundFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        string exceptionMessage = context.Exception.Message;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.Result = new ContentResult 
        {
            Content = $"{exceptionMessage}"
        };
        context.ExceptionHandled = true;
    }
}