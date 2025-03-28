using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriaVirtualData.Library.Data.Helpers
{
    public class DataHelper : IDataHelper
    {
        private readonly LibreriaContext _context;

        public DataHelper(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarUsuario(Guid idUsuario)
        {
            Usuario? usuario = await _context.Usuarios.Where(u => u.Id == idUsuario && u.Activo).FirstOrDefaultAsync();            
            return usuario;
        }

        public async Task<Autor> BuscarAutor(int idAutor)
        {
            Autor? autor = await _context.Autores.FindAsync(idAutor);            
            return autor;
        }

        public async Task<Suscripcion> BuscarSuscripcion(Guid idUsuario, int idAutor)
        {
            Suscripcion? suscripcion = await _context.Susripciones.Where(s => s.IdUsuario == idUsuario && s.IdAutor == idAutor).FirstOrDefaultAsync();
            return suscripcion;
        }

        public async Task<bool> YaExisteSuscripcion(Guid idUsuario, int idAutor)
        {
            Suscripcion? suscripcion = await _context.Susripciones.Where(s => s.IdUsuario == idUsuario && s.IdAutor == idAutor).FirstOrDefaultAsync();
            bool existe = suscripcion == null ? false : true;

            return existe;
        }
        
    }
}
