using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Models;
using LibreriaVirtualData.Library.Data.Helpers;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace LibreriaVirtualData.Library.Data
{
    public class UsuarioData : IUsuarioData
    {
        private readonly LibreriaContext _context;
        private readonly IDataHelper _dataHelper;
        public UsuarioData(LibreriaContext context, IDataHelper dataHelper)
        {
            _context = context;
            _dataHelper = dataHelper;
        }

        public async Task<ResultadoOperacion> RegistrarUsuario(Usuario usuario)
        {
            var resultado = new ResultadoOperacion();
            var us = _context.Usuarios.Where(u => u.Id == usuario.Id);
            if (us != null)
            {
                resultado.Errores.Add($"Ya existe un usuario con el id {usuario.Id}");
                resultado.StatusCode = HttpStatusCode.Conflict;
                return resultado;
            }
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> CambiarFotoUsuario(Guid idUsuario, string url)
        {
            var resultado = new ResultadoOperacion();
            var usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)
            {                
                resultado.Errores.Add($"No se encontró un usuario con id {idUsuario}");
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }            
            usuario.UrlFoto = url;
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }      

        public async Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario)
        {
            var resultado = new ResultadoOperacion();
            var usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)
            {                
                resultado.Errores.Add($"No se encontró el usuario con id {idUsuario}");
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            usuario.Activo = false;
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> SuscribirseAutor(Guid idUsuario, int idAutor)
        {
            var resultado = new ResultadoOperacion();
            var usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)            {
                
                resultado.Errores.Add($"No se encontró el usuario con id {idUsuario}");
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            var autor = await _dataHelper.BuscarAutor(idAutor);
            if (autor == null)
            {
                resultado.Errores.Add($"No se encontró el autor con id {idAutor}");
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            var suscripcion = new Suscripcion
            {
                IdAutor = autor.Id,
                IdUsuario = usuario.Id
            };
            bool existe = await _dataHelper.YaExisteSuscripcion(idUsuario, idAutor);
            if (existe)
            {
                resultado.Errores.Add($"El usuario con id {idUsuario} ya esta suscripto al autor con id {idAutor}");
                resultado.StatusCode = HttpStatusCode.Conflict;
                return resultado;
            }
            await _context.Susripciones.AddAsync(suscripcion);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> EliminarSuscripcion(Guid idUsuario, int idAutor)
        {
            var resultado = new ResultadoOperacion();
            var suscripcion = await _dataHelper.BuscarSuscripcion(idUsuario, idAutor);
            if (suscripcion == null)
            {
                resultado.Errores.Add($"No se encontró una suscripción de un usuario con id {idUsuario} a un autor con id {idAutor}");
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            _context.Susripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
            
        }

        public async Task<ResultadoOperacion> ListadoDeUsuarios(int offset, int limit)
        {
            var resultado = new ResultadoOperacion();
            var usuarios = await _context.Usuarios
                .Include(u => u.Suscripciones)
                .OrderBy(u => u.FechaRegistro)
                .Skip(offset)
                .Take(limit)
                .Select(u => new
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Imagen = u.UrlFoto,
                    FechaRegistro = u.FechaRegistro,
                    CantidadAutoresSuscritos = u.Suscripciones.Count
                })
                .ToListAsync<object>();
            resultado.Exito = true;
            resultado.Data = usuarios;
            return resultado;
        }
    }
}
