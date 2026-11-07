using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabA1.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _log;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> log) => _log = log;

    public void OnException(ExceptionContext context)
    {
        _log.LogError(context.Exception, "Przechwycono wyjątek w GlobalExceptionFilter");

        if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
        {
            context.Result = new ObjectResult(new ProblemDetails
            {
                Status = 500, Title = "Błąd API", Detail = context.Exception.Message
            }) { StatusCode = 500 };
        }
        else
        {
          
            context.Result = new ViewResult { ViewName = "Error" };
            context.HttpContext.Response.StatusCode = 500;
        }
        
        context.ExceptionHandled = true;
    }
}