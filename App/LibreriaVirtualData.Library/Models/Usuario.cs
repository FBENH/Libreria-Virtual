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

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
