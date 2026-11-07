using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LabA1.Models;

public class RouteaInfo
{
    public string Pattern { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string? Area { get; set; }
    public string Methods { get; set; }
}

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
    {
        var rows = provider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(r =>
            {
                return new RouteaInfo()
                {
                    Pattern = "/" + (r.AtributeRouteInfo?.Template ?? "Konwencja"),
                    Action = r.ActionName,
                    Controller = r.ControllerName,
                    Methods = string.Join(",",
                        r.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                            .Select(c => c.HttpMethods ?? ["*"]) ?? Array.Empty<string>()
                    ),
                    Area = r.RouteValues.TryGetValue("area", out var area) ? area : null
                };
            });
        return View(routes);
    }
}