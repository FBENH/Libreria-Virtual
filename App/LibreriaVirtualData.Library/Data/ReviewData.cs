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
        private readonly MensajesService _mensajes;

        public ReviewData(LibreriaContext context,
                          MensajesService mensajes)
        {
            _context = context;
            _mensajes = mensajes;
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

            List<Review> reviews = await query.Skip(parametros.Offset).Take(parametros.Limit).ToListAsync();
            resultado.Exito = true;
            resultado.Data.Add(reviews);
            return resultado;
        }
    }
}
