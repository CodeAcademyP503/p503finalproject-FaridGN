using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class SubViewModel
    {
        public int Id { get; set; }
        public Subcategory Subcategory { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
        public List<Category> Categories { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}
