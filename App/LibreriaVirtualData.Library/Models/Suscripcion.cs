using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Models
{
    public class Suscripcion
    {
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public Guid IdUsuario { get; set; }
        [ForeignKey("IdAutor")]
        public virtual Autor Autor { get; set; }
        [Required]
        public int IdAutor { get; set; }
    }
}
