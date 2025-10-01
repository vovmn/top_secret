using IAM.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IAM.API.Extensions;

// Infrastructure/Middleware/ExceptionMiddlewareExtensions.cs
public static class ExceptionMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                var (statusCode, title) = exception switch
                {
                    ValidationException => (StatusCodes.Status400BadRequest, "Validation error"),
                    ArgumentNullException => (StatusCodes.Status500InternalServerError, "Empty field"),
                    RepositoryException => (StatusCodes.Status500InternalServerError, "Database error"),
                    ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument error"),
                    AccessViolationException => (StatusCodes.Status401Unauthorized, "Access Denied"),
                    _ => (StatusCodes.Status500InternalServerError, "Internal server error")
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = exception?.Message
                }));
            });
        });
    }
}
