using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class BuscarLibrosDTO
    {
        public int? AuthorId { get; set; }
        public string? EditorialName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Before { get; set; }
        [DataType(DataType.Date)]
        public DateTime? After { get; set; }
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;
        [Range(1, int.MaxValue)]
        public int Limit { get; set; } = 50;
        public bool? Sort { get; set; }
    }
}
