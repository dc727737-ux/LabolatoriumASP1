using LabA1.Middlewares;
using LabA1.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews(options => 
{
    options.Filters.Add<GlobalExceptionFilter>();
});


builder.Services.AddSingleton<RequestStats>();


builder.Services.AddScoped<LogActionFilter>();

var app = builder.Build();


app.UseMiddleware<RequestTimingMiddleware>();

if (!app.Environment.IsDevelopment())
{
   
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseStatusCodePagesWithReExecute("/Home/HttpStatus", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "admin-area",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "archive",
    pattern: "archive/{year:int}/{month:int:range(1,12)}",
    defaults: new { controller = "Book", action = "Archive" });


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