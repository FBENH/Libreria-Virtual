using API.Models.Respuesta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Library.Mensajes.Mensajes;

namespace API.Filters
{
    public class ValidacionModelStateFilter : IAsyncActionFilter
    {
        private readonly IMensajesService _mensajes;

        public ValidacionModelStateFilter(IMensajesService mensajes)
        {
            _mensajes = mensajes;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errores = context.ModelState.SelectMany(x => x.Value!.Errors)
                                                .Select(e => e.ErrorMessage).ToList();


                var respuesta = new Respuesta
                {
                    exito = 0,
                    mensaje = "Error de validación.",
                    data = errores
                };

                context.Result = new BadRequestObjectResult(respuesta);
                return;
            }
            
            await next();
        }
    }
}
