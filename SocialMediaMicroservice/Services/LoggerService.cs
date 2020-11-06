using JungleMicroserviceEntities.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using static JungleUtilityServices.APIClient;
using static JungleUtilityServices.EndPoint;
    public static class LoggerService
    {
        #region Logger
        public async static Task<APIResponse> CreateAppLogger(Exception ex)
        {
            APIResponse api = new APIResponse();
            try
            {
                ApplicationLogger _logger = new ApplicationLogger()
                {
                    message = ex.Message,
                    stackTrace = ex.StackTrace,
                    loggedDate = DateTime.Now.ToString(),
                    loggedBy = "LoginMicroservice"
                };
                api = await APICall(UTILITY_MICROSERVICE, $"/api/AppLogger/CreateAppLogger", _logger, "post");
            }
            catch
            {
            }
            return api;
        }
        #endregion
    }

#region ErrorLoggingMiddleware

        public class ErrorLoggingMiddleware
        {
            private readonly RequestDelegate _next;

            public ErrorLoggingMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception e)
                {
                    await LoggerService.CreateAppLogger(e);
                }
            }
        }

        public static class ErrorLoggingMiddlewareExtensions
        {
            public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<ErrorLoggingMiddleware>();
            }
        }

#endregion
