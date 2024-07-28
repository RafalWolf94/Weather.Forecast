using System.Diagnostics;
using System.Net;
using Core.Domain.TechnicalStuff.ErrorCodes;
using Core.Domain.TechnicalStuff.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Weather.Forecast.TechnicalStuff.Error;

public static class ExceptionPolicy
{
    public static void UseAppExceptionPolicy(this IApplicationBuilder app) => app.Run(Handle);

    private static async Task Handle(HttpContext httpContext)
    {
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionDetails?.Error;
        if (exception is null) return;

        var responseModel = new ErrorResponse(Activity.Current?.Id);
        
        if (exception is BaseException baseException)
            SetResponseFromCustomException(responseModel, httpContext.Response, baseException);
        else
            SetResponseFromGenericException(responseModel, httpContext.Response, exception);

        await httpContext.Response.WriteAsJsonAsync(responseModel);
    }

    private static void SetResponseFromCustomException(ErrorResponse responseModel, HttpResponse httpResponse, BaseException exception)
    {
        responseModel.ErrorCode = exception.GetErrorCode();

        httpResponse.StatusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            DomainException => (int)HttpStatusCode.BadRequest,
            DesignErrorException => (int)HttpStatusCode.InsufficientStorage,
            _ => (int)HttpStatusCode.InsufficientStorage,
        };
    }

    private static void SetResponseFromGenericException(ErrorResponse responseModel, HttpResponse httpResponse, Exception exception)
    {
        switch (exception)
        {
            case DbUpdateConcurrencyException:
                responseModel.ErrorCode = ErrorCode.DbUpdateConcurrencyExceptionErrorCode.Code;
                httpResponse.StatusCode = (int)HttpStatusCode.Conflict;
                break;
            case BadHttpRequestException:
                responseModel.ErrorCode = ErrorCode.BadHttpRequestExceptionErrorCode.Code;
                httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                responseModel.ErrorCode = ErrorCode.DefaultExceptionErrorCode.Code;
                httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
    }
}