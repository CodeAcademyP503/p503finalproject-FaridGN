using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foroffer.Models.ViewModels
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}
