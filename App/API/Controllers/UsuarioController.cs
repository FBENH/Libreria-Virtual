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
    }
}
