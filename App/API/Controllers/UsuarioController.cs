using API.Models.DTO;
using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/users/")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _servicioUsuarios;
        private readonly ManejarRespuestaDeErrorService _servicioRespuesta;

        public UsuarioController(IUsuarioService servicioUsuarios, ManejarRespuestaDeErrorService servicioRespuesta)
        {
            _servicioUsuarios = servicioUsuarios;
            _servicioRespuesta = servicioRespuesta;
        }

        [HttpPost]        
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistroDTO usuario)
        {
            var respuesta = new Respuesta();
                            
            ResultadoOperacion resultado = await _servicioUsuarios.RegistrarUsuario(usuario);
            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = "Se registró el usuario exitosamente."; //TODO traer mensajes de configuracion
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }                   
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> CambiarUrlFoto([FromBody] ActualizarUrlUsuarioDTO url, [FromRoute] Guid userId)
        {
            var respuesta = new Respuesta();

            ResultadoOperacion resultado = await _servicioUsuarios.ActualizarFoto(userId, url);
            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = "Se actualizó la foto del usuario";//TODO traer mensajes de configuracion
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }            
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] Guid userId)
        {
            var respuesta = new Respuesta();

            var resultado = await _servicioUsuarios.EliminarUsuario(userId);
            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = "Se eliminó el usuario";//TODO traer mensajes de configuracion
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }
            
        }

        [HttpPost]
        [Route("{userId}/subscribe-to-author/{authorId}")]
        public async Task<IActionResult> SuscribirseAutor([FromRoute] Guid userId, [FromRoute] int authorId)
        {
            var respuesta = new Respuesta();

            var resultado = await _servicioUsuarios.SuscribirseAutor(userId, authorId);
            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = "Se suscribió al autor exitosamente";
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);                
            }            
        }        

        [HttpDelete]
        [Route("{userId}/subscribe-to-author/{authorId}")]
        public async Task<IActionResult> EliminarSuscripcion([FromRoute] Guid userId, [FromRoute] int authorId)
        {
            var respuesta = new Respuesta();
            var resultado = await _servicioUsuarios.EliminarSuscripcion(userId, authorId);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = "Se eliminó la suscripción exitosamente";
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
