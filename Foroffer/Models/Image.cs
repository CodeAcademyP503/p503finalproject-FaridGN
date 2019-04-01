using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
