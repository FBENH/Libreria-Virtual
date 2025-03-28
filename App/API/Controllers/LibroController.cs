using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/books/")]
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;
        private readonly MensajesService _mensajes;

        public LibroController(ILibroService libroService, MensajesService mensajes)
        {
            _libroService = libroService;
            _mensajes = mensajes;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarLibros([FromQuery] BuscarLibrosDTO query)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _libroService.BuscarLibros(query);

            respuesta.exito = 1;
            respuesta.mensaje = _mensajes.GetMensaje(Mensajes.LibrosListado);
            respuesta.data = resultado.Data;

            return Ok(respuesta);
        }
    }
}
