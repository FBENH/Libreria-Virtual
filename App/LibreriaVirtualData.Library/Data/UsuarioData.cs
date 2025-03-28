using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Models;
using LibreriaVirtualData.Library.Data.Helpers;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Shared.Library;

namespace LibreriaVirtualData.Library.Data
{
    public class UsuarioData : IUsuarioData
    {
        private readonly LibreriaContext _context;
        private readonly IDataHelper _dataHelper;
        private readonly MensajesService _mensajes;
        public UsuarioData(LibreriaContext context, 
            IDataHelper dataHelper, MensajesService mensajes)
        {
            _context = context;
            _dataHelper = dataHelper;
            _mensajes = mensajes;
        }

        public async Task<ResultadoOperacion> RegistrarUsuario(Usuario usuario)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario? us = await _context.Usuarios.Where(u => u.Id == usuario.Id).FirstOrDefaultAsync();
            if (us != null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioYaExiste,usuario.Id));
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
            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)
            {                
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioNoExiste,idUsuario));
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

        public async Task<ResultadoOperacion> SuscribirseAutor(Guid idUsuario, int idAutor)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Usuario usuario = await _dataHelper.BuscarUsuario(idUsuario);
            if (usuario == null)            {
                
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioNoExiste, idUsuario));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            Autor autor = await _dataHelper.BuscarAutor(idAutor);
            if (autor == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.AutorNoExiste, idAutor));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }
            Suscripcion suscripcion = new Suscripcion
            {
                IdAutor = autor.Id,
                IdUsuario = usuario.Id
            };
            bool existe = await _dataHelper.YaExisteSuscripcion(idUsuario, idAutor);
            if (existe)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioYaSuscripto, [idUsuario, idAutor]));
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
            ResultadoOperacion resultado = new ResultadoOperacion();
            Suscripcion suscripcion = await _dataHelper.BuscarSuscripcion(idUsuario, idAutor);
            if (suscripcion == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.SuscripcionNoExiste, [idUsuario, idAutor]));
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
            ResultadoOperacion resultado = new ResultadoOperacion();
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
