
using API.Models.Respuesta;
using System.Net;

namespace API.Middlewares
{
    public class ManejadorExcepcionesMiddleware : IMiddleware
    {
		private readonly ILogger<ManejadorExcepcionesMiddleware> _logger;
        public ManejadorExcepcionesMiddleware(ILogger<ManejadorExcepcionesMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await next(context);
			}
			catch (Exception ex)
			{
                _logger.LogError(ex, ex.Message);
                await ManejarExcepcionAsync(context, ex);
			}
        }

        private async Task ManejarExcepcionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var respuesta = new Respuesta
            {
                mensaje = "Error en el servidor." //TODO traer mensaje desde configuracion
            };

            await context.Response.WriteAsJsonAsync(respuesta);
        }
    }
}
