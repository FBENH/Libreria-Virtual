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
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();          
        }
    }
}
