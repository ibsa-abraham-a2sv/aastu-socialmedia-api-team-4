using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Post;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApi.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Check if the request path is not for login or register
        if (!context.Request.Path.StartsWithSegments("/auth/login") && !context.Request.Path.StartsWithSegments("/user/register"))
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsJsonAsync("User not authenticated");
                return;
            }
        }

        await _next(context);
    }
}
