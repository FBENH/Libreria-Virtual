using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;

namespace LibreriaVirtualData.Library.Data
{
    public interface IAutorData
    {
        Task<ResultadoOperacion> RegistrarAutor(Autor autor);
        Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor);
    }
}