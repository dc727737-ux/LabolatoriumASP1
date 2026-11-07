using LabA1.Middlewares; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<RequestStats>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseMiddleware<RequestTimingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "admin-area",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "books-by-year", 
    pattern: "books/year/{year:int:min(1450)}",
    defaults: new { controller = "Book", action = "ByYear" });

app.MapControllerRoute(
    name: "books-by-isbn", 
    pattern: "books/isbn/{isbn:regex(^\\d{{13}}$)}",
    defaults: new { controller = "Book", action = "ByIsbn" });

app.MapControllerRoute(
    name: "docs", 
    pattern: "docs/{*path}",
    defaults: new { controller = "Home", action = "Docs" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();