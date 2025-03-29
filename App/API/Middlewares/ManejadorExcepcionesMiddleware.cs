using API.Models.Respuesta;
using Microsoft.EntityFrameworkCore;
using Shared.Library.Mensajes.Mensajes;
using System.Net;

namespace API.Middlewares
{
    public class ManejadorExcepcionesMiddleware : IMiddleware
    {
		private readonly ILogger<ManejadorExcepcionesMiddleware> _logger;
        private readonly IMensajesService _mensajes;

        public ManejadorExcepcionesMiddleware(ILogger<ManejadorExcepcionesMiddleware> logger, IMensajesService mensajes)
        {
            _logger = logger;
            _mensajes = mensajes;
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
            int statusCode;
            string mensaje;

            switch (exception)
            {
                case DbUpdateConcurrencyException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    mensaje = _mensajes.GetMensaje(Mensajes.ErrorConcurrencia);
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    mensaje = _mensajes.GetMensaje(Mensajes.InternalError);
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            Respuesta respuesta = new Respuesta
            {
                mensaje = mensaje
            };

            await context.Response.WriteAsJsonAsync(respuesta);
        }
    }
}
