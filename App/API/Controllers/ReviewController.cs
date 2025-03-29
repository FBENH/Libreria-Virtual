using API.Models.Respuesta;
using API.Services;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.DTO;
using Shared.Library.Mensajes.Mensajes;

namespace API.Controllers
{
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly MensajesService _mensajes;
        private readonly ManejarRespuestaDeErrorService _manejarRespuesta;

        public ReviewController(IReviewService reviewService,
                                MensajesService mensajes,
                                ManejarRespuestaDeErrorService manejarRespuesta)
        {
            _reviewService = reviewService;
            _mensajes = mensajes;
            _manejarRespuesta = manejarRespuesta;
        }


        [HttpGet]
        [Route("/api/v1.0/library/books/{bookId}/reviews")]
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
    }
}
