using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace API.Services
{
    public interface IUsuarioService
    {
        Task<ResultadoOperacion> RegistrarUsuario(UsuarioRegistroDTO usuario);
        Task<ResultadoOperacion> ActualizarFoto(Guid idUsuario, ActualizarUrlUsuarioDTO url);
        Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario);
        Task<ResultadoOperacion> SuscribirseAutor(SuscribirseAutorDTO suscripcion);
        Task<ResultadoOperacion> EliminarSuscripcion(SuscribirseAutorDTO suscripcion);
        Task<ResultadoOperacion> ListadoDeUsuarios(OffsetLimitDTO parameters);
    }
}