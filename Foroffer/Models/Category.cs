using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Category
    {
        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
