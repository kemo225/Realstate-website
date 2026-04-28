using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Common.Models;

namespace RealEstate.API.Filters;

public class ApiResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is FileResult)
        {
            await next();
            return;
        }

        if (context.Result is EmptyResult)
        {
            context.Result = new OkObjectResult(CreateSuccessResponse(null));
            await next();
            return;
        }

        if (context.Result is StatusCodeResult statusCodeResult)
        {
            context.Result = new ObjectResult(CreateErrorResponse(statusCodeResult.StatusCode, null))
            {
                StatusCode = statusCodeResult.StatusCode
            };
            await next();
            return;
        }

        if (context.Result is not ObjectResult objectResult)
        {
            await next();
            return;
        }

        if (IsApiResponse(objectResult.Value))
        {
            await next();
            return;
        }

        var statusCode = objectResult.StatusCode ?? context.HttpContext.Response.StatusCode;
        var value = objectResult.Value;

        if (value is Result result)
        {
            if (!result.Succeeded && statusCode < 400)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }

            var data = GetResultData(value);

            context.Result = new ObjectResult(new ApiResponse<object?>
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "Operation completed successfully" : "Operation failed",
                Data = result.Succeeded ? data : null,
                Errors = result.Succeeded ? null : result.Errors
            })
            {
                StatusCode = statusCode
            };

            await next();
            return;
        }

        if (statusCode >= 400)
        {
            context.Result = new ObjectResult(CreateErrorResponse(statusCode, value))
            {
                StatusCode = statusCode
            };
            await next();
            return;
        }

        context.Result = new ObjectResult(CreateSuccessResponse(value))
        {
            StatusCode = statusCode
        };

        await next();
    }

    private static bool IsApiResponse(object? value)
    {
        if (value == null)
        {
            return false;
        }

        var type = value.GetType();
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ApiResponse<>);
    }

    private static object? GetResultData(object value)
    {
        var type = value.GetType();
        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Result<>))
        {
            return null;
        }

        return type.GetProperty("Data")?.GetValue(value);
    }

    private static ApiResponse<object?> CreateSuccessResponse(object? data)
    {
        return new ApiResponse<object?>
        {
            Success = true,
            Message = "Operation completed successfully",
            Data = data,
            Errors = null
        };
    }

    private static ApiResponse<object?> CreateErrorResponse(int statusCode, object? value)
    {
        return new ApiResponse<object?>
        {
            Success = false,
            Message = GetErrorMessage(statusCode),
            Data = null,
            Errors = ExtractErrors(value)
        };
    }

    private static string GetErrorMessage(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "Validation failed",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status403Forbidden => "Forbidden",
            StatusCodes.Status404NotFound => "Entity not found",
            _ => "An error occurred while processing your request"
        };
    }

    private static object? ExtractErrors(object? value)
    {
        return value switch
        {
            null => null,
            ValidationProblemDetails validationProblem => validationProblem.Errors,
            SerializableError serializableError => serializableError,
            ProblemDetails details => details.Detail ?? details.Title,
            string text => new[] { text },
            _ => value
        };
    }
}
