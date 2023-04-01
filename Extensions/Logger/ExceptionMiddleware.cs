namespace GlobalException
{
    using System.Net;
    using System.Threading.Tasks;
    using Logger;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Exception = System.Exception;

    public static class ExceptionHandlerExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;        

        public ExceptionMiddleware(RequestDelegate next)
        {           
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {           
            var loggerFactory = (ILoggerFactory)httpContext.RequestServices.GetService(typeof(ILoggerFactory));
            var logger= loggerFactory.CreateLogger<ExceptionMiddleware>();
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                await HandleExceptionAsync(httpContext);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new 
                                                   {
                                                       context.Response.StatusCode,
                                                       Message = "Internal Server Error from the custom middleware."
                                                   }.ToString());
        }
    }
   
}
