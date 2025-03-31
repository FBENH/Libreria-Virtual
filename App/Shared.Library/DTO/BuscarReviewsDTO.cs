using Shared.Library.Enums;
using System.ComponentModel.DataAnnotations;


namespace Shared.Library.DTO
{
    public class BuscarReviewsDTO
    {
        public Calificacion? ReviewType { get; set; } // Si es null, se toman en cuenta todos los reviews
        public bool? Sort { get; set; } // null = sin orden, true = ascendente, false = descendente        
        [Range(0, int.MaxValue, ErrorMessage = "El valor de offset debe estar entre 0 y 2147483647")]
        public int Offset { get; set; } = 0;        
        [Range(1, int.MaxValue, ErrorMessage = "El valor de limit debe estar entre 1 y 2147483647")]
        public int Limit { get; set; } = 50;
    }
}
