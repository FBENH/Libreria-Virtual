using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;

namespace LibreriaVirtualData.Library.Data
{
    public interface IUsuarioData
    {
        Task<ResultadoOperacion> RegistrarUsuario(Usuario usuario);
        Task<ResultadoOperacion> CambiarFotoUsuario(Guid idUsuario, string url);
        Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario);
        Task<ResultadoOperacion> SuscribirseAutor(Guid idUsuario, int idAutor);
        Task<ResultadoOperacion> EliminarSuscripcion(Guid idUsuario, int idAutor);
        Task<ResultadoOperacion> ListadoDeUsuarios(int offset, int limit);
    }
}