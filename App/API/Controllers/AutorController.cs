using API.Models.DTO;
using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/authors/")]
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;
        private readonly ManejarRespuestaDeErrorService _servicioRespuesta;
        public AutorController(IAutorService autorService, 
            ManejarRespuestaDeErrorService servicioRespuesta)
        {
            _autorService = autorService;
            _servicioRespuesta = servicioRespuesta;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarAutor(AutorRegistroDTO autor)
        {
            var respuesta = new Respuesta();
            var resultado = await _autorService.RegistrarAutor(autor);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = "Se registro el autor exitosamente.";
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
                respuesta.mensaje = "Se obtuvieron los detalles del autor";
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
