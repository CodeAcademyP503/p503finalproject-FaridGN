using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string PicPath { get; set; }
        public Detailed Detailed { get; set; }
        public int DetailedId { get; set; }
    }
}
