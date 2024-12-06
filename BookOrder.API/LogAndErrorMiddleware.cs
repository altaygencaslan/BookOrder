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

        public async Task Invoke(HttpContext context)
        {
            string method = string.Empty;
            switch (context.Request.Method)
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

            context.Request.EnableBuffering();
            var contentBody = await (new StreamReader(context.Request.Body)).ReadToEndAsync();
            contentBody = string.IsNullOrEmpty(contentBody) ? "no_data" : contentBody;
            context.Request.Body.Position = 0;

            var contentQuery = context.Request.QueryString.Value;
            contentQuery = string.IsNullOrEmpty(contentQuery) ? "no_data" : contentQuery;

            try
            {
                Debug.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} - BEFORE PROCESS > User request method {method}. Requested endpoint: {context.Request.Path} with Query: {contentQuery} OR with Body: {contentBody}");
                await _next(context);
                Debug.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} - AFTER PROCESS > User request completed as Successfully method {method}. Requested endpoint: {context.Request.Path} with Query: {contentQuery} OR with Body: {contentBody}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} - EXCEPTION! > Exception throwed by API when User request method {method}. Requested endpoint: {context.Request.Path} \n  " +
                                  $"with Query: {contentQuery} OR with Body: {contentBody} " +
                                  $"\n Exception Detail: {ex.InnerException}");

                var error = new ResultDto<string>(false, $"Exception: {ex.ToString()}", "Somethings went wrong");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(error);
                return;
            }
        }
    }
}
