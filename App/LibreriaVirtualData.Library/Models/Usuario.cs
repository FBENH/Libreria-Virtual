using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? UrlFoto { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();
    }
}
