using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace LibreriaVirtual.Logica.Services
{
    public interface ILibroService
    {
        Task<ResultadoOperacion> BuscarLibros(BuscarLibrosDTO query);
        Task<ResultadoOperacion> IngresarLibro(int idAutor, IngresarLibroDTO libro);
    }
}