using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Exceptions;
using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Data
{
    public class UsuarioData : IUsuarioData
    {
        private readonly LibreriaContext _context;
        public UsuarioData(LibreriaContext context)
        {
            _context = context;
        }

        public async Task RegistrarUsuario(Usuario usuario)
        {            
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();          
        }

        public async Task CambiarFotoUsuario(Guid idUsuario, string url)
        {
            var usuario = await BuscarUsuario(idUsuario);
            usuario.UrlFoto = url;
            _context.SaveChanges();
        }

        private async Task<Usuario> BuscarUsuario(Guid idUsuario)
        {
            var usuario = await _context.Usuarios.Where(u => u.Id == idUsuario && u.Activo).FirstOrDefaultAsync();
            if (usuario == null)
            {
                throw new UsuarioNotFoundException(idUsuario.ToString());
            }
            return usuario;
        }

        public async Task EliminarUsuario(Guid idUsuario)
        {
            var usuario = await BuscarUsuario(idUsuario);
            usuario.Activo = false;
            _context.SaveChanges();
        }
    }
}
