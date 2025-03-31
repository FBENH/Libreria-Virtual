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
    public class AutorControllerTests
    {
        private readonly Mock<IAutorService> _autorServiceMock;
        private readonly Mock<IManejarRespuestaDeErrorService> _servicioRespuestaMock;
        private readonly Mock<IMensajesService> _mensajesMock;
        private readonly AutorController _controller;

        public AutorControllerTests()
        {
            _autorServiceMock = new Mock<IAutorService>();
            _servicioRespuestaMock = new Mock<IManejarRespuestaDeErrorService>();
            _mensajesMock = new Mock<IMensajesService>();
            _controller = new AutorController(_autorServiceMock.Object, _servicioRespuestaMock.Object, _mensajesMock.Object);
        }

        [Fact]
        public async Task AutorController_RegistrarAutor_ReturnOkEnExito()
        {
            // Arrange
            var autorDTO = new AutorRegistroDTO();
            var resultado = new ResultadoOperacion { Exito = true };

            _autorServiceMock.Setup(s => s.RegistrarAutor(autorDTO)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.AutorRegistroExito)).Returns("Se registró el autor exitosamente.");

            // Act
            var resultadoAccion = await _controller.RegistrarAutor(autorDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal("Se registró el autor exitosamente.", respuesta.mensaje);
        }        

        [Fact]
        public async Task AutorController_ObtenerDetallesDeAutor_ReturnOkCuandoSeEncuentra()
        {
            // Arrange
            int authorId = 1;
            var resultado = new ResultadoOperacion { Exito = true, Data = new List<object> { new { Id = authorId, Nombre = "Autor de prueba" } } };

            _autorServiceMock.Setup(s => s.ObtenerDetallesDeAutor(authorId)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.DetalleAutorExito, authorId)).Returns($"Se obtuvieron los detalles del autor con id {authorId}");

            // Act
            var resultadoAccion = await _controller.ObtenerDetallesDeAutor(authorId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal($"Se obtuvieron los detalles del autor con id {authorId}", respuesta.mensaje);
            Assert.NotNull(respuesta.data);
        }

        [Fact]
        public async Task AutorController_ObtenerDetallesDeAutor_ReturnNotFoundCuandoNoSeEncuentra()
        {
            // Arrange
            int authorId = 1;
            var resultado = new ResultadoOperacion { Exito = false, Errores = new List<string> { $"No existe un autor con el id {authorId}." } };

            _autorServiceMock.Setup(s => s.ObtenerDetallesDeAutor(authorId)).ReturnsAsync(resultado);
            _servicioRespuestaMock
                .Setup(s => s.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>()))
                .Returns(new NotFoundObjectResult(new Respuesta { mensaje = $"No existe un autor con el id {authorId}." }));

            // Act
            var resultadoAccion = await _controller.ObtenerDetallesDeAutor(authorId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal($"No existe un autor con el id {authorId}.", respuesta.mensaje);
            Assert.Equal(0, respuesta.exito);
        }

    }
}
