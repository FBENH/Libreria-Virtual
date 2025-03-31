using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library.DTO
{
    public class OffsetLimitDTO
    {
        [Required, Range(0, int.MaxValue, ErrorMessage = "El valor de offset debe estar entre 0 y 2147483647.")]
        public int Offset { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "El valor de limit debe estar entre 1 y 2147483647.")]
        public int Limit { get; set; }
    }
}
