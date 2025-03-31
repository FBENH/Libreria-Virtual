using API.Middlewares;
using API.Models.Respuesta;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Library.Mensajes.Mensajes;
using System.Text.Json;

namespace LibreriaVirtual.Tests.Middlewares
{
    public class ManejadorExcepcionesMiddlewareTests
    {
        private readonly Mock<ILogger<ManejadorExcepcionesMiddleware>> _loggerMock;
        private readonly Mock<IMensajesService> _mensajesMock;
        private readonly ManejadorExcepcionesMiddleware _middleware;
        

        public ManejadorExcepcionesMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<ManejadorExcepcionesMiddleware>>();
            _mensajesMock = new Mock<IMensajesService>();            
            _middleware = new ManejadorExcepcionesMiddleware(_loggerMock.Object, _mensajesMock.Object);
        }

        [Fact]
        public async Task InvokeAsync_DeberiaRetornar500_CuandoSeLanzaUnaExcepcionGenerica()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.InternalError)).Returns("Error en el servidor.");

            var next = new RequestDelegate(ctx => throw new Exception("Error inesperado"));

            // Act
            await _middleware.InvokeAsync(context, next);

            // Resetear la posición del stream para poder leerlo
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var respuesta = JsonSerializer.Deserialize<Respuesta>(responseBody);

            // Assert
            Assert.Equal(500, context.Response.StatusCode);
            Assert.NotNull(respuesta);
            Assert.IsType<Respuesta>(respuesta);
            Assert.Equal("Error en el servidor.", respuesta.mensaje);
        }

        [Fact]
        public async Task InvokeAsync_DeberiaRetornar409_CuandoSeLanzaDbUpdateConcurrencyException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.ErrorConcurrencia)).Returns("Ocurrió un error de concurrencia.");

            var next = new RequestDelegate(ctx => throw new DbUpdateConcurrencyException("Conflicto de concurrencia"));

            // Act
            await _middleware.InvokeAsync(context, next);

            // Resetear la posición del stream para poder leerlo
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var respuesta = JsonSerializer.Deserialize<Respuesta>(responseBody);

            // Assert
            Assert.Equal(409, context.Response.StatusCode);
            Assert.NotNull(respuesta);
            Assert.IsType<Respuesta>(respuesta);
            Assert.Equal("Ocurrió un error de concurrencia.", respuesta.mensaje);
        }
    }
}
