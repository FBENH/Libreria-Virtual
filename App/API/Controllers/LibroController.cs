using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/books")]
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;
        private readonly MensajesService _mensajes;
        private readonly ManejarRespuestaDeErrorService _respuestaService;

        public LibroController(ILibroService libroService, 
            MensajesService mensajes, ManejarRespuestaDeErrorService respuestaService)
        {
            _libroService = libroService;
            _mensajes = mensajes;
            _respuestaService = respuestaService;
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

        [HttpPost]
        [Route("/api/v1.0/library/authors/{authorId}/books")]
        public async Task<IActionResult> IngresarLibro([FromRoute] int authorId, [FromBody] IngresarLibroDTO libro)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _libroService.IngresarLibro(authorId, libro);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.LibroIngresar);
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _respuestaService.ManejarRespuestaDeError(resultado, respuesta);
            }
        }
    }
}
