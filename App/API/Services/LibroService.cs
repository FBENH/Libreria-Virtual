using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using LibreriaVirtualData.Library.Models;
using Shared.Library.DTO;

namespace API.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroData _repositorioLibros;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public LibroService(ILibroData repositorioLibros,
                            IEmailSender emailSender,
                            IConfiguration config)
        {
            _repositorioLibros = repositorioLibros;
            _emailSender = emailSender;
            _config = config;
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
                    string textMessage = $"Hola {suscriptor.Nombre},\n\n" +
                                         $"Tenemos el placer de informarte que el autor {autor.Nombre} ha publicado un nuevo libro titulado \"{libro.Titulo}\". " +
                                         "Este libro podría ser de tu interés.\n\n" +
                                         $"Para más información, visita el siguiente enlace: {libro.Url}\n\n" +
                                         "¡Esperamos que disfrutes de la lectura!\n\n";
                    
                    _emailSender.SendEmailAsync(fromAddress, suscriptor.Email, subject, textMessage);
                }
            }

            return resultado;
        }
    }
}
