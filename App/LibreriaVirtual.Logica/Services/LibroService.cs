using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Microsoft.Extensions.Configuration;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace LibreriaVirtual.Logica.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroData _repositorioLibros;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        private readonly IMensajesService _mensajes;

        public LibroService(ILibroData repositorioLibros,
                            IEmailSender emailSender,
                            IConfiguration config,
                            IMensajesService mensajes)
        {
            _repositorioLibros = repositorioLibros;
            _emailSender = emailSender;
            _config = config;
            _mensajes = mensajes;
        }
        public async Task<ResultadoOperacion> BuscarLibros(BuscarLibrosDTO query)
        {
            ResultadoOperacion resultado = await _repositorioLibros.BuscarLibros(query);            
            return resultado;
        }

        public async Task<ResultadoOperacion> IngresarLibro(int idAutor, IngresarLibroDTO libro)
        {
            ResultadoOperacion resultado = await _repositorioLibros.IngresarLibro(idAutor, libro);
            
            string? fromAddress = _config.GetSection("EmailConfig:FromAddress").Value;
            string? subject = _config.GetSection("EmailConfig:Subject").Value;

            if (resultado.Data.Any())
            {
                Autor autor = (Autor)resultado.Data.First();
                
                var suscriptores = autor.Suscripciones.Select(s => s.Usuario).ToList();

                foreach (var suscriptor in suscriptores)
                {
                    string textMessage = _mensajes.GetMensaje(Mensajes.EmailBody, [suscriptor.Nombre, autor.Nombre, libro.Titulo, libro.Url]);
                    
                    await _emailSender.SendEmailAsync(fromAddress, suscriptor.Email, subject, textMessage);
                }
            }

            resultado.Data.Clear();

            return resultado;
        }
    }
}
