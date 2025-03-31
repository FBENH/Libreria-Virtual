using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace LibreriaVirtual.Logica.Services
{
    public interface IAutorService
    {
        Task<ResultadoOperacion> RegistrarAutor(AutorRegistroDTO autor);
        Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor);
    }
}