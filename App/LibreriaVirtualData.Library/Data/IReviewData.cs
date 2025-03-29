using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.DTO;

namespace LibreriaVirtualData.Library.Data
{
    public interface IReviewData
    {
        Task<ResultadoOperacion> BuscarReviews(int idLibro, BuscarReviewsDTO parametros);
    }
}