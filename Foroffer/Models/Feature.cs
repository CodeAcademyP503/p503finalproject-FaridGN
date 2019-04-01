using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Parameter { get; set; }
        public Detailed Detailed { get; set; }
        public int DetailedId { get; set; }
    }
}
