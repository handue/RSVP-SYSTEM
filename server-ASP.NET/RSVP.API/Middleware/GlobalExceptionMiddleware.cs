using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using RSVP.Core.Exceptions;
using RSVP.Core.DTOs;

namespace RSVP.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        // RequestDelegate: HTTP 요청을 처리하는 함수 대리자로, 다음 미들웨어로 요청을 전달하는 역할
        // RequestDelegate: A function delegate that processes HTTP requests and passes them to the next middleware
        private readonly RequestDelegate _next;

        // ILogger: 애플리케이션의 이벤트와 오류를 기록하는 로깅 인터페이스
        // ILogger: Logging interface that records application events and errors
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        // IWebHostEnvironment: 애플리케이션 실행 환경(개발/스테이징/프로덕션)에 대한 정보를 제공하는 인터페이스
        // IWebHostEnvironment: Interface that provides information about the application's runtime environment (dev/staging/prod)
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

    // * 해당 InvokeAsync는 Program.cs에 app.UseMiddleware에 이 클래스가 등록돼 있으면, http 요청이 서버에 도착시, 미들웨어 클래스에서 InvokeAsync 또는 Invoke 를 찾아 자동으로 실행해줌.
    // * 첫 번째 매개변수 타입은 httpcontex , 반환 타입은 Task여야함
        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Code = GetErrorCode(exception),
                Message = GetErrorMessage(exception),
                Details = _env.IsDevelopment() ? exception.ToString() : null
            };

            response.StatusCode = GetStatusCode(exception);
            _logger.LogError(exception,
          "An error occurred: {Message}. Request Path: {Path}, Method: {Method}",
          exception.Message,
          context.Request.Path,
          context.Request.Method);

            var apiResponse = ApiResponse<object>.CreateError(errorResponse);
            await response.WriteAsJsonAsync(apiResponse);
        }

        private string GetErrorCode(Exception exception)
        {
            return exception switch
            {
                // * 당장에 사용자 정의를 할지는 모르곘다. 
                AppException appException => appException.ErrorCode,
                KeyNotFoundException => ErrorCodes.NotFound,
                UnauthorizedAccessException => ErrorCodes.Unauthorized,
                InvalidOperationException => ErrorCodes.BusinessRuleViolation,
                _ => "INTERNAL_SERVER_ERROR"
            };
        }

        private string GetErrorMessage(Exception exception)
        {
            return exception switch
            {
                AppException appException => string.Format(appException.Message, appException.Parameters),
                KeyNotFoundException => "The requested resource was not found.",
                UnauthorizedAccessException => "You are not authorized to perform this action.",
                InvalidOperationException => exception.Message,
                _ => "An unexpected error occurred."
            };
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                AppException appException => appException.ErrorCode switch
                {
                    ErrorCodes.NotFound => (int)HttpStatusCode.NotFound,
                    ErrorCodes.Unauthorized => (int)HttpStatusCode.Unauthorized,
                    ErrorCodes.Forbidden => (int)HttpStatusCode.Forbidden,
                    ErrorCodes.ValidationError => (int)HttpStatusCode.BadRequest,
                    ErrorCodes.DuplicateEntry => (int)HttpStatusCode.Conflict,
                    _ => (int)HttpStatusCode.BadRequest
                },
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }

  
}