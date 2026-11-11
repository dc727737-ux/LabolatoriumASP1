using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabA1.Filters;

public class MeasureTimeAttribute : ActionFilterAttribute
{
    private Stopwatch? _sw;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _sw = Stopwatch.StartNew();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _sw?.Stop();
        context.HttpContext.Response.Headers["X-Action-Ms"] = _sw?.ElapsedMilliseconds.ToString();
    }
}