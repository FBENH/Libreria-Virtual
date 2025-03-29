using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;
using System.Net;

namespace LibreriaVirtualData.Library.Data
{
    public class LibroData : ILibroData
    {
        private readonly LibreriaContext _context;
        private readonly IMensajesService _mensajes;
        private readonly IDataHelper _dataHelper;

        public LibroData(LibreriaContext context, IMensajesService mensajes, IDataHelper dataHelper)
        {
            _context = context;
            _mensajes = mensajes;
            _dataHelper = dataHelper;
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

        public async Task<ResultadoOperacion> IngresarLibro(int idAutor, IngresarLibroDTO libroDto)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();

            Autor? a = await _context.Autores
                .Include(a => a.Suscripciones)
                .ThenInclude(s => s.Usuario)
                .Where(a => a.Id == idAutor).FirstOrDefaultAsync();
            if (a == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.AutorNoExiste, idAutor));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }

            Libro libro = new Libro
            {
                Titulo = libroDto.Titulo,
                Editorial = libroDto.Editorial,
                Paginas = libroDto.Paginas,
                FechaPublicacion = libroDto.FechaPublicacion,
                ISBN = libroDto.ISBN,
                Url = libroDto.Url,
                IdAutor = a.Id,                
            };

            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            resultado.Data.Add(a);
            return resultado;
        }
    }
}
