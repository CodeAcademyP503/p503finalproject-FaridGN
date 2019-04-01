using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(55)]
        public string Title { get; set; }
        [Required]
        [MaxLength(145)]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        public string URL { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public bool SpecDiscount { get; set; }
        public bool Gifted { get; set; }
        public Detailed Detailed { get; set; }
    }
}
