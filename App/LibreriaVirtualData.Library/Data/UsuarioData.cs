using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Models;
using LibreriaVirtualData.Library.Data.Helpers;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Shared.Library.Mensajes.Mensajes;
using Shared.Library.DTO;

namespace LibreriaVirtualData.Library.Data
{
    public class UsuarioData : IUsuarioData
    {
        private readonly LibreriaContext _context;
        private readonly IDataHelper _dataHelper;
        private readonly IMensajesService _mensajes;
        public UsuarioData(LibreriaContext context, 
            IDataHelper dataHelper, IMensajesService mensajes)
        {
            _context = context;
            _dataHelper = dataHelper;
            _mensajes = mensajes;
        }

        public async Task<ResultadoOperacion> RegistrarUsuario(UsuarioRegistroDTO usuario)
        {
            Usuario nuevoUsuario = new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                UrlFoto = usuario.UrlFoto
            };

            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario? us = await _context.Usuarios.Where(u => u.Id == usuario.Id).FirstOrDefaultAsync();
            if (us != null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioYaExiste,usuario.Id));
                resultado.StatusCode = HttpStatusCode.Conflict;
                return resultado;
            }
            await _context.Usuarios.AddAsync(nuevoUsuario);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> CambiarFotoUsuario(Guid idUsuario, ActualizarUrlUsuarioDTO url)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)
            {                
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioNoExiste,idUsuario));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }            
            usuario.UrlFoto = url.Url;
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }      

        public async Task<ResultadoOperacion> EliminarUsuario(Guid idUsuario)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)
            {                
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioNoExiste,idUsuario));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            usuario.Activo = false;
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> SuscribirseAutor(SuscribirseAutorDTO suscripcionDto)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario usuario = await _dataHelper.BuscarUsuario(suscripcionDto.userId);
            if (usuario == null)            {
                
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioNoExiste, suscripcionDto.userId));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            Autor autor = await _dataHelper.BuscarAutor(suscripcionDto.authorId);
            if (autor == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.AutorNoExiste, suscripcionDto.authorId));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            Suscripcion suscripcion = new Suscripcion
            {
                IdAutor = autor.Id,
                IdUsuario = usuario.Id
            };
            bool existe = await _dataHelper.YaExisteSuscripcion(suscripcionDto.userId, suscripcionDto.authorId);
            if (existe)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioYaSuscripto, [suscripcionDto.userId, suscripcionDto.authorId]));
                resultado.StatusCode = HttpStatusCode.Conflict;
                return resultado;
            }
            await _context.Susripciones.AddAsync(suscripcion);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> EliminarSuscripcion(SuscribirseAutorDTO suscripcionDto)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Suscripcion suscripcion = await _dataHelper.BuscarSuscripcion(suscripcionDto.userId, suscripcionDto.authorId);
            if (suscripcion == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.SuscripcionNoExiste, [suscripcionDto.userId, suscripcionDto.authorId]));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            _context.Susripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
            
        }

        public async Task<ResultadoOperacion> ListadoDeUsuarios(OffsetLimitDTO parameters)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            var usuarios = await _context.Usuarios
                .Include(u => u.Suscripciones)
                .OrderBy(u => u.FechaRegistro)
                .Skip(parameters.Offset)
                .Take(parameters.Limit)
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
