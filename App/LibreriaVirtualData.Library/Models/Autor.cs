using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Models
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nacionalidad { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
