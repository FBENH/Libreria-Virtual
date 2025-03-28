﻿using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Shared.Library.DTO;

namespace API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioData _repositorioUsuarios;

        public UsuarioService(IUsuarioData repositorioUsuarios)
        {
            _repositorioUsuarios = repositorioUsuarios;
        }
        public async Task<ResultadoOperacion> RegistrarUsuario(UsuarioRegistroDTO usuario)
        {
            Usuario nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                UrlFoto = usuario.UrlFoto
            };

            ResultadoOperacion resultado = await _repositorioUsuarios.RegistrarUsuario(nuevoUsuario);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarFoto(Guid idUsuario, ActualizarUrlUsuarioDTO url)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.CambiarFotoUsuario(idUsuario, url);
            return resultado;
        }

        public async Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.EliminarUsuario(idUsuario);
            return resultado;
        }

        public async Task<ResultadoOperacion> SuscribirseAutor(SuscribirseAutorDTO suscripcion)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.SuscribirseAutor(suscripcion);
            return resultado;
        }

        public async Task<ResultadoOperacion> EliminarSuscripcion(SuscribirseAutorDTO suscripcion)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.EliminarSuscripcion(suscripcion);
            return resultado;
        }

        public async Task<ResultadoOperacion> ListadoDeUsuarios(OffsetLimitDTO parameters)
        {
            ResultadoOperacion resultado = await _repositorioUsuarios.ListadoDeUsuarios(parameters);
            return resultado;
        }
    }
}
