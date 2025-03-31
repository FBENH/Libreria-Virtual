using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Shared.Library.DTO;

namespace LibreriaVirtual.Logica.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorData _repositorioAutores;
        public AutorService(IAutorData repositorioAutores)
        {
            _repositorioAutores = repositorioAutores;
        }
        public async Task<ResultadoOperacion> RegistrarAutor(AutorRegistroDTO autor)
        {
            ResultadoOperacion resultado = await _repositorioAutores.RegistrarAutor(autor);
            return resultado;
        }

        public async Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor)
        {
            ResultadoOperacion resultado = await _repositorioAutores.ObtenerDetallesDeAutor(idAutor);
            return resultado;
        }
    }
}
