using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Cross
{
    public class ExceptionCross
    {
        private readonly ILogger Logger;
        private readonly RequestDelegate RequestDelegate;

        [Obsolete]
        public ExceptionCross(ILogger logger, RequestDelegate requestDelegate)
        {
            Logger = logger;
            RequestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await RequestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                var completeMessage = GetExceptionMessage(exception);
                Logger.LogInformation($"{completeMessage}\n{exception.StackTrace}");
                httpContext.Response.ContentType = "Application/JSON; Charset=UTF-8";
                await using var streamWriter = new StreamWriter(httpContext.Response.Body);
                var jsonSerilizer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                jsonSerilizer.Serialize(streamWriter, completeMessage);
                await streamWriter.FlushAsync().ConfigureAwait(false);
            }
        }

        private string GetExceptionMessage(Exception exception)
        {
            return exception.InnerException == null ? exception.Message : $"{exception.Message}\n{GetExceptionMessage(exception.InnerException)}";
        }
    }
}
