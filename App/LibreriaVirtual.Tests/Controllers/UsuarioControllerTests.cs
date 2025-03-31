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
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly Mock<IMensajesService> _mensajesMock;
        private readonly Mock<IManejarRespuestaDeErrorService> _manejarRespuestaMock;
        private readonly UsuarioController _controller;
        public UsuarioControllerTests()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _mensajesMock = new Mock<IMensajesService>();
            _manejarRespuestaMock = new Mock<IManejarRespuestaDeErrorService>();
            _controller = new UsuarioController(_usuarioServiceMock.Object, _manejarRespuestaMock.Object, _mensajesMock.Object);
        }

        [Fact]
        public async Task UsuarioController_RegistrarUsuario_DeberiaRetornarOk_CuandoSeRegistra()
        {
            // Arrange            
            var parametros = new UsuarioRegistroDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Nombre",
                Email = "mail@mail.com",
                UrlFoto = ""
            };
            string mensaje = "Se registró el usuario exitosamente.";
            var resultado = new ResultadoOperacion
            {
                Exito = true                
            };            

            _usuarioServiceMock.Setup(s => s.RegistrarUsuario(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioRegistroExito)).Returns(mensaje);                    

            // Act
            var resultadoAccion = await _controller.RegistrarUsuario(parametros);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_RegistrarUsuario_DeberiaRetornarConflict_CuandoIdYaExiste()
        {
            // Arrange            
            var parametros = new UsuarioRegistroDTO
            {
                Id = Guid.NewGuid(),
                Nombre = "Nombre",
                Email = "mail@mail.com",
                UrlFoto = ""
            };
            string mensaje = $"Ya existe un usuario con el id {parametros.Id}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.Conflict
            };

            _usuarioServiceMock.Setup(s => s.RegistrarUsuario(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioRegistroExito)).Returns(mensaje);

            var conflictObject = new ConflictObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(conflictObject);

            // Act
            var resultadoAccion = await _controller.RegistrarUsuario(parametros);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(conflictResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_CambiarUrlFoto_DeberiaRetornarNotFound_CuandoUsuarioNoExiste()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var parametros = new ActualizarUrlUsuarioDTO
            {
                Url = "123"
            };
            string mensaje = $"No existe el usuario con el id {userId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };

            _usuarioServiceMock.Setup(s => s.ActualizarFoto(userId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioNoExiste)).Returns(mensaje);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.CambiarUrlFoto(parametros, userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_CambiarUrlFoto_DeberiaRetornarOk_EnExito()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var parametros = new ActualizarUrlUsuarioDTO
            {
                Url = "123"
            };
            string mensaje = $"Se actualizó la foto del usuario con id {userId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = true
            };

            _usuarioServiceMock.Setup(s => s.ActualizarFoto(userId, parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioFoto, userId)).Returns(mensaje);                     

            // Act
            var resultadoAccion = await _controller.CambiarUrlFoto(parametros, userId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okObjectResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_EliminarUsuario_DeberiaRetornarOk_EnExito()
        {
            // Arrange
            Guid userId = Guid.NewGuid();            
            string mensaje = $"Se eliminó el usuario con id {userId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = true
            };

            _usuarioServiceMock.Setup(s => s.EliminarUsuario(userId)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioEliminar, userId)).Returns(mensaje);

            // Act
            var resultadoAccion = await _controller.EliminarUsuario(userId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okObjectResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_EliminarUsuario_DeberiaRetornarNotFound_CuandoUsuarioNoExiste()
        {
            // Arrange
            Guid userId = Guid.NewGuid();            
            string mensaje = $"No existe el usuario con el id {userId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };

            _usuarioServiceMock.Setup(s => s.EliminarUsuario(userId)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioNoExiste)).Returns(mensaje);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.EliminarUsuario(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_SuscribirseAutor_DeberiaRetornarOk_EnExito()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int authorId = 1;
            var parametros = new SuscribirseAutorDTO
            {
                userId = userId,
                authorId = authorId
            };
            string mensaje = $"Se suscribió el usuario con id {userId} al autor de id {authorId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = true
            };

            _usuarioServiceMock.Setup(s => s.SuscribirseAutor(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioSuscribir, userId,authorId)).Returns(mensaje);

            // Act
            var resultadoAccion = await _controller.SuscribirseAutor(parametros);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okObjectResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_SuscribirseAutor_DeberiaRetornarNotFound_CuandoUsuarioNoExiste()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int authorId = 1;
            var parametros = new SuscribirseAutorDTO
            {
                userId = userId,
                authorId = authorId
            };
            string mensaje = $"No existe el usuario con el id {userId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };

            _usuarioServiceMock.Setup(s => s.SuscribirseAutor(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioNoExiste, userId)).Returns(mensaje);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.SuscribirseAutor(parametros);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_SuscribirseAutor_DeberiaRetornarNotFound_CuandoAutorNoExiste()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int authorId = 1;
            var parametros = new SuscribirseAutorDTO
            {
                userId = userId,
                authorId = authorId
            };
            string mensaje = $"No existe un autor con el id {authorId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };

            _usuarioServiceMock.Setup(s => s.SuscribirseAutor(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.AutorNoExiste, authorId)).Returns(mensaje);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.SuscribirseAutor(parametros);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_SuscribirseAutor_DeberiaRetornarConflict_CuandoYaExisteSuscripcion()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int authorId = 1;
            var parametros = new SuscribirseAutorDTO
            {
                userId = userId,
                authorId = authorId
            };
            string mensaje = $"El usuario con id {userId} ya esta suscripto al autor con id {authorId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.Conflict
            };

            _usuarioServiceMock.Setup(s => s.SuscribirseAutor(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioYaSuscripto, userId, authorId)).Returns(mensaje);

            var conflictObject = new ConflictObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(conflictObject);

            // Act
            var resultadoAccion = await _controller.SuscribirseAutor(parametros);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(conflictResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_EliminarSuscripcion_DeberiaRetornarOk_EnExito()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int authorId = 1;
            var parametros = new SuscribirseAutorDTO
            {
                userId = userId,
                authorId = authorId
            };
            string mensaje = $"Se eliminó la suscripción del usuario con id {userId} del autor con id {authorId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = true
            };

            _usuarioServiceMock.Setup(s => s.EliminarSuscripcion(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioEliminarSuscripcion, userId, authorId)).Returns(mensaje);

            // Act
            var resultadoAccion = await _controller.EliminarSuscripcion(parametros);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okObjectResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_EliminarSuscripcion_DeberiaRetornarNotFound_CuandoNoExisteSuscripcion()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int authorId = 1;
            var parametros = new SuscribirseAutorDTO
            {
                userId = userId,
                authorId = authorId
            };
            string mensaje = $"No se encontró una suscripción de un usuario con id {userId} a un autor con id {authorId}.";
            var resultado = new ResultadoOperacion
            {
                Exito = false,
                StatusCode = HttpStatusCode.NotFound
            };

            _usuarioServiceMock.Setup(s => s.EliminarSuscripcion(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.SuscripcionNoExiste, userId, authorId)).Returns(mensaje);

            var notFoundObject = new NotFoundObjectResult(new Respuesta
            {
                mensaje = mensaje
            });

            _manejarRespuestaMock.Setup(r => r.ManejarRespuestaDeError(resultado, It.IsAny<Respuesta>())).Returns(notFoundObject);

            // Act
            var resultadoAccion = await _controller.EliminarSuscripcion(parametros);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(notFoundResult.Value);
            Assert.Equal(0, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }

        [Fact]
        public async Task UsuarioController_ListadoDeUsuarios_DeberiaRetornarOk_EnExito()
        {
            // Arrange            
            var parametros = new OffsetLimitDTO
            {
                Offset = 0,
                Limit = 50
            };
            string mensaje = "Éxito al obtener el listado de usuarios";
            var resultado = new ResultadoOperacion
            {
                Exito = true
            };

            _usuarioServiceMock.Setup(s => s.ListadoDeUsuarios(parametros)).ReturnsAsync(resultado);
            _mensajesMock.Setup(m => m.GetMensaje(Mensajes.UsuarioListado)).Returns(mensaje);

            // Act
            var resultadoAccion = await _controller.ListadoDeUsuarios(parametros);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(resultadoAccion);
            var respuesta = Assert.IsType<Respuesta>(okObjectResult.Value);
            Assert.Equal(1, respuesta.exito);
            Assert.Equal(mensaje, respuesta.mensaje);
        }
    }
}
