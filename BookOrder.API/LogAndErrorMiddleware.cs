using BookOrder.Business.Dto;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Net;

namespace BookOrder.API
{

    public class LogAndErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public LogAndErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                FireAndForget(() => CustomLogWriter(context, LogType.Entries));
                await _next(context);
                FireAndForget(() => CustomLogWriter(context, LogType.Exit));
            }
            catch (Exception ex)
            {
                FireAndForget(() => CustomErrorWriter(context, ex));

                var error = new ResultDto<string>(false, $"Exception: {ex.ToString()}", "Somethings went wrong");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(error);

                return;
            }
        }

        private void CustomLogWriter(HttpContext context, LogType logType)
        {
            string method = GetRequestMethod(context.Request.Method);
            string logDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

            switch (logType)
            {
                case LogType.Entries:
                    Debug.WriteLine($"{logDate} - BEFORE PROCESS > User request method {method}. Requested endpoint: {context.Request.Path}");
                    break;

                case LogType.Exit:
                    Debug.WriteLine($"{logDate} - AFTER PROCESS > User request completed as Successfully method {method}. Requested endpoint: {context.Request.Path}");
                    break;

                default:
                    Debug.WriteLine($"{logDate} - PROCESS DEFAULT CASE > User call method {method}. Requested endpoint: {context.Request.Path}");
                    break;
            }
        }

        private void CustomErrorWriter(HttpContext context, Exception ex)
        {
                string method = GetRequestMethod(context.Request.Method);
                Debug.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} - EXCEPTION! > Exception throwed by API when User request method {method}. Requested endpoint: {context.Request.Path}, Exception Detail: {ex?.InnerException?.ToString()}");
        }
        private void FireAndForget(Action action)
        {
            Task.Run(action)
                .ContinueWith((t) => { if (t.Exception != null) Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} - LogError!"); });
        }


        private string GetRequestMethod(string reqMethod)
        {
            string method = "GETTING";

            switch (reqMethod)
            {
                case "GET":
                    method = "GETTING";
                    break;

                case "POST":
                    method = "POSTING (CREATE OR FILTER)";
                    break;

                case "PUT":
                    method = "UPDATEING";
                    break;

                case "DELETE":
                    method = "DELETING";
                    break;

                default:
                    method = "GETTING";
                    break;
            }

            return method;
        }
    }

    public enum LogType
    {
        Entries,
        Exit,
        Exception
    }
}
