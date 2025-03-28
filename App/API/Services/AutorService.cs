﻿using API.Models.DTO;
using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;

namespace API.Services
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
            var nuevoAutor = new Autor
            {
                Nombre = autor.Nombre,
                Nacionalidad = autor.Nacionalidad,
                FechaNacimiento = autor.FechaNacimiento
            };

            var resultado = await _repositorioAutores.RegistrarAutor(nuevoAutor);
            return resultado;
        }

        public async Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor)
        {
            ResultadoOperacion resultado = await _repositorioAutores.ObtenerDetallesDeAutor(idAutor);
            return resultado;
        }
    }
}
