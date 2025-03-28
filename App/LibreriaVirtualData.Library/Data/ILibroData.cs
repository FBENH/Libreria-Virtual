using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace LibreriaVirtualData.Library.Data
{
    public interface ILibroData
    {
        Task<ResultadoOperacion> BuscarLibros(BuscarLibrosDTO queryDto);
    }
}