using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/users/")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _servicioUsuarios;
        private readonly IManejarRespuestaDeErrorService _servicioRespuesta;
        private readonly IMensajesService _mensajes;
        public UsuarioController(IUsuarioService servicioUsuarios, 
            IManejarRespuestaDeErrorService servicioRespuesta, IMensajesService mensajes)
        {
            _servicioUsuarios = servicioUsuarios;
            _servicioRespuesta = servicioRespuesta;
            _mensajes = mensajes;
        }

        [HttpPost]        
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistroDTO usuario)
        {
            Respuesta respuesta = new Respuesta();                            
            ResultadoOperacion resultado = await _servicioUsuarios.RegistrarUsuario(usuario);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioRegistroExito);
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
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.ActualizarFoto(userId, url);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioFoto, userId);
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
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.EliminarUsuario(userId);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioEliminar, userId);
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
        public async Task<IActionResult> SuscribirseAutor([FromRoute] SuscribirseAutorDTO suscripcion)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.SuscribirseAutor(suscripcion);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioSuscribir, [suscripcion.userId, suscripcion.authorId]);
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
        public async Task<IActionResult> EliminarSuscripcion([FromRoute] SuscribirseAutorDTO suscripcion)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.EliminarSuscripcion(suscripcion);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioEliminarSuscripcion, [suscripcion.userId, suscripcion.authorId]);
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }            
        }

        [HttpGet]        
        public async Task<IActionResult> ListadoDeUsuarios([FromQuery] OffsetLimitDTO parameters)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.ListadoDeUsuarios(parameters);
            
            respuesta.exito = 1;
            respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioListado);
            respuesta.data = new
            {
                TotalDeUsuarios = resultado.Data?.Count(),
                Usuarios = resultado.Data
            };
            return Ok(respuesta);       
        }

    }
}
