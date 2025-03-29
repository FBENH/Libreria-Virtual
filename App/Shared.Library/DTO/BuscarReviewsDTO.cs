using Shared.Library.Enums;
using System.ComponentModel.DataAnnotations;


namespace Shared.Library.DTO
{
    public class BuscarReviewsDTO
    {
        public Calificacion? ReviewType { get; set; } // Si es null, se toman en cuenta todos los reviews
        public bool? Sort { get; set; } // null = sin orden, true = ascendente, false = descendente        
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;        
        [Range(1, 100)]
        public int Limit { get; set; } = 50;
    }
}
