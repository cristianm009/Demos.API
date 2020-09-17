using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Net.Http;

namespace Demos.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                var exception = (UnauthorizedAccessException)context.Exception;
                var validation = new
                {
                    Status = 401,
                    Title = "Unauthorized",
                    Detail = $"Unauthorized -- {exception.Message}"
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.ExceptionHandled = true;
            }
            else
            {
                if (context.Exception is Exception)
                {
                    var exception = (Exception)context.Exception;
                    var validation = new
                    {
                        Status = 400,
                        Title = "Bad Request",
                        Detail = $"Bad Request -- {exception.Message}"
                    };
                    var json = new
                    {
                        errors = new[] { validation }
                    };
                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.ExceptionHandled = true;
                }
                else
                {
                    if (context.Exception is HttpRequestException)
                    {
                        var exception = (HttpRequestException)context.Exception;
                        var validation = new
                        {
                            Status = 400,
                            Title = "HttpRequestException",
                            Detail = $"HttpRequestException -- {exception.Message}"
                        };
                        var json = new
                        {
                            errors = new[] { validation }
                        };
                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.ExceptionHandled = true;
                    }
                    else
                    {
                        var validation = new
                        {
                            Status = 400,
                            Title = "Bad Request",
                            Detail = "Unexpected error"
                        };
                        var json = new
                        {
                            errors = new[] { validation }
                        };
                        context.Result = new BadRequestObjectResult(json);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.ExceptionHandled = true;
                    }
                }
            }
        }
    }
}
