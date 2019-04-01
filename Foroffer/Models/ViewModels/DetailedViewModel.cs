using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class DetailedViewModel
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public Detailed Detailed { get; set; }
        public IEnumerable<Detailed> Detaileds { get; set; }
        public IEnumerable<IFormFile> formFiles { get; set; }
        public IFormFile File { get; set; }
        public Feature Feature { get; set; }
        public IEnumerable<Feature> Features { get; set; }
        public Picture Picture { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}
