using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace API.Services
{
    public interface IAutorService
    {
        Task<ResultadoOperacion> RegistrarAutor(AutorRegistroDTO autor);
        Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor);
    }
}