using API.Models.DTO;
using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/users/")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _servicioUsuarios;
        private readonly ManejarRespuestaDeErrorService _servicioRespuesta;
        private readonly MensajesService _mensajes;
        public UsuarioController(IUsuarioService servicioUsuarios, 
            ManejarRespuestaDeErrorService servicioRespuesta, MensajesService mensajes)
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
        public async Task<IActionResult> SuscribirseAutor([FromRoute] Guid userId, [FromRoute] int authorId)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.SuscribirseAutor(userId, authorId);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioSuscribir, [userId, authorId]);
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
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.EliminarSuscripcion(userId, authorId);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.UsuarioEliminarSuscripcion, [userId, authorId]);
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _servicioRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }            
        }

        [HttpGet]        
        public async Task<IActionResult> ListadoDeUsuarios([FromQuery, Required, Range(0,int.MaxValue)] int offset,
            [FromQuery, Required, Range(0, int.MaxValue)] int limit)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _servicioUsuarios.ListadoDeUsuarios(offset, limit);
            
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
