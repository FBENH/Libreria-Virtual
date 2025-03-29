using LibreriaVirtualData.Library.Models;

namespace LibreriaVirtualData.Library.Data.Helpers
{
    public interface IDataHelper
    {
        Task<Usuario> BuscarUsuario(Guid idUsuario);
        Task<Autor> BuscarAutor(int idAutor);
        Task<Libro> BuscarLibro(int idLibro, bool includeReviews);
        Task<bool> YaExisteSuscripcion(Guid idUsuario, int idAutor);
        Task<Suscripcion> BuscarSuscripcion(Guid idUsuario, int idAutor);
        Task<bool> LibroExiste(int idLibro);
        Task<bool> UsuarioExiste(Guid idUsuario);
    }
}