using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class SlideViewModel
    {
        public Image Image { get; set; }
        public IFormFile imageFile { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public Post Post { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}
