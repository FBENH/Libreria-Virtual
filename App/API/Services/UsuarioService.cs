using API.Models.DTO;
using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
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
        public async Task<ResultadoOperacion> RegistrarUsuario(UsuarioRegistroDTO usuario)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                UrlFoto = usuario.UrlFoto
            };

            ResultadoOperacion resultado = await _repositorioUsuarios.RegistrarUsuario(nuevoUsuario);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarFoto(Guid idUsuario, ActualizarUrlUsuarioDTO url)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.CambiarFotoUsuario(idUsuario, url.Url);
            return resultado;
        }

        public async Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.EliminarUsuario(idUsuario);
            return resultado;
        }

        public async Task<ResultadoOperacion> SuscribirseAutor(Guid idUsuario, int idAutor)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.SuscribirseAutor(idUsuario, idAutor);
            return resultado;
        }

        public async Task<ResultadoOperacion> EliminarSuscripcion(Guid idUsuario, int idAutor)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.EliminarSuscripcion(idUsuario, idAutor);
            return resultado;
        }

        public async Task<ResultadoOperacion> ListadoDeUsuarios(int offset, int limit)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.ListadoDeUsuarios(offset, limit);
            return resultado;
        }
    }
}
