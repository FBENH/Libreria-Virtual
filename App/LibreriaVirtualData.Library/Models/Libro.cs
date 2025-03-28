using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Models
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; }
        public string? Url { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Paginas { get; set; }
        [Required]
        [MaxLength(100)]
        public string Editorial { get; set; }
        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        [Required]
        public int IdAutor { get; set; }
        [Range(1,5)]
        public decimal? Calificacion { get; set; }
        [ForeignKey("IdAutor")]
        public virtual Autor Autor { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
