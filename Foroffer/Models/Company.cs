using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Company
    {
        public Company()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? VOEN { get; set; }
        public string LogoPath { get; set; }
        [Required]
        public string Address { get; set; }
        public ICollection<Post> Posts { get; set; }
        public string MoreLink { get; set; }
        public string Membership { get; set; }
        public ICollection<CompanySubcategory> CompanySubcategories { get; set; }
    }
}
