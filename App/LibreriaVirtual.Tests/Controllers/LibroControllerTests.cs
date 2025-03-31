using API.Controllers;
using API.Helpers;
using API.Models.Respuesta;
using LibreriaVirtual.Logica.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace LibreriaVirtual.Tests.Controllers
{
    public class LibroControllerTests
    {
        private readonly Mock<ILibroService> _libroServiceMock;
        private readonly Mock<IMensajesService> _mensajesMock;
        private readonly Mock<IManejarRespuestaDeErrorService> _respuestaServiceMock;
        private readonly LibroController _controller;

        public LibroControllerTests()
        {
            _libroServiceMock = new Mock<ILibroService>();
            _mensajesMock = new Mock<IMensajesService>();
            _respuestaServiceMock = new Mock<IManejarRespuestaDeErrorService>();
            _controller = new LibroController(_libroServiceMock.Object, _mensajesMock.Object, _respuestaServiceMock.Object);
        }

        [Fact]
        public async Task LibroController_BuscarLibros_ReturnOkCuandoEncuentraLibros()
        {
            // Arrange
            var query = new BuscarLibrosDTO();
            var resultado = new ResultadoOperacion { Exito = true, Data = new List<object> { new BuscarLibrosRespuestaDTO  
            {
                Titulo = "Libro de prueba",
                NombreAutor = "Autor de prueba",
                Editorial = "EditorialABC",
                ISBN = "ISBN de prueba"
            } 
            }};

            _libroServiceMock.Setup(s => s.BuscarLibros(query)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.LibrosListado)).Returns("Éxito al obtener el listado de libros.");

            // Act
            var resultadoAccion = await _controller.BuscarLibros(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okResult.Value);
            var objetoEnData = Assert.IsType<List<object>>(respuesta.data).FirstOrDefault();
            Assert.IsType<BuscarLibrosRespuestaDTO>(objetoEnData);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal("Éxito al obtener el listado de libros.", respuesta.mensaje);
            Assert.NotNull(respuesta.data);
        }
    }
}
