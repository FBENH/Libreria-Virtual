using API.Models.Respuesta;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
    public interface IManejarRespuestaDeErrorService
    {
        IActionResult ManejarRespuestaDeError(ResultadoOperacion resultado, Respuesta respuesta);
    }
}