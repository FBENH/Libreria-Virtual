using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace LibreriaVirtual.Logica.Services
{
    public interface IReviewService
    {
        Task<ResultadoOperacion> BuscarReviews(int idLibro, BuscarReviewsDTO parametros);
        Task<ResultadoOperacion> AgregarReview(int idLibro, Guid idUsuario, AgregarReviewDTO reviewDto);
    }
}