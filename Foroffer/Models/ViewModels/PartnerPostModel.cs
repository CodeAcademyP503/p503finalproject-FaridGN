using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class PartnerPostModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(55)]
        public string Title { get; set; }
        [Required]
        [MaxLength(145)]
        public string Description { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string URL { get; set; }
    }
}
