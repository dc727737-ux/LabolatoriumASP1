using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using LabA1.Models;

namespace LabA1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Docs(string? path)
    {
        return Content($"Jesteś w dziale dokumentacji. Ścieżka: {path}");
    }

    public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
    {
        var rows = provider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(r =>
            {
                var methodsList = r.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                    .SelectMany(c => c.HttpMethods)
                    .ToList();

                return new RouteInfo
                {
              
                    Pattern = "/" + (r.AttributeRouteInfo?.Template ?? "Konwencja"),
                    Action = r.ActionName,
                    Controller = r.ControllerName,

                    Methods = (methodsList != null && methodsList.Any()) 
                        ? string.Join(", ", methodsList) 
                        : "*",
                    Area = r.RouteValues.TryGetValue("area", out var area) ? area : null
                };
            })
            .ToList();

   
        return View(rows);
    }

    public IActionResult Boom() => throw new InvalidOperationException("Celowy wyjątek");

    public IActionResult HttpStatus(int code)
    {
        Response.StatusCode = code;
        ViewBag.Code = code;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
    
}
   