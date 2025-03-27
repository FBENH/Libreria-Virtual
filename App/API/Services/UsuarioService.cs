using API.Models.DTO;
using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Models;

namespace API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioData _repositorioUsuarios;

        public UsuarioService(IUsuarioData repositorioUsuarios)
        {
            _repositorioUsuarios = repositorioUsuarios;
        }
        public async Task RegistrarUsuario(UsuarioRegistroDTO usuario)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                UrlFoto = usuario.UrlFoto
            };

            await _repositorioUsuarios.RegistrarUsuario(nuevoUsuario);
        }

        public async Task ActualizarFoto(Guid idUsuario, ActualizarUrlUsuarioDTO url)
        {
            await _repositorioUsuarios.CambiarFotoUsuario(idUsuario, url.Url);
        }

        public async Task EliminarUsuario(Guid idUsuario)
        {
            await _repositorioUsuarios.EliminarUsuario(idUsuario);
        }
    }
}
