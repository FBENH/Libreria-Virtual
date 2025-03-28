using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/authors/")]
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;
        private readonly ManejarRespuestaDeErrorService _servicioRespuesta;
        private readonly MensajesService _mensajes;
        public AutorController(IAutorService autorService,
            ManejarRespuestaDeErrorService servicioRespuesta, MensajesService mensajes)
        {
            _autorService = autorService;
            _servicioRespuesta = servicioRespuesta;
            _mensajes = mensajes;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarAutor(AutorRegistroDTO autor)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _autorService.RegistrarAutor(autor);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.AutorRegistroExito);
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<IActionResult> ObtenerDetallesDeAutor([FromRoute] int authorId)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _autorService.ObtenerDetallesDeAutor(authorId);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.DetalleAutorExito, authorId);
                respuesta.data = resultado.Data.FirstOrDefault();
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }
        }
    }
}
