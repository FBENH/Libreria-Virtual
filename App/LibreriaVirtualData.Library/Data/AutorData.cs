using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;
using System.Net;

namespace LibreriaVirtualData.Library.Data
{
    public class AutorData : IAutorData
    {
        private readonly LibreriaContext _context;
        private readonly IDataHelper _dataHelper;
        private readonly IMensajesService _mensajes;
        public AutorData(LibreriaContext context, 
            IDataHelper dataHelper, IMensajesService mensajes)
        {
            _context = context;
            _dataHelper = dataHelper;
            _mensajes = mensajes;
        }

        public async Task<ResultadoOperacion> RegistrarAutor(AutorRegistroDTO autor)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            Autor nuevoAutor = new Autor
            {
                Nombre = autor.Nombre,
                Nacionalidad = autor.Nacionalidad,
                FechaNacimiento = autor.FechaNacimiento
            };

            await _context.Autores.AddAsync(nuevoAutor);
            await _context.SaveChangesAsync();
            resultado.Exito = true;
            return resultado;
        }

        public async Task<ResultadoOperacion> ObtenerDetallesDeAutor(int idAutor)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            var a = await _context.Autores.Where(a => a.Id == idAutor)
                    .Select(a => new
                    {
                        Nombre = a.Nombre,
                        Nacionalidad = a.Nacionalidad,
                        FechaNacimiento = a.FechaNacimiento,
                        CantidadSuscritos = a.Suscripciones.Count(),
                        Libros = a.Libros
                    }).FirstOrDefaultAsync();                

            if (a == null)
            {
                resultado.Errores.Add(_mensajes.GetMensaje(Mensajes.AutorNoExiste, idAutor));
                resultado.StatusCode = HttpStatusCode.NotFound;
                return resultado;
            }

            resultado.Exito = true;
            resultado.Data.Add(a);
            return resultado;
        }
    }
}
