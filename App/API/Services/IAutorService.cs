using API.Models.DTO;
using LibreriaVirtualData.Library.Data.Helpers;

namespace API.Services
{
    public interface IAutorService
    {
        Task<ResultadoOperacion> RegistrarAutor(AutorRegistroDTO autor);
        Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor);
    }
}