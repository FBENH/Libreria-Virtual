using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Shared.Library.DTO;

namespace LibreriaVirtualData.Library.Data
{
    public interface IAutorData
    {
        Task<ResultadoOperacion> RegistrarAutor(AutorRegistroDTO autor);
        Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor);
    }
}