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
        return Content($"Przechwycona ścieżka: {path ?? string.Empty}");
    }

    public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
    {
        var routes = provider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(descriptor =>
            {
                var area = descriptor.RouteValues.TryGetValue("area", out var areaName) ? areaName : null;
                var methods = descriptor.ActionConstraints?
                    .OfType<HttpMethodActionConstraint>()
                    .SelectMany(constraint => constraint.HttpMethods)
                    .Distinct()
                    .ToArray();

                return new RouteInfo
                {
                    Pattern = GetRoutePattern(descriptor, area),
                    Controller = descriptor.ControllerName,
                    Action = descriptor.ActionName,
                    Area = area,
                    Methods = methods is { Length: > 0 } ? string.Join(", ", methods) : "*"
                };
            })
            .OrderBy(route => route.Pattern)
            .ThenBy(route => route.Controller)
            .ThenBy(route => route.Action)
            .ToList();

        return View(routes);
    }

    private static string GetRoutePattern(ControllerActionDescriptor descriptor, string? area)
    {
        if (descriptor.AttributeRouteInfo?.Template is { } attributeRoute)
        {
            return $"/{attributeRoute}";
        }

        return (descriptor.ControllerName, descriptor.ActionName) switch
        {
            ("Book", "ByYear") => "/books/year/{year:int:min(1450)}",
            ("Book", "ByIsbn") => "/books/isbn/{isbn:regex(^\\d{{13}}$)}",
            ("Home", "Docs") => "/docs/{*path}",
            _ when area is not null => $"/{area}/{{controller=Dashboard}}/{{action=Index}}/{{id?}}",
            _ => "/{controller=Home}/{action=Index}/{id?}"
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    using Microsoft.AspNetCore.Mvc.Infrastructure; 
        public IActionResult Docs(string? path)
        {
            return Content($"Jesteś w dziale dokumentacji. Ścieżka: {path}");
        }
    
   
        public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Wklej to do pliku lab3.md:\n");
            sb.AppendLine("| Adres (Template) | Kontroler.Akcja | Metoda HTTP |");
            sb.AppendLine("|---|---|---|");
            
            foreach (var route in provider.ActionDescriptors.Items)
            {
                var template = route.AttributeRouteInfo?.Template ?? "Konwencjonalna";
                var controller = route.RouteValues["controller"];
                var action = route.RouteValues["action"];
                sb.AppendLine($"| {template} | {controller}.{action} | ANY/GET/POST |");
            }
            return Content(sb.ToString(), "text/plain; charset=utf-8");
        }
}