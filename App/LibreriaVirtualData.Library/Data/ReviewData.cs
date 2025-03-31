using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Data
{
    public class ReviewData : IReviewData
    {
        private readonly LibreriaContext _context;
        private readonly IMensajesService _mensajes;
        private readonly IDataHelper _dataHelper;

        public ReviewData(LibreriaContext context,
                          IMensajesService mensajes,
                          IDataHelper dataHelper)
        {
            _context = context;
            _mensajes = mensajes;
            _dataHelper = dataHelper;
        }

        public async Task<ResultadoOperacion> AgregarReview(int idLibro, Guid idUsuario, AgregarReviewDTO reviewDto)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();

            if (await YaExisteReviewDeUsuarioParElLibro(idUsuario, idLibro))
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.ReviewYaExiste, [idUsuario, idLibro]));
                resultado.StatusCode = HttpStatusCode.Conflict;
                return resultado;
            }

            Libro libro = await _dataHelper.BuscarLibro(idLibro, includeReviews: true);
            
            if (libro == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.LibroNoExiste, idLibro));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }

            if (!await _dataHelper.UsuarioExiste(idUsuario))
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.UsuarioNoExiste, idUsuario));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }

            Review review = new Review
            {
                Opinion = reviewDto.Opinion,
                Calificacion = reviewDto.Calificacion,
                IdUsuario = idUsuario,
                IdLibro = idLibro
            };

            using var transaction = await _context.Database.BeginTransactionAsync(); 
            try
            {
                
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();                

                libro.Calificacion = (decimal) await _context.Reviews
                                                             .Where(r => r.IdLibro == idLibro)
                                                             .AverageAsync(r => (int)r.Calificacion);

                _context.Entry(libro).OriginalValues["RowVersion"] = libro.RowVersion; // Optimistic locking
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                resultado.Exito = true;
                return resultado;
            }
            catch (DbUpdateConcurrencyException)
            {                
                await transaction.RollbackAsync();
                throw; // Lo maneja el middleware
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<ResultadoOperacion> BuscarReviews(int idLibro, BuscarReviewsDTO parametros)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            bool libroExiste = await _context.Libros.Where(l => l.Id == idLibro).AnyAsync();
            if (!libroExiste)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.LibroNoExiste, idLibro));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }

            var query = _context.Reviews.Where(r => r.IdLibro == idLibro).AsQueryable();

            if (parametros.ReviewType.HasValue)
            {
                query = query.Where(r => r.Calificacion == parametros.ReviewType.Value);
            }

            if (parametros.Sort.HasValue)
            {
                query = parametros.Sort.Value ? query.OrderBy(r => r.Fecha) : query.OrderByDescending(r => r.Fecha);
            }

            List<BuscarReviewsRespuestaDTO> reviews = await query
                .Skip(parametros.Offset).Take(parametros.Limit)
                .Select(r => new BuscarReviewsRespuestaDTO
                {
                    NombreLibro = r.Libro.Titulo,
                    NombreUsuario = r.Usuario.Nombre,
                    Opinion = r.Opinion,
                    Calificacion = r.Calificacion,
                    Fecha = r.Fecha
                })
                .ToListAsync();
            resultado.Exito = true;
            resultado.Data.Add(reviews);
            return resultado;
        }

        public async Task<bool> YaExisteReviewDeUsuarioParElLibro(Guid idUsuario, int idLibro)
        {
            bool existe = await _context.Reviews.Where(r => r.IdUsuario == idUsuario && r.IdLibro == idLibro).AnyAsync();
            return existe;
        }
    }
}
