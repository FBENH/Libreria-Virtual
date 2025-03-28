using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace API.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroData _repositorioLibros;

        public LibroService(ILibroData repositorioLibros)
        {
            _repositorioLibros = repositorioLibros;
        }
        public async Task<ResultadoOperacion> BuscarLibros(BuscarLibrosDTO query)
        {
            ResultadoOperacion resultado = await _repositorioLibros.BuscarLibros(query);            
            return resultado;
        }
    }
}
