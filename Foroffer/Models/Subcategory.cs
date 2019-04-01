using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Subcategory
    {
        public Subcategory()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Post> Posts { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public ICollection<CompanySubcategory> CompanySubcategories { get; set; }
    }
}
