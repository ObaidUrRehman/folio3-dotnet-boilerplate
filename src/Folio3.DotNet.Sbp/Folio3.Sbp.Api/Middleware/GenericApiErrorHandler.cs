using System;
using System.Net;
using System.Threading.Tasks;
using Folio3.Sbp.Service.Common.Dto;
using Folio3.Sbp.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Folio3.Sbp.Api.Middleware
{
    public static class GenericApiErrorHandler
    {
        public static async Task HandleErrorAsync(HttpContext context, ILogger logger, bool isDev)
        {
            var status = HttpStatusCode.InternalServerError;
            context.Response.StatusCode = (int) status;
            context.Response.ContentType = "application/json";

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature == null)
                return;

            var ex = contextFeature.Error;

            var response = new ResponseDto {Success = false};

            if (ex is ServiceException)
            {
                status = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            else
            {
                response.Message = "Internal Server Error.";

                logger.LogError(ex, "SSI.Api Exception");
                //response.TraceId = Sentry.SentrySdk.CaptureException(ex).ToString();

                if (isDev)
                {
                    AddExceptions(ex);

                    void AddExceptions(Exception e)
                    {
                        if (e != null)
                        {
                            response.Errors.Add($"{e.Message}\n{e.StackTrace}\n");
                            AddExceptions(e.InnerException);
                        }
                    }
                }
            }

            var json = JsonConvert.SerializeObject(response,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});

            context.Response.StatusCode = (int) status;
            await context.Response.WriteAsync(json);
        }
    }
}