using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Shared.Library.DTO;

namespace LibreriaVirtualData.Library.Data
{
    public interface IUsuarioData
    {
        Task<ResultadoOperacion> RegistrarUsuario(Usuario usuario);
        Task<ResultadoOperacion> CambiarFotoUsuario(Guid idUsuario, ActualizarUrlUsuarioDTO url);
        Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario);
        Task<ResultadoOperacion> SuscribirseAutor(SuscribirseAutorDTO suscripcion);
        Task<ResultadoOperacion> EliminarSuscripcion(SuscribirseAutorDTO suscripcion);
        Task<ResultadoOperacion> ListadoDeUsuarios(OffsetLimitDTO parameters);
    }
}