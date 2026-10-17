using Microsoft.AspNetCore.Mvc;
using System.Text;
using LabA1.Models;

namespace LabA1.Controllers;

public class BookController : Controller
{

    public IActionResult Hello() => Content("Witaj w BookStore!", "text/plain; charset=utf-8");


    public IActionResult Status(int code = 200) => StatusCode(code);


    public IActionResult Details(int id)
    {
        ViewBag.Id = id;
        return View();
    }

  
    public IActionResult Echo([FromQuery] string name = "nieznajomy", [FromQuery] int count = 1) =>
        Content(string.Join("\n", Enumerable.Repeat($"Cześć, {name}!", Math.Clamp(count, 1, 20))));
    
    public IActionResult Square(int id) => Content($"{id}^2 = {id * id}");

  
    [HttpPost]
    public IActionResult Import([FromBody] BookDto dto) =>
        Json(new { received = dto.Title, dto.Isbn, dto.Price });

 
    public IActionResult Inspect()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Method: {Request.Method}");
        sb.AppendLine($"Path:   {Request.Path}{Request.QueryString}");
        foreach (var h in Request.Headers) sb.AppendLine($"{h.Key}: {h.Value}");
        return Content(sb.ToString(), "text/plain; charset=utf-8");
    }
    
  
    public IActionResult Sum(int a, int b)
    {
        return Content($"{a} + {b} = {a + b}");
    }

 
    public IActionResult UserAgent()
    {
        
        string userAgent = Request.Headers.UserAgent.ToString();
        return Content($"Twój klient to: {userAgent}");
    }

 
    public IActionResult Health()
    {
        
        return Json(new { status = "ok", time = DateTime.Now });
    }

    public IActionResult Index()
    {
        return View(BookStoreMemory.Books);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new BookFormViewModel());
    }

    [HttpPost]
    public IActionResult Create(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        BookStoreMemory.Books.Add(new Book(){
            Id = BookStoreMemory.Books.Max(x => x.Id) + 1,
            Title = model.Title,
            Author = model.Author, 
            Price= model.Price,
            NumPages= model.NumPages,
            Isbn = model.Isbn,
            PublicationDate = model.PublicationDate,

            });
        return RedirectToAction(nameof(Index));
    }
}