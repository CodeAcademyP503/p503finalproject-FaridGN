using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class NestedCategory
    {
        public NestedCategory()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
