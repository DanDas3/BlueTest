using Agenda.Service.Exceptions;
using System.Net;
using System.Text.Json;

namespace Agenda.Api.Middleware
{
    public class ExceptionHanddlerMiddware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHanddlerMiddware(RequestDelegate next, ILogger<ExceptionHanddlerMiddware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AgendaException e:                        
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    
                    default:
                        // unhandled error
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
