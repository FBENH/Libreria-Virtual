using API.Models.Respuesta;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Helpers
{
    public class ManejarRespuestaDeErrorService : IManejarRespuestaDeErrorService
    {
        public IActionResult ManejarRespuestaDeError(ResultadoOperacion resultado, Respuesta respuesta)
        {
            switch (resultado.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(respuesta);
                case HttpStatusCode.Conflict:
                    return new ConflictObjectResult(respuesta);
                default:
                    return new BadRequestObjectResult(respuesta);
            }
        }
    }
}
