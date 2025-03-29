using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Library.Enums;

namespace LibreriaVirtualData.Library.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Opinion { get; set; }
        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        [Required]        
        public Calificacion Calificacion { get; set; }
        [Required]
        public Guid IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public int IdLibro { get; set; }
        [ForeignKey("IdLibro")]
        public virtual Libro Libro { get; set; }
    }    
}
