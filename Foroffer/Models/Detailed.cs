using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Detailed
    {
        public Detailed()
        {
            Features = new HashSet<Feature>();
            Pictures = new HashSet<Picture>();
        }

        public int Id { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public string Outline { get; set; }
        public string CompanyInfo { get; set; }
        public ICollection<Feature> Features { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
