using API.Models.DTO;
using API.Models.Respuesta;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1.0/library/")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _servicioUsuarios;

        public UsuarioController(IUsuarioService servicioUsuarios)
        {
            _servicioUsuarios = servicioUsuarios;
        }


        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegistroDTO usuario)
        {
            var respuesta = new Respuesta();
                            
            await _servicioUsuarios.RegistrarUsuario(usuario);
            respuesta.exito = 1;
            respuesta.mensaje = "Se registró el usuario exitosamente."; //TODO traer mensajes de configuracion
            return Ok(respuesta);          
        }

        [HttpPatch]
        [Route("users/{userId}")]
        public async Task<IActionResult> CambiarUrlFoto([FromBody] ActualizarUrlUsuarioDTO url, [FromRoute] Guid userId)
        {
            var respuesta = new Respuesta();

            await _servicioUsuarios.ActualizarFoto(userId, url);
            respuesta.exito = 1;
            respuesta.mensaje = "Se actualizó la foto del usuario";//TODO traer mensajes de configuracion
            return Ok(respuesta);
        }

        [HttpDelete]
        [Route("users/{userId}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] Guid userId)
        {
            var respuesta = new Respuesta();

            await _servicioUsuarios.EliminarUsuario(userId);
            respuesta.exito = 1;
            respuesta.mensaje = "Se eliminó el usuario";//TODO traer mensajes de configuracion
            return Ok(respuesta);
        }
    }
}
