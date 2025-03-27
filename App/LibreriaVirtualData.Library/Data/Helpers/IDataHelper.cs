using LibreriaVirtualData.Library.Models;

namespace LibreriaVirtualData.Library.Data.Helpers
{
    public interface IDataHelper
    {
        Task<Usuario> BuscarUsuario(Guid idUsuario);
        Task<Autor> BuscarAutor(int idAutor);
        Task<bool> YaExisteSuscripcion(Guid idUsuario, int idAutor);
        Task<Suscripcion> BuscarSuscripcion(Guid idUsuario, int idAutor);
    }
}