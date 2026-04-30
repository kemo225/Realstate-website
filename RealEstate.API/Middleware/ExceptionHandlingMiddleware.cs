using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Exceptions;

namespace RealEstate.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An error occurred while processing your request.";
        object? errors = null;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                message = "Validation failed";
                var validationErrors = new Dictionary<string, string[]>();
                foreach (var group in validationException.Errors.GroupBy(e => e.PropertyName))
                    validationErrors.Add(group.Key, group.Select(e => e.ErrorMessage).ToArray());
                errors = validationErrors;
                break;

            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
                break;

            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                message = badRequestException.Message;
                break;

            case ValidatationException validatationException:
                statusCode = HttpStatusCode.BadRequest;
                message = validatationException.Message;
                break;

            case UnauthorizedException unauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                message = unauthorizedException.Message;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                errors = new[] { exception.Message };
                break;
        }

        var result = JsonSerializer.Serialize(new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Data = null,
            Errors = errors
        }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }
}
