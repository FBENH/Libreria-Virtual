using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace LibreriaVirtual.Logica.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewData _repositorioReviews;

        public ReviewService(IReviewData repositorioReviews)
        {
            _repositorioReviews = repositorioReviews;
        }

        public async Task<ResultadoOperacion> AgregarReview(int idLibro, Guid idUsuario, AgregarReviewDTO reviewDto)
        {
            ResultadoOperacion resultado = await _repositorioReviews.AgregarReview(idLibro, idUsuario, reviewDto);
            return resultado;
        }

        public async Task<ResultadoOperacion> BuscarReviews(int idLibro, BuscarReviewsDTO parametros)
        {
            ResultadoOperacion resultado = await _repositorioReviews.BuscarReviews(idLibro, parametros);
            return resultado;
        }
    }
}
