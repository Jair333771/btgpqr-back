using btg_pqr_back.Common.Globals;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace btg_pqr_back.Infrastructure.Handlers
{
    public static class ExceptionMildwareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILog logger) =>
            app.UseExceptionHandler(appError =>
                appError.Run(async context =>
                    await LogHandler(context, logger)));

        public static async Task LogHandler(HttpContext context, ILog logger)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json.ToString();

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature == null && contextFeature.Error != null)
                return;

            var message = !string.IsNullOrEmpty(contextFeature.Error.Message)
                && !contextFeature.Error.Message.Contains("See the inner exception for details") ?
                    contextFeature.Error.Message : contextFeature.Error.InnerException.Message;

            logger.Error(message, contextFeature.Error);
            var result = SetErrorResponse(contextFeature, context);
            await context.Response.WriteAsync(result.ToString());
        }

        public static ErrorResponse SetErrorResponse(IExceptionHandlerFeature contextFeature, HttpContext context)
        {
            var typeException = contextFeature.Error.GetType().Name;
            var customStatus = context.Response.StatusCode;
            var customMessage = string.Empty;

            if (typeException.Equals("PqrException"))
            {
                context.Response.StatusCode = (int)contextFeature
                    .Error.GetType()
                    .GetProperty("StatusCode")
                    .GetValue(contextFeature.Error, null);

                customMessage = contextFeature
                    .Error.GetType()
                    .GetProperty("Message")
                    .GetValue(contextFeature.Error, null)
                    .ToString();

                customMessage = !customMessage.Contains("Exception of type") ? customMessage : null;
            }

            return new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = customMessage
            };
        }
    }
}