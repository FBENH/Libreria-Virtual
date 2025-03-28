using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace API.Services
{
    public interface IUsuarioService
    {
        Task<ResultadoOperacion> RegistrarUsuario(UsuarioRegistroDTO usuario);
        Task<ResultadoOperacion> ActualizarFoto(Guid idUsuario, ActualizarUrlUsuarioDTO url);
        Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario);
        Task<ResultadoOperacion> SuscribirseAutor(Guid idUsuario, int idAutor);
        Task<ResultadoOperacion> EliminarSuscripcion(Guid idUsuario, int idAutor);
        Task<ResultadoOperacion> ListadoDeUsuarios(int offset, int limit);
    }
}