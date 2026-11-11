using System.Collections.Concurrent;
using System.Diagnostics;

namespace LabA1.Middlewares;

public class RequestStats                 
{
    private long _total;
    public long Total => Interlocked.Read(ref _total);
    
 
    public ConcurrentDictionary<string, long> PathCounts { get; } = new();

    public long Hit(string path)
    {
        PathCounts.AddOrUpdate(path, 1, (_, count) => count + 1);
        return Interlocked.Increment(ref _total);
    }
}

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _log;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> log)
    { 
        _next = next; 
        _log = log; 
    }

    public async Task InvokeAsync(HttpContext ctx, RequestStats stats)
    {

        if (ctx.Request.Path.StartsWithSegments("/blokada")) 
        { 
            ctx.Response.StatusCode = 403; 
            return; 
        }

        var sw = Stopwatch.StartNew();
        var n = stats.Hit(ctx.Request.Path); // Zliczanie konkretnej ścieżki
        
        ctx.Response.OnStarting(() => {
            ctx.Response.Headers["X-Elapsed-Ms"] = sw.ElapsedMilliseconds.ToString();
            ctx.Response.Headers["X-Request-Count"] = n.ToString();
            return Task.CompletedTask;
        });

        await _next(ctx);                                 

        _log.LogInformation("{Method} {Path} -> {Status} w {Ms} ms", 
            ctx.Request.Method, ctx.Request.Path, ctx.Response.StatusCode, sw.ElapsedMilliseconds);
    }
}