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
        public async Task BuscarReviews_DeberiaRetornarOk_CuandoEncuentraReviews()
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
    }
}
