using API.Controllers;
using API.Helpers;
using API.Models.Respuesta;
using LibreriaVirtual.Logica.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Library.DTO;
using Shared.Library.Enums;
using Shared.Library.Mensajes.Mensajes;
using System.Net;

namespace LibreriaVirtual.Tests.Controllers
{
    public class ReviewControllerTests
    {
        private readonly Mock<IReviewService> _reviewServiceMock;
        private readonly Mock<IMensajesService> _mensajesMock;
        private readonly Mock<IManejarRespuestaDeErrorService> _manejarRespuestaMock;
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _reviewServiceMock = new Mock<IReviewService>();
            _mensajesMock = new Mock<IMensajesService>();
            _manejarRespuestaMock = new Mock<IManejarRespuestaDeErrorService>();
            _controller = new ReviewController(_reviewServiceMock.Object, _mensajesMock.Object, _manejarRespuestaMock.Object);
        }

        [Fact]
        public async Task ReviewController_BuscarReviews_DeberiaRetornarOk_CuandoEncuentraReviews()
        {
            // Arrange
            int bookId = 1;
            var parametros = new BuscarReviewsDTO { Limit = 1, ReviewType = Calificacion.Excelente };
            var resultado = new ResultadoOperacion { Exito = true, Data = new List<object> { 
                new BuscarReviewsRespuestaDTO { Opinion = "Buen libro", Calificacion = Calificacion.Excelente},                
            }};

            _reviewServiceMock.Setup(s => s.BuscarReviews(bookId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.ReviewsListado)).Returns("Éxito al obtener el listado de Reviews.");

            // Act
            var resultadoAccion = await _controller.BuscarReviews(bookId, parametros);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal("Éxito al obtener el listado de Reviews.", respuesta.mensaje);
            Assert.NotNull(respuesta.data);
            var dataList = Assert.IsAssignableFrom<List<object>>(respuesta.data);
            Assert.Single(dataList);
            Assert.IsType<BuscarReviewsRespuestaDTO>(dataList[0]);
        }

        [Fact]
        public async Task ReviewController_BuscarReviews_DeberiaRetornarNotFound_CuandoElLibroNoExiste()
        {
            // Arrange
            int bookId = 999;
            var parametros = new BuscarReviewsDTO { Limit = 1, ReviewType = Calificacion.Excelente };
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };
            string mensajeError = "No se encontró el libro con id 999.";
            resultado.Errores.Add(mensajeError);
            
            _reviewServiceMock.Setup(s => s.BuscarReviews(bookId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.LibroNoExiste)).Returns(mensajeError);
            
            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensajeError
            });
            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.BuscarReviews(bookId, parametros);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensajeError, respuesta.mensaje);
        }

        [Fact]
        public async Task ReviewController_AgregarReview_DeberiaRetornarConflict_CuandoYaExisteReviewDeEseUsuarioParaEseLibro()
        {
            // Arrange
            int bookId = 1;
            Guid userId = Guid.NewGuid();
            var parametros = new AgregarReviewDTO 
            {
                Opinion = "Una opinión",
                Calificacion = Calificacion.Regular
            };
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.Conflict
            };
            string mensajeError = $"Ya existe una review del usuario con id {userId} para el libro con id {bookId}.";
            resultado.Errores.Add(mensajeError);

            _reviewServiceMock.Setup(s => s.AgregarReview(bookId, userId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.ReviewYaExiste)).Returns(mensajeError);

            var conflictObject = new ConflictObjectResult(new Respuesta
            {
                mensaje = mensajeError
            });
            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(conflictObject);

            // Act
            var resultadoAccion = await _controller.AgregarReview(bookId, userId, parametros);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(conflictResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensajeError, respuesta.mensaje);
        }

        [Fact]
        public async Task ReviewController_AgregarReview_DeberiaRetornarNotFound_CuandoElLibroNoExiste()
        {
            // Arrange
            int bookId = 999;
            Guid userId = Guid.NewGuid();
            var parametros = new AgregarReviewDTO 
            {
                Opinion = "Una opinion",
                Calificacion = Calificacion.Malo
            };

            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };
            string mensajeError = "No se encontró el libro con id 999.";
            resultado.Errores.Add(mensajeError);

            _reviewServiceMock.Setup(s => s.AgregarReview(bookId, userId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.LibroNoExiste)).Returns(mensajeError);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensajeError
            });
            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.AgregarReview(bookId, userId, parametros);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensajeError, respuesta.mensaje);
        }

        [Fact]
        public async Task ReviewController_AgregarReview_DeberiaRetornarNotFound_CuandoElUsuarioNoExiste()
        {
            // Arrange
            int bookId = 1;
            Guid userId = Guid.NewGuid();
            var parametros = new AgregarReviewDTO 
            {
                Opinion = "Una opinion",
                Calificacion = Calificacion.Excelente
            };
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };
            string mensajeError = $"No existe el usuario con el id {userId}.";
            resultado.Errores.Add(mensajeError);

            _reviewServiceMock.Setup(s => s.AgregarReview(bookId, userId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioNoExiste)).Returns(mensajeError);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensajeError
            });
            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.AgregarReview(bookId, userId, parametros);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensajeError, respuesta.mensaje);
        }
    }
}
