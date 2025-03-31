using API.Helpers;
using API.Models.Respuesta;
using LibreriaVirtual.Logica.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;


namespace API.Controllers
{
    
    [ApiController]
    [Route("api/v1.0")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMensajesService _mensajes;
        private readonly IManejarRespuestaDeErrorService _manejarRespuesta;

        public ReviewController(IReviewService reviewService,
                                IMensajesService mensajes,
                                IManejarRespuestaDeErrorService manejarRespuesta)
        {
            _reviewService = reviewService;
            _mensajes = mensajes;
            _manejarRespuesta = manejarRespuesta;
        }


        [HttpGet]
        [Route("library/books/{bookId}/reviews")]
        public async Task<IActionResult> BuscarReviews([FromRoute] int bookId, [FromQuery]BuscarReviewsDTO parametros)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _reviewService.BuscarReviews(bookId, parametros);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.ReviewsListado);
                respuesta.data = resultado.Data;
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _manejarRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }
        }

        [HttpPost]
        [Route("library/books/{bookId}/reviews/from/users/{userId}")]
        public async Task<IActionResult> AgregarReview([FromRoute]int bookId, [FromRoute]Guid userId, [FromBody] AgregarReviewDTO reviewDto)
        {
            Respuesta respuesta = new Respuesta();
            ResultadoOperacion resultado = await _reviewService.AgregarReview(bookId, userId, reviewDto);

            if (resultado.Exito)
            {
                respuesta.exito = 1;
                respuesta.mensaje = _mensajes.GetMensaje(Mensajes.ReviewAgregar, [bookId, userId]);
                return Ok(respuesta);
            }
            else
            {
                respuesta.mensaje = resultado.TextoErrores();
                return _manejarRespuesta.ManejarRespuestaDeError(resultado, respuesta);
            }
        }
    }
}
