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
        [Required, Range(0, int.MaxValue)]
        public int Offset { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Limit { get; set; }
    }
}
