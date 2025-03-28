using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace LibreriaVirtualData.Library.Data
{
    public class LibroData : ILibroData
    {
        private readonly LibreriaContext _context;
        private readonly MensajesService _mensajes;

        public LibroData(LibreriaContext context, MensajesService mensajes)
        {
            _context = context;
            _mensajes = mensajes;
        }
        public async Task<ResultadoOperacion> BuscarLibros(BuscarLibrosDTO queryDto)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();

            var query = _context.Libros.Include(l => l.Autor).AsQueryable();

            if (queryDto.AuthorId.HasValue)
            {
                query = query.Where(l => l.IdAutor == queryDto.AuthorId.Value);
            }
            if (!string.IsNullOrEmpty(queryDto.EditorialName))
            {
                query = query.Where(l => l.Editorial == queryDto.EditorialName);
            }
            if (queryDto.Before.HasValue)
            {
                query = query.Where(l => l.FechaPublicacion < queryDto.Before.Value);
            }
            if (queryDto.After.HasValue)
            {
                query = query.Where(l => l.FechaPublicacion > queryDto.After.Value);
            }
            if (queryDto.Sort.HasValue)
            {
                query = queryDto.Sort.Value
                    ? query.OrderBy(l => l.Calificacion ?? 0) : query.OrderByDescending(l => l.Calificacion ?? 0);
            }

            var libros = await query
                .Skip(queryDto.Offset)
                .Take(queryDto.Limit)
                .Select(l => new
                {
                    Titulo = l.Titulo,
                    NombreAutor = l.Autor.Nombre,
                    Editorial = l.Editorial,
                    ISBN = l.ISBN
                }).ToListAsync<object>();

            resultado.Exito = true;
            resultado.Data = libros;
            return resultado;

        }
    }
}
