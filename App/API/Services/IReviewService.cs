using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace API.Services
{
    public interface IReviewService
    {
        Task<ResultadoOperacion> BuscarReviews(int idLibro, BuscarReviewsDTO parametros);
    }
}