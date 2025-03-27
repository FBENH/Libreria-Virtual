using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Models;
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
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                UrlFoto = usuario.UrlFoto
            };

            try
            {
                await _context.Usuarios.AddAsync(nuevoUsuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
