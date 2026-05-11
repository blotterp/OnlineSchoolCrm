using FluentValidation;
using System.Net;
using System.Text.Json;


namespace OnlineSchoolCrm.Middleware;
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {  
        _next = next; 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
        
}
